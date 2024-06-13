using AssemblyRemapper.Enums;
using AssemblyRemapper.Models;
using AssemblyRemapper.Utils;
using Mono.Cecil;

namespace AssemblyRemapper.Reflection;

internal class Remapper
{
    /// <summary>
    /// Start the remapping process
    /// </summary>
    public void InitializeRemap()
    {
        DisplayBasicModuleInformation();

        foreach (var remap in DataProvider.Remaps)
        {
            Logger.Log($"Finding best match for {remap.NewTypeName}...", ConsoleColor.Gray);

            HandleMapping(remap);
        }

        ChooseBestMatches();

        if (DataProvider.AppSettings.ScoringMode) { return; }

        // Dont publicize and unseal until after the remapping so we can use those as search parameters
        Publicizer.Publicize();
        Publicizer.Unseal();

        // We are done, write the assembly
        WriteAssembly();
    }

    /// <summary>
    /// Display information about the module we are remapping
    /// </summary>
    private void DisplayBasicModuleInformation()
    {
        Logger.Log("-----------------------------------------------", ConsoleColor.Yellow);
        Logger.Log($"Starting remap...", ConsoleColor.Yellow);
        Logger.Log($"Module contains {DataProvider.ModuleDefinition.Types.Count} Types", ConsoleColor.Yellow);
        Logger.Log($"Publicize: {DataProvider.AppSettings.Publicize}", ConsoleColor.Yellow);
        Logger.Log($"Unseal: {DataProvider.AppSettings.Unseal}", ConsoleColor.Yellow);
        Logger.Log("-----------------------------------------------", ConsoleColor.Yellow);
    }

    /// <summary>
    /// Loop over all types in the assembly and score them
    /// </summary>
    /// <param name="mapping">Mapping to score</param>
    private void HandleMapping(RemapModel mapping)
    {
        foreach (var type in DataProvider.ModuleDefinition.Types)
        {
            var result = ScoreType(type, mapping);

            if (result is not EFailureReason.None)
            {
                //Logger.LogDebug($"Remap [{type.Name} : {mapping.NewTypeName}] failed with reason {result}", silent: true);
            }
        }
    }

    /// <summary>
    /// Score the type against the remap checking against all remap properties
    /// </summary>
    /// <param name="type">Type to score</param>
    /// <param name="remap">Remap to check against</param>
    /// <param name="parentTypeName"></param>
    /// <returns>Failure reason or none if matched</returns>
    private EFailureReason ScoreType(TypeDefinition type, RemapModel remap)
    {
        // Handle Direct Remaps by strict naming first bypasses everything else
        if (remap.UseForceRename)
        {
            HandleByDirectName(type, remap);
            return EFailureReason.None;
        }

        foreach (var nestedType in type.NestedTypes)
        {
            ScoreType(nestedType, remap);
        }

        var score = new ScoringModel
        {
            ProposedNewName = remap.NewTypeName,
            ReMap = remap,
            Definition = type,
        };

        // Set the original type name to be used later
        score.ReMap.OriginalTypeName = type.Name;

        if (type.MatchIsAbstract(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            remap.FailureReason = EFailureReason.IsAbstract;
            return EFailureReason.IsAbstract;
        }

        if (type.MatchIsEnum(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            remap.FailureReason = EFailureReason.IsEnum;
            return EFailureReason.IsEnum;
        }

        if (type.MatchIsNested(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            remap.FailureReason = EFailureReason.IsNested;
            return EFailureReason.IsNested;
        }

        if (type.MatchIsSealed(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            remap.FailureReason = EFailureReason.IsSealed;
            return EFailureReason.IsSealed;
        }

        if (type.MatchIsDerived(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            remap.FailureReason = EFailureReason.IsDerived;
            return EFailureReason.IsDerived;
        }

        if (type.MatchIsInterface(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            remap.FailureReason = EFailureReason.IsInterface;
            return EFailureReason.IsInterface;
        }

        if (type.MatchHasGenericParameters(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            remap.FailureReason = EFailureReason.HasGenericParameters;
            return EFailureReason.HasGenericParameters;
        }

        if (type.MatchIsPublic(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            remap.FailureReason = EFailureReason.IsPublic;
            return EFailureReason.IsPublic;
        }

        if (type.MatchHasAttribute(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            remap.FailureReason = EFailureReason.HasAttribute;
            return EFailureReason.HasAttribute;
        }

        if (type.MatchMethods(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            remap.FailureReason = EFailureReason.HasMethods;
            return EFailureReason.HasMethods;
        }

        if (type.MatchFields(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            remap.FailureReason = EFailureReason.HasFields;
            return EFailureReason.HasFields;
        }

        if (type.MatchProperties(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            remap.FailureReason = EFailureReason.HasProperties;
            return EFailureReason.HasProperties;
        }

        if (type.MatchNestedTypes(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            remap.FailureReason = EFailureReason.HasNestedTypes;
            return EFailureReason.HasNestedTypes;
        }

        remap.OriginalTypeName = type.Name;
        remap.Succeeded = true;
        remap.FailureReason = EFailureReason.None;
        score.AddScoreToResult();

        return EFailureReason.None;
    }

    private void HandleByDirectName(TypeDefinition type, RemapModel remap)
    {
        if (type.Name != remap.OriginalTypeName) { return; }

        var oldName = type.Name;
        remap.OriginalTypeName = type.Name;
        remap.FailureReason = EFailureReason.None;
        remap.Succeeded = true;
        type.Name = remap.NewTypeName;

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
        Logger.Log($"Renamed {oldName} to {type.Name} directly", ConsoleColor.Green);

        Renamer.RenameAllDirect(remap, type);

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
                return;
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

        var highestScore = scores.OrderByDescending(score => score.Score).FirstOrDefault();

        if (highestScore is null) { return; }

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
        Logger.Log($"Renaming {highestScore.Definition.Name} to {highestScore.ProposedNewName}", ConsoleColor.Green);
        Logger.Log($"Max possible score: {highestScore.ReMap.SearchParams.CalculateMaxScore()}", ConsoleColor.Green);
        Logger.Log($"Scored: {highestScore.Score} points", ConsoleColor.Green);

        if (scores.Count > 1)
        {
            Logger.Log($"Warning! There were {scores.Count - 1} possible matches. Considering adding more search parameters", ConsoleColor.Yellow);

            foreach (var score in scores.OrderByDescending(score => score.Score).Skip(1))
            {
                Logger.Log($"{score.Definition.Name} - Score [{score.Score}]", ConsoleColor.Yellow);
            }
        }

        highestScore.ReMap.OriginalTypeName = highestScore.Definition.Name;

        // Rename type and all associated type members
        Renamer.RenameAll(highestScore);

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
    }

    /// <summary>
    /// Write the assembly back to disk and update the mapping file on disk
    /// </summary>
    private void WriteAssembly()
    {
        var filename = Path.GetFileNameWithoutExtension(DataProvider.AppSettings.AssemblyPath);
        var strippedPath = Path.GetDirectoryName(filename);

        filename = $"{filename}-Remapped.dll";

        var remappedPath = Path.Combine(strippedPath, filename);

        DataProvider.AssemblyDefinition.Write(remappedPath);
        DataProvider.UpdateMapping();

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
        Logger.Log($"Complete: Assembly written to `{remappedPath}`", ConsoleColor.Green);
        Logger.Log("Original type names updated on mapping file.", ConsoleColor.Green);
        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
    }
}