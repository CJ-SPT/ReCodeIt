using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using ReCodeIt.CrossCompiler;
using ReCodeIt.Enums;
using ReCodeIt.Models;
using ReCodeIt.ReMapper.Search;
using ReCodeIt.Utils;
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

    private readonly ReCodeItCrossCompiler _compiler;

    public static bool IsRunning { get; private set; } = false;

    public delegate void OnCompleteHandler();

    public event OnCompleteHandler? OnComplete;

    private static readonly Stopwatch Stopwatch = new();

    private RemapperSettings Settings => DataProvider.Settings.Remapper;

    private string OutPath { get; set; } = string.Empty;

    private bool CrossMapMode { get; set; } = false;

    private string AssemblyPath { get; set; }

    /// <summary>
    /// Start the remapping process
    /// </summary>
    public void InitializeRemap(
        List<RemapModel> remapModels,
        string assemblyPath,
        string outPath,
        bool crossMapMode = false)
    {
        DataProvider.LoadAssemblyDefinition(assemblyPath);

        AssemblyPath = assemblyPath;
        CrossMapMode = crossMapMode;

        OutPath = outPath;

        IsRunning = true;
        DisplayBasicModuleInformation();

        Stopwatch.Start();

        foreach (var remap in remapModels)
        {
            Logger.Log($"Finding best match for {remap.NewTypeName}...", ConsoleColor.Gray);

            ScoreMapping(remap);
        }

        ChooseBestMatches();

        // Don't publicize and unseal until after the remapping, so we can use those as search parameters
        if (Settings.MappingSettings.Publicize)
        {
            //Publicizer.Publicize();

            Logger.Log("Publicizing classes...", ConsoleColor.Green);

            SPTPublicizer.PublicizeClasses(DataProvider.ModuleDefinition);
        }

        if (Settings.MappingSettings.Unseal)
        {
            //Publicizer.Unseal();
        }

        // We are done, write the assembly
        WriteAssembly();

        if (CrossMapMode)
        {
            ProjectManager.SaveCrossCompilerProjectModel(_compiler.ActiveProject);
        }
    }

    /// <summary>
    /// Display information about the module we are remapping
    /// </summary>
    private void DisplayBasicModuleInformation()
    {
        Logger.Log("-----------------------------------------------", ConsoleColor.Yellow);
        Logger.Log($"Starting remap...", ConsoleColor.Yellow);
        Logger.Log($"Module contains {DataProvider.ModuleDefinition.Types.Count} Types", ConsoleColor.Yellow);
        Logger.Log($"Publicize: {Settings.MappingSettings.Publicize}", ConsoleColor.Yellow);
        Logger.Log($"Unseal: {Settings.MappingSettings.Unseal}", ConsoleColor.Yellow);
        Logger.Log("-----------------------------------------------", ConsoleColor.Yellow);
    }

    /// <summary>
    /// Loop over all types in the assembly and score them
    /// </summary>
    /// <param name="mapping">Mapping to score</param>
    public void ScoreMapping(RemapModel mapping)
    {
        foreach (var type in DataProvider.ModuleDefinition.Types)
        {
            FindMatch(type, mapping);
        }
    }

    /// <summary>
    /// Find a match result
    /// </summary>
    /// <param name="type">OriginalTypeRef to score</param>
    /// <param name="remap">Remap to check against</param>
    /// <param name="parentTypeName"></param>
    /// <returns>EMatchResult</returns>
    private void FindMatch(TypeDefinition type, RemapModel remap)
    {
        // Handle Direct Remaps by strict naming first bypasses everything else
        if (remap.UseForceRename)
        {
            HandleByDirectName(type, remap);
            return;
        }

        var tokens = DataProvider.Settings.AutoMapper.TokensToMatch;

        if (tokens.Where(token => !tokens.Any(token => type.Name.StartsWith(token))).Any())
        {
            return;
        }

        foreach (var nestedType in type.NestedTypes)
        {
            if (remap.SearchParams.IsNested is false) { return; }

            FindMatch(nestedType, remap);
        }

        var score = new ScoringModel
        {
            ProposedNewName = remap.NewTypeName,
            ReMap = remap,
            Definition = type,
        };

        var matches = new List<EMatchResult>
        {
            type.MatchConstructors(remap.SearchParams, score),
            type.MatchMethods(remap.SearchParams, score),
            type.MatchFields(remap.SearchParams, score),
            type.MatchProperties(remap.SearchParams, score),
            type.MatchNestedTypes(remap.SearchParams, score),
            type.MatchIsPublic(remap.SearchParams, score) ,
            type.MatchIsInterface(remap.SearchParams, score),
            type.MatchIsAbstract(remap.SearchParams, score),
            type.MatchIsSealed(remap.SearchParams, score) ,
            type.MatchIsEnum(remap.SearchParams, score) ,
            type.MatchIsNested(remap.SearchParams, score),
            type.MatchIsDerived(remap.SearchParams, score) ,
            type.MatchHasGenericParameters(remap.SearchParams, score),
            type.MatchHasAttribute(remap.SearchParams, score),
        };

        var NoMatch = matches.Where(x => x.Equals(EMatchResult.NoMatch)).FirstOrDefault();

        if (NoMatch == EMatchResult.NoMatch)
        {
            remap.FailureReason = score.FailureReason;
            return;
        }

        var match = matches
            .Where(x => x.Equals(EMatchResult.Match))
            .Where(x => !x.Equals(EMatchResult.Disabled))
            .Any();

        if (match)
        {
            // Set the original type name to be used later
            score.ReMap.OriginalTypeName = type.Name;
            remap.OriginalTypeName = type.Name;
            remap.Succeeded = true;
            remap.FailureReason = EFailureReason.None;
            score.AddScoreToResult();
        }
    }

    private void HandleByDirectName(TypeDefinition type, RemapModel remap)
    {
        if (type.Name != remap.OriginalTypeName) { return; }

        var oldName = type.Name;
        remap.OriginalTypeName = type.Name;
        remap.FailureReason = EFailureReason.None;
        remap.Succeeded = true;

        if (CrossMapMode)
        {
            // Store the original types for caching
            _compiler.ActiveProject.ChangedTypes.Add(remap.NewTypeName, type.Name);
        }

        type.Name = remap.NewTypeName;

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
        Logger.Log($"Renamed {oldName} to {type.Name} directly", ConsoleColor.Green);

        RenameHelper.RenameAllDirect(remap, type);

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
    }

    /// <summary>
    /// Choose the best possible match from all remaps
    /// </summary>
    private void ChooseBestMatches()
    {
        foreach (var score in DataProvider.ScoringModels)
        {
            ChooseBestMatch(score.Value);
        }

        var failures = 0;
        var changes = 0;

        foreach (var remap in DataProvider.Remaps)
        {
            if (remap.Succeeded is false)
            {
                Logger.Log("-----------------------------------------------", ConsoleColor.Red);
                Logger.Log($"Renaming {remap.NewTypeName} failed with reason {remap.FailureReason}", ConsoleColor.Red);
                Logger.Log("-----------------------------------------------", ConsoleColor.Red);
                failures++;
                continue;
            }

            changes++;
        }

        Logger.Log("-----------------------------------------------", ConsoleColor.Yellow);
        Logger.Log($"Result: Remapped {changes} Types. Failed to remap {failures} Types", ConsoleColor.Yellow);
        Logger.Log("-----------------------------------------------", ConsoleColor.Yellow);
    }

    /// <summary>
    /// Choose best match from a collection of scores, then start the renaming process
    /// </summary>
    /// <param name="scores">Scores to rate</param>
    private void ChooseBestMatch(HashSet<ScoringModel> scores)
    {
        if (scores.Count == 0) { return; }

        var filteredScores = scores
            .Where(score => score.Score > 0)
            .OrderByDescending(score => score.Score)
            .Take(5);

        var highestScore = filteredScores.FirstOrDefault();

        if (highestScore is null) { return; }

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
        Logger.Log($"Renaming {highestScore.Definition.Name} to {highestScore.ProposedNewName}", ConsoleColor.Green);
        Logger.Log($"Scored: {highestScore.Score} points", ConsoleColor.Green);

        if (filteredScores.Count() > 1 && filteredScores.Skip(1).Any(score => score.Score == highestScore.Score))
        {
            Logger.Log($"Warning! There were {filteredScores.Count()} possible matches. Considering adding more search parameters, Only showing first 5.", ConsoleColor.Yellow);

            foreach (var score in filteredScores.Skip(1).Take(5))
            {
                Logger.Log($"{score.Definition.Name} - Score [{score.Score}]", ConsoleColor.Yellow);
            }
        }

        // highestScore.ReMap.OriginalTypeName = highestScore.Definition.Name;

        if (CrossMapMode)
        {// Store the original types for caching
            _compiler.ActiveProject.ChangedTypes.Add(highestScore.ProposedNewName, highestScore.Definition.Name);
        }

        // Rename type and all associated type members

        RenameHelper.RenameAll(highestScore);

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
    }

    /// <summary>
    /// Write the assembly back to disk and update the mapping file on disk
    /// </summary>
    private void WriteAssembly()
    {
        if (!OutPath.EndsWith(".dll"))
        {
            var moduleName = DataProvider.AssemblyDefinition.MainModule.Name;
            moduleName = moduleName.Replace(".dll", "-Remapped.dll");

            OutPath = Path.Combine(OutPath, moduleName);
        }

        var path = DataProvider.WriteAssemblyDefinition(OutPath);

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
        Hollow();

        var hollowedDir = Path.GetDirectoryName(OutPath);
        var hollowedPath = Path.Combine(hollowedDir, "Hollowed.dll");
        DataProvider.WriteAssemblyDefinition(hollowedPath);

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
        Logger.Log($"Complete: Assembly written to `{path}`", ConsoleColor.Green);
        Logger.Log($"Complete: Hollowed written to `{hollowedPath}`", ConsoleColor.Green);
        Logger.Log("Original type names updated on mapping file.", ConsoleColor.Green);
        Logger.Log($"Remap took {Stopwatch.Elapsed.TotalSeconds:F1} seconds", ConsoleColor.Green);
        Logger.Log("-----------------------------------------------", ConsoleColor.Green);

        DataProvider.ScoringModels = [];
        Stopwatch.Reset();

        IsRunning = false;
        OnComplete?.Invoke();
    }

    /// <summary>
    /// Hollows out all logic from the dll
    /// </summary>
    private void Hollow()
    {
        foreach (var type in DataProvider.ModuleDefinition.GetAllTypes())
        {
            foreach (var method in type.Methods.Where(m => m.HasBody))
            {
                var ilProcessor = method.Body.GetILProcessor();

                // Remove existing instructions
                ilProcessor.Clear();

                if (method.ReturnType.FullName != "System.Void")
                {
                    // Return appropriate default value based on return type
                    if (method.ReturnType.IsValueType)
                    {
                        // Load 0 onto the stack (works for most value types)
                        ilProcessor.Emit(OpCodes.Ldc_I4_0);

                        // Convert to Int64 if needed
                        if (method.ReturnType.FullName == "System.Int64")
                            ilProcessor.Emit(OpCodes.Conv_I8);
                    }
                    else
                    {
                        // Load null for reference types
                        ilProcessor.Emit(OpCodes.Ldnull);
                    }
                }

                // Add a return instruction
                ilProcessor.Emit(OpCodes.Ret);
            }
        }
    }
}