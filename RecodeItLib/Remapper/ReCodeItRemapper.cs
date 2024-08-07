﻿using dnlib.DotNet;
using dnlib.DotNet.Emit;
using ReCodeIt.CrossCompiler;
using ReCodeIt.Enums;
using ReCodeIt.Models;
using ReCodeIt.ReMapper.Search;
using ReCodeIt.Utils;
using ReCodeItLib.Remapper.Search;
using System.Diagnostics;

namespace ReCodeIt.ReMapper;

public class ReCodeItRemapper
{
    public ReCodeItRemapper(ReCodeItCrossCompiler compiler)
    {
        _compiler = compiler;
    }

    public ReCodeItRemapper()
    { }

    private ModuleDefMD? Module { get; set; }

    private readonly ReCodeItCrossCompiler _compiler;

    public static bool IsRunning { get; private set; } = false;

    public delegate void OnCompleteHandler();

    public event OnCompleteHandler? OnComplete;

    private static readonly Stopwatch Stopwatch = new();

    private RemapperSettings Settings => DataProvider.Settings.Remapper;

    private string OutPath { get; set; } = string.Empty;

    private bool CrossMapMode { get; set; } = false;

    private string AssemblyPath { get; set; }

    private List<RemapModel> _remaps = [];

    private List<string> _alreadyGivenNames = [];

    /// <summary>
    /// Start the remapping process
    /// </summary>
    public void InitializeRemap(
        List<RemapModel> remapModels,
        string assemblyPath,
        string outPath,
        bool crossMapMode = false,
        bool validate = false)
    {
        _remaps = [];
        _remaps = remapModels;
        Module = DataProvider.LoadModule(assemblyPath);

        AssemblyPath = assemblyPath;
        CrossMapMode = crossMapMode;

        OutPath = outPath;

        if (!Validate(_remaps)) return;

        IsRunning = true;
        Stopwatch.Start();

        var types = Module.GetTypes();

        var tasks = new List<Task>(remapModels.Count);
        foreach (var remap in remapModels)
        {
            tasks.Add(
                Task.Factory.StartNew(() =>
                {
                    Logger.Log($"\nFinding best match for {remap.NewTypeName}...", ConsoleColor.Gray);
                    ScoreMapping(remap, types);
                })
            );
        }
        Task.WaitAll(tasks.ToArray());

        ChooseBestMatches();

        // Don't go any further during a validation
        if (validate)
        {
            DisplayEndBanner(validate: true);
            return;
        }

        var renameTasks = new List<Task>(remapModels.Count);
        foreach (var remap in remapModels)
        {
            renameTasks.Add(
                Task.Factory.StartNew(() =>
                {
                    RenameHelper.RenameAll(types, remap);
                })
            );
        }
        Task.WaitAll(renameTasks.ToArray());

        // Don't publicize and unseal until after the remapping, so we can use those as search parameters
        if (Settings.MappingSettings.Publicize)
        {
            Logger.Log("Publicizing classes...", ConsoleColor.Yellow);

            SPTPublicizer.PublicizeClasses(Module);
        }

        // We are done, write the assembly
        WriteAssembly();

        if (CrossMapMode)
        {
            ProjectManager.SaveCrossCompilerProjectModel(_compiler.ActiveProject);
        }
    }

    private bool Validate(List<RemapModel> remaps)
    {
        var duplicateGroups = remaps
            .GroupBy(m => m.NewTypeName)
            .Where(g => g.Count() > 1)
            .ToList();

        if (duplicateGroups.Count() > 1)
        {
            Logger.Log($"There were {duplicateGroups.Count()} duplicated sets of remaps.", ConsoleColor.Yellow);

            foreach (var duplicate in duplicateGroups)
            {
                var duplicateNewTypeName = duplicate.Key;
                Logger.Log($"Ambiguous NewTypeName: {duplicateNewTypeName} found. Cancelling Remap.", ConsoleColor.Red);
            }

            return false;
        }

        return true;
    }

    /// <summary>
    /// First we filter our type collection based on simple search parameters (true/false/null)
    /// where null is a third disabled state. Then we score the types based on the search parameters
    /// </summary>
    /// <param name="mapping">Mapping to score</param>
    private void ScoreMapping(RemapModel mapping, IEnumerable<TypeDef> types)
    {
        var tokens = DataProvider.Settings.AutoMapper.TokensToMatch;

        if (mapping.UseForceRename)
        {
            HandleDirectRename(mapping, types);
            return;
        }

        if (mapping.SearchParams.IsNested is false or null)
        {
            types = types.Where(type => tokens.Any(token => type.Name.StartsWith(token)));
        }

        types = GenericTypeFilters.FilterPublic(types, mapping.SearchParams);

        if (!types.Any())
        {
            Logger.Log($"All types filtered out after public filter for: {mapping.NewTypeName}", ConsoleColor.Red);
        }

        types = GenericTypeFilters.FilterAbstract(types, mapping.SearchParams);
        types = GenericTypeFilters.FilterSealed(types, mapping.SearchParams);
        types = GenericTypeFilters.FilterInterface(types, mapping.SearchParams);
        types = GenericTypeFilters.FilterStruct(types, mapping.SearchParams);
        types = GenericTypeFilters.FilterEnum(types, mapping.SearchParams);
        types = GenericTypeFilters.FilterAttributes(types, mapping.SearchParams);
        types = GenericTypeFilters.FilterDerived(types, mapping.SearchParams);
        types = GenericTypeFilters.FilterByGenericParameters(types, mapping.SearchParams);

        types = MethodTypeFilters.FilterByInclude(types, mapping.SearchParams);
        types = MethodTypeFilters.FilterByExclude(types, mapping.SearchParams);
        types = MethodTypeFilters.FilterByCount(types, mapping.SearchParams);

        types = FieldTypeFilters.FilterByInclude(types, mapping.SearchParams);
        types = FieldTypeFilters.FilterByExclude(types, mapping.SearchParams);
        types = FieldTypeFilters.FilterByCount(types, mapping.SearchParams);

        types = PropertyTypeFilters.FilterByInclude(types, mapping.SearchParams);
        types = PropertyTypeFilters.FilterByExclude(types, mapping.SearchParams);
        types = PropertyTypeFilters.FilterByCount(types, mapping.SearchParams);

        types = CtorTypeFilters.FilterByParameterCount(types, mapping.SearchParams);

        types = NestedTypeFilters.FilterByInclude(types, mapping.SearchParams);
        types = NestedTypeFilters.FilterByExclude(types, mapping.SearchParams);
        types = NestedTypeFilters.FilterByCount(types, mapping.SearchParams);

        mapping.TypeCandidates.UnionWith(types);
    }

    private void HandleDirectRename(RemapModel mapping, IEnumerable<TypeDef> types)
    {
        foreach (var type in types)
        {
            if (type.Name == mapping.OriginalTypeName)
            {
                mapping.TypePrimeCandidate = type;
                mapping.OriginalTypeName = type.Name.String;
                mapping.Succeeded = true;

                _alreadyGivenNames.Add(mapping.OriginalTypeName);

                if (CrossMapMode)
                {// Store the original types for caching
                    _compiler.ActiveProject.ChangedTypes.Add(mapping.NewTypeName, mapping.OriginalTypeName);
                }

                return;
            }
        }
    }

    /// <summary>
    /// Choose the best possible match from all remaps
    /// </summary>
    private void ChooseBestMatches()
    {
        foreach (var remap in _remaps)
        {
            ChooseBestMatch(remap);
        }
    }

    /// <summary>
    /// Choose best match from a collection of types on a remap
    /// </summary>
    /// <param name="remap"></param>
    private void ChooseBestMatch(RemapModel remap)
    {
        if (remap.TypeCandidates.Count == 0 || remap.Succeeded) { return; }

        var winner = remap.TypeCandidates.FirstOrDefault();
        remap.TypePrimeCandidate = winner;
        remap.OriginalTypeName = winner.Name.String;

        if (winner is null) { return; }

        if (_alreadyGivenNames.Contains(winner.FullName))
        {
            remap.NoMatchReasons.Add(ENoMatchReason.AmbiguousWithPreviousMatch);
            remap.AmbiguousTypeMatch = winner.FullName;
            remap.Succeeded = false;

            return;
        }

        _alreadyGivenNames.Add(remap.OriginalTypeName);

        remap.Succeeded = true;

        remap.OriginalTypeName = winner.Name.String;

        if (CrossMapMode)
        {// Store the original types for caching
            _compiler.ActiveProject.ChangedTypes.Add(winner.Name.String, remap.OriginalTypeName);
        }
    }

    /// <summary>
    /// Write the assembly back to disk and update the mapping file on disk
    /// </summary>
    private void WriteAssembly()
    {
        var moduleName = Module.Name;

        moduleName = CrossMapMode
            ? moduleName
            : moduleName.Replace(".dll", "-Remapped.dll");

        OutPath = Path.Combine(OutPath, moduleName);

        try
        {
            Module.Write(OutPath);
        }
        catch (Exception e)
        {
            Logger.Log(e);
            throw;
        }

        if (!CrossMapMode)
        {
            Logger.Log("Creating Hollow...", ConsoleColor.Yellow);
            Hollow();

            var hollowedDir = Path.GetDirectoryName(OutPath);
            var hollowedPath = Path.Combine(hollowedDir, "Hollowed.dll");
            Module.Write(hollowedPath);

            DisplayEndBanner(hollowedPath);
        }

        if (DataProvider.Settings.Remapper.MappingPath != string.Empty)
        {
            DataProvider.UpdateMapping(DataProvider.Settings.Remapper.MappingPath, _remaps);
        }

        Stopwatch.Reset();
        Module = null;

        IsRunning = false;
        OnComplete?.Invoke();
    }

    /// <summary>
    /// Hollows out all logic from the dll
    /// </summary>
    private void Hollow()
    {
        foreach (var type in Module.GetTypes())
        {
            foreach (var method in type.Methods.Where(m => m.HasBody))
            {
                if (!method.HasBody) continue;

                method.Body = new CilBody();
                method.Body.Instructions.Add(OpCodes.Ret.ToInstruction());
            }
        }
    }

    private void DisplayEndBanner(string hollowedPath = "", bool validate = false)
    {
        var failures = 0;
        var changes = 0;

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
        Logger.Log("-----------------------------------------------", ConsoleColor.Green);

        foreach (var remap in _remaps)
        {
            if (remap.Succeeded is false) { continue; }

            var original = remap.OriginalTypeName;
            var proposed = remap.NewTypeName;

            Logger.Log($"Renamed {original} to {proposed}", ConsoleColor.Green);

            DisplayAlternativeMatches(remap);
        }

        foreach (var remap in _remaps)
        {
            if (remap.Succeeded is false && remap.NoMatchReasons.Contains(ENoMatchReason.AmbiguousWithPreviousMatch))
            {
                Logger.Log("----------------------------------------------------------------------", ConsoleColor.Red);
                Logger.Log("Ambiguous match with a previous match during matching. Skipping remap.", ConsoleColor.Red);
                Logger.Log($"New Type Name: {remap.NewTypeName}", ConsoleColor.Red);
                Logger.Log($"{remap.AmbiguousTypeMatch} already assigned to a previous match.", ConsoleColor.Red);
                Logger.Log("----------------------------------------------------------------------", ConsoleColor.Red);
            }
            else if (remap.Succeeded is false)
            {
                Logger.Log("-----------------------------------------------", ConsoleColor.Red);
                Logger.Log($"Renaming {remap.NewTypeName} failed with reason(s)", ConsoleColor.Red);

                foreach (var reason in remap.NoMatchReasons)
                {
                    Logger.Log($"Reason: {reason}", ConsoleColor.Red);
                }

                Logger.Log("-----------------------------------------------", ConsoleColor.Red);
                failures++;
                continue;
            }

            changes++;
        }

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
        Logger.Log($"Result renamed {changes} Types. Failed to rename {failures} Types", ConsoleColor.Green);

        if (!validate)
        {
            Logger.Log($"Assembly written to `{OutPath}`", ConsoleColor.Green);
            Logger.Log($"Hollowed written to `{hollowedPath}`", ConsoleColor.Green);
            Logger.Log($"Remap took {Stopwatch.Elapsed.TotalSeconds:F1} seconds", ConsoleColor.Green);
        }
    }

    private void DisplayAlternativeMatches(RemapModel remap)
    {
        if (remap.TypeCandidates.Count() > 1)
        {
            Logger.Log($"Warning! There were {remap.TypeCandidates.Count()} possible matches for {remap.NewTypeName}. Consider adding more search parameters, Only showing the first 5.", ConsoleColor.Yellow);

            foreach (var type in remap.TypeCandidates.Skip(1).Take(5))
            {
                Logger.Log($"{type.Name}", ConsoleColor.Yellow);
            }
        }
    }
}