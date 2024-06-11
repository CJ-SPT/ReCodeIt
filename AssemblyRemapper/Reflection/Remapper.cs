using AssemblyRemapper.Models;
using AssemblyRemapper.Utils;
using Mono.Cecil;

namespace AssemblyRemapper.Reflection;

internal class Remapper
{
    public static Dictionary<string, HashSet<ScoringModel>> ScoringModels { get; set; } = [];

    public void InitializeRemap()
    {
        // Make sure any previous results are cleared just incase
        ScoringModels.Clear();

        DisplayBasicModuleInformation();
        StartRemap();
    }

    private void DisplayBasicModuleInformation()
    {
        Logger.Log($"Module contains {DataProvider.ModuleDefinition.Types.Count} Types");
        Logger.Log($"Starting remap...");
        Logger.Log($"Publicize: {DataProvider.AppSettings.Publicize}");
        Logger.Log($"Unseal: {DataProvider.AppSettings.Unseal}");
    }

    private void StartRemap()
    {
        foreach (var remap in DataProvider.AppSettings.Remaps)
        {
            Logger.Log("-----------------------------------------------");
            Logger.Log($"Trying to remap {remap.NewTypeName}...");

            HandleMapping(remap);
        }

        ChooseBestMatches();

        if (DataProvider.AppSettings.ScoringMode) { return; }

        // Dont publicize and unseal until after the remapping so we can use those as search parameters
        HandlePublicize();
        HandleUnseal();

        // We are done, write the assembly
        WriteAssembly();
    }

    private void HandleMapping(Remap mapping)
    {
        string newName = mapping.NewTypeName;
        string oldName = mapping?.OldTypeName ?? string.Empty;

        bool useDirectRename = mapping.UseDirectRename;

        foreach (var type in DataProvider.ModuleDefinition.Types)
        {
            // Handle Direct Remaps by strict naming first bypasses everything else
            if (useDirectRename)
            {
                HandleByDirectName(oldName, newName, type);
                continue;
            }

            ScoreType(type, mapping);
        }
    }

    private void HandlePublicize()
    {
        if (!DataProvider.AppSettings.Publicize) { return; }

        Logger.Log("Starting publicization...");

        foreach (var type in DataProvider.ModuleDefinition.Types)
        {
            if (type.IsNotPublic) { type.IsPublic = true; }

            // We only want to do methods and properties

            if (type.HasMethods)
            {
                foreach (var method in type.Methods)
                {
                    method.IsPublic = true;
                }
            }

            if (type.HasProperties)
            {
                foreach (var property in type.Properties)
                {
                    if (property.SetMethod != null)
                    {
                        property.SetMethod.IsPublic = true;
                    }

                    if (property.GetMethod != null)
                    {
                        property.GetMethod.IsPublic = true;
                    }
                }
            }
        }
    }

    private void HandleUnseal()
    {
        if (!DataProvider.AppSettings.Unseal) { return; }

        Logger.Log("Starting unseal...");

        foreach (var type in DataProvider.ModuleDefinition.Types)
        {
            if (type.IsSealed) { type.IsSealed = false; }
        }
    }

    private void HandleByDirectName(string oldName, string newName, TypeDefinition type)
    {
        if (type.Name != oldName) { return; }

        Logger.Log($"Renaming directly...");

        type.Name = newName;

        RenameService.RenameAllFields(oldName, newName, DataProvider.ModuleDefinition.Types);
        RenameService.RenameAllProperties(oldName, newName, DataProvider.ModuleDefinition.Types);

        Logger.Log($"Renamed {oldName} to {newName}");
        Logger.Log("-----------------------------------------------");
    }

    private void ScoreType(TypeDefinition type, Remap remap, string parentTypeName = "")
    {
        foreach (var nestedType in type.NestedTypes)
        {
            ScoreType(nestedType, remap, type.Name);
        }

        var score = new ScoringModel
        {
            Definition = type,
            ProposedNewName = remap.NewTypeName,
        };

        if (type.MatchIsAbstract(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            return;
        }
        /*
        if (type.MatchIsEnum(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            return;
        }

        if (type.MatchIsNested(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            return;
        }

        type.MatchIsSealed(remap.SearchParams, score);

        if (type.MatchIsSealed(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            return;
        }

        if (type.MatchIsDerived(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            return;
        }

        if (type.MatchIsInterface(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            return;
        }

        if (type.MatchIsGeneric(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            return;
        }

        if (type.MatchIsPublic(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            return;
        }

        if (type.MatchHasAttribute(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            return;
        }

        if (type.MatchMethods(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            return;
        }

        if (type.MatchFields(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            return;
        }

        if (type.MatchProperties(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            return;
        }

        if (type.MatchNestedTypes(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            return;
        }
        */
        ScoringModelExtensions.AddModelToResult(score);
    }

    private void ChooseBestMatches()
    {
        foreach (var remap in ScoringModels)
        {
            ChooseBestMatch(remap.Value, true);
        }
    }

    private void ChooseBestMatch(HashSet<ScoringModel> scores, bool isBest = false)
    {
        if (ScoringModels.Count == 0)
        {
            return;
        }

        var highestScore = scores.OrderByDescending(model => model.Score).FirstOrDefault();
        var secondScore = scores.OrderByDescending(model => model.Score).Skip(1).FirstOrDefault();

        if (highestScore is null || secondScore is null) { return; }

        var potentialText = isBest
            ? "Best potential"
            : "Next potential";

        if (highestScore.Score <= 0) { return; }

        Logger.Log("-----------------------------------------------");
        Logger.Log($"Found {scores.Count} possible matches");
        Logger.Log($"Scored: {highestScore.Score} points");
        Logger.Log($"Next Best: {secondScore.Score} points");
        Logger.Log($"{potentialText} match is `{highestScore.Definition.Name}` for `{highestScore.ProposedNewName}`");

        if (DataProvider.AppSettings.ScoringMode)
        {
            Logger.Log("Show next result? (y/n)");
            var answer = Console.ReadLine();

            if (answer == "yes" || answer == "y")
            {
                scores.Remove(highestScore);
                ChooseBestMatch(scores);
            }

            Logger.Log("-----------------------------------------------");
            return;
        }

        var anwser = "";

        if (!DataProvider.AppSettings.SilentMode)
        {
            Logger.Log($"Should we continue? (y/n)");
            anwser = Console.ReadLine();
        }

        if (anwser == "yes" || anwser == "y" || DataProvider.AppSettings.SilentMode)
        {
            var oldName = highestScore.Definition.Name;

            highestScore.Definition.Name = highestScore.ProposedNewName;

            RenameService.RenameAllFields(oldName, highestScore.Definition.Name, DataProvider.ModuleDefinition.Types);
            RenameService.RenameAllProperties(oldName, highestScore.Definition.Name, DataProvider.ModuleDefinition.Types);

            Logger.Log($"Remapped {oldName} to `{highestScore.Definition.Name}`");
            Logger.Log("-----------------------------------------------");
            return;
        }

        scores.Remove(highestScore);
        ChooseBestMatch(scores);
    }

    private void WriteAssembly()
    {
        var filename = Path.GetFileNameWithoutExtension(DataProvider.AppSettings.AssemblyPath);
        var strippedPath = Path.GetDirectoryName(filename);

        filename = $"{filename}-Remapped.dll";

        var remappedPath = Path.Combine(strippedPath, filename);

        DataProvider.AssemblyDefinition.Write(remappedPath);

        Logger.Log("-----------------------------------------------");
        Logger.Log($"Complete: Assembly written to `{remappedPath}`");
        Logger.Log("-----------------------------------------------");
    }
}