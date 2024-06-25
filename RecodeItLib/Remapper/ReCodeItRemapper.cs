using dnlib.DotNet;
using dnlib.DotNet.Emit;
using ReCodeIt.CrossCompiler;
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

    private ModuleDefMD Module { get; set; }

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

    /// <summary>
    /// Start the remapping process
    /// </summary>
    public void InitializeRemap(
        List<RemapModel> remapModels,
        string assemblyPath,
        string outPath,
        bool crossMapMode = false)
    {
        _remaps = remapModels;
        Module = DataProvider.LoadModule(assemblyPath);

        AssemblyPath = assemblyPath;
        CrossMapMode = crossMapMode;

        OutPath = outPath;

        IsRunning = true;
        DisplayBasicModuleInformation();

        Stopwatch.Start();

        var tasks = new List<Task>(remapModels.Count);
        foreach (var remap in remapModels)
        {
            tasks.Add(
                Task.Factory.StartNew(() =>
                {
                    Logger.Log($"\nFinding best match for {remap.NewTypeName}...", ConsoleColor.Gray);
                    ScoreMapping(remap);
                })
            );
        }
        Task.WaitAll(tasks.ToArray());

        ChooseBestMatches();

        // Don't publicize and unseal until after the remapping, so we can use those as search parameters
        if (Settings.MappingSettings.Publicize)
        {
            //Publicizer.Publicize();

            Logger.Log("Publicizing classes...", ConsoleColor.Green);

            //SPTPublicizer.PublicizeClasses(DataProvider.ModuleDefinition);
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
        Logger.Log($"Module contains {Module.GetTypes().Count()} Types", ConsoleColor.Yellow);
        Logger.Log($"Publicize: {Settings.MappingSettings.Publicize}", ConsoleColor.Yellow);
        Logger.Log($"Unseal: {Settings.MappingSettings.Unseal}", ConsoleColor.Yellow);
        Logger.Log("-----------------------------------------------", ConsoleColor.Yellow);
    }

    /// <summary>
    /// First we filter our type collection based on simple search parameters (true/false/null)
    /// where null is a third disabled state. Then we score the types based on the search parameters
    /// </summary>
    /// <param name="mapping">Mapping to score</param>
    private void ScoreMapping(RemapModel mapping)
    {
        var types = Module.GetTypes();

        var tokens = DataProvider.Settings.AutoMapper.TokensToMatch;

        if (mapping.SearchParams.IsNested is false or null)
        {
            types = types.Where(type => tokens.Any(token => type.Name.StartsWith(token)));
        }

        types = TypeFilters.FilterPublic(types, mapping.SearchParams);

        if (!types.Any())
        {
            Logger.Log($"All types filtered out after public filter for: {mapping.NewTypeName}", ConsoleColor.Red);
        }

        types = TypeFilters.FilterAbstract(types, mapping.SearchParams);

        types = TypeFilters.FilterInterface(types, mapping.SearchParams);

        types = TypeFilters.FilterStruct(types, mapping.SearchParams);

        types = TypeFilters.FilterEnum(types, mapping.SearchParams);

        types = TypeFilters.FilterAttributes(types, mapping.SearchParams);

        types = TypeFilters.FilterDerived(types, mapping.SearchParams);

        foreach (var type in types)
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
    private void FindMatch(TypeDef type, RemapModel remap)
    {
        // Handle Direct Remaps by strict naming first bypasses everything else
        if (remap.UseForceRename)
        {
            HandleByDirectName(type, remap);
            return;
        }

        foreach (var nestedType in type.NestedTypes)
        {
            FindMatch(nestedType, remap);
        }

        var score = new ScoringModel
        {
            ProposedNewName = remap.NewTypeName,
            ReMap = remap,
            Definition = type,
        };

        if (score.ProposedNewName == "GameServerClass")
        {
            //Console.WriteLine("BreakPoint!");
        }

        type.MatchConstructors(remap.SearchParams, score);
        type.MatchMethods(remap.SearchParams, score);
        type.MatchFields(remap.SearchParams, score);
        type.MatchProperties(remap.SearchParams, score);
        type.MatchNestedTypes(remap.SearchParams, score);

        if (score.Score == 0)
        {
            remap.NoMatchReasons = score.NoMatchReasons;
            return;
        }

        // Set the original type name to be used later
        score.ReMap.OriginalTypeName = type.FullName;
        remap.OriginalTypeName = type.FullName;
        score.AddScoreToResult();
    }

    private void HandleByDirectName(TypeDef type, RemapModel remap)
    {
        if (type.Name != remap.OriginalTypeName) { return; }

        var oldName = type.Name;
        remap.OriginalTypeName = type.Name;
        remap.NoMatchReasons.Clear();
        remap.Succeeded = true;

        if (CrossMapMode)
        {
            // Store the original types for caching
            _compiler.ActiveProject.ChangedTypes.Add(remap.NewTypeName, type.Name);
        }

        type.Name = remap.NewTypeName;

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
        Logger.Log($"Renamed {oldName} to {type.Name} directly", ConsoleColor.Green);

        RenameHelper.RenameAllDirect(Module, remap, type);

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

        foreach (var remap in _remaps)
        {
            if (remap.Succeeded is false)
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

        highestScore.ReMap.Succeeded = true;

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
        Logger.Log($"Renaming {highestScore.Definition.FullName} to {highestScore.ProposedNewName}", ConsoleColor.Green);
        Logger.Log($"Scored: {highestScore.Score} points", ConsoleColor.Green);

        DisplayAlternativeMatches(filteredScores, highestScore);

        highestScore.ReMap.OriginalTypeName = highestScore.Definition.Name;

        if (CrossMapMode)
        {// Store the original types for caching
            _compiler.ActiveProject.ChangedTypes.Add(highestScore.ProposedNewName, highestScore.Definition.Name);
        }

        // Rename type and all associated type members

        //RenameHelper.RenameAll(Module, highestScore);

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
    }

    private void DisplayAlternativeMatches(IEnumerable<ScoringModel> filteredScores, ScoringModel highestScore)
    {
        if (filteredScores.Count() > 1 && filteredScores.Skip(1).Any(score => score.Score == highestScore.Score))
        {
            Logger.Log($"Warning! There were {filteredScores.Count()} possible matches. Consider adding more search parameters, Only showing the first 5.", ConsoleColor.Yellow);

            foreach (var score in filteredScores.Skip(1).Take(5))
            {
                Logger.Log($"{score.Definition.Name} - Score [{score.Score}]", ConsoleColor.Yellow);
            }
        }
    }

    /// <summary>
    /// Write the assembly back to disk and update the mapping file on disk
    /// </summary>
    private void WriteAssembly()
    {
        var moduleName = Module.Name;
        moduleName = moduleName.Replace(".dll", "-Remapped.dll");

        OutPath = Path.Combine(OutPath, moduleName);

        Module.Write(OutPath);

        Logger.Log("Creating Hollow...", ConsoleColor.Yellow);
        Hollow();

        var hollowedDir = Path.GetDirectoryName(OutPath);
        var hollowedPath = Path.Combine(hollowedDir, "Hollowed.dll");
        Module.Write(hollowedPath);

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
        Logger.Log($"Complete: Assembly written to `{OutPath}`", ConsoleColor.Green);
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
        foreach (var type in Module.GetTypes())
        {
            foreach (var method in type.Methods.Where(m => m.HasBody))
            {
                if (!method.HasBody) { continue; }

                method.Body = new CilBody();
            }
        }
    }
}