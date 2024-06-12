using AssemblyRemapper.Enums;
using AssemblyRemapper.Models;
using AssemblyRemapper.Utils;
using Mono.Cecil;

namespace AssemblyRemapper.Reflection;

internal class Remapper
{
    public void InitializeRemap()
    {
        DisplayBasicModuleInformation();

        foreach (var remap in DataProvider.Remaps)
        {
            Logger.Log($"Trying to remap {remap.NewTypeName}...", ConsoleColor.Gray);

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

    private void DisplayBasicModuleInformation()
    {
        Logger.Log("-----------------------------------------------", ConsoleColor.Yellow);
        Logger.Log($"Starting remap...", ConsoleColor.Yellow);
        Logger.Log($"Module contains {DataProvider.ModuleDefinition.Types.Count} Types", ConsoleColor.Yellow);
        Logger.Log($"Publicize: {DataProvider.AppSettings.Publicize}", ConsoleColor.Yellow);
        Logger.Log($"Unseal: {DataProvider.AppSettings.Unseal}", ConsoleColor.Yellow);
        Logger.Log("-----------------------------------------------", ConsoleColor.Yellow);
    }

    private void HandleMapping(RemapModel mapping)
    {
        foreach (var type in DataProvider.ModuleDefinition.Types)
        {
            ScoreType(type, mapping);
        }
    }

    private void HandlePublicize()
    {
        if (!DataProvider.AppSettings.Publicize) { return; }

        Logger.Log("Starting publicization...", ConsoleColor.Green);

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

        Logger.Log("Starting unseal...", ConsoleColor.Green);

        foreach (var type in DataProvider.ModuleDefinition.Types)
        {
            if (type.IsSealed) { type.IsSealed = false; }
        }
    }

    private void ScoreType(TypeDefinition type, RemapModel remap, string parentTypeName = "")
    {
        // Handle Direct Remaps by strict naming first bypasses everything else
        if (remap.UseForceRename)
        {
            HandleByDirectName(type, remap);
            return;
        }

        foreach (var nestedType in type.NestedTypes)
        {
            ScoreType(nestedType, remap, type.Name);
        }

        var score = new ScoringModel
        {
            ProposedNewName = remap.NewTypeName,
            RemapModel = remap,
            Definition = type,
        };

        // Set the original type name to be used later
        score.RemapModel.OriginalTypeName = type.Name;

        if (type.MatchIsAbstract(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            LogDiscard("IsAbstract", type.Name, score.ProposedNewName);
            return;
        }

        if (type.MatchIsEnum(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            LogDiscard("IsEnum", type.Name, score.ProposedNewName);
            return;
        }

        if (type.MatchIsNested(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            LogDiscard("IsNested", type.Name, score.ProposedNewName);
            return;
        }

        if (type.MatchIsSealed(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            LogDiscard("IsSealed", type.Name, score.ProposedNewName);
            return;
        }

        if (type.MatchIsDerived(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            LogDiscard("IsDerived", type.Name, score.ProposedNewName);
            return;
        }

        if (type.MatchIsInterface(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            LogDiscard("IsInterface", type.Name, score.ProposedNewName);
            return;
        }

        if (type.MatchIsGeneric(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            return;
        }

        if (type.MatchIsPublic(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            LogDiscard("IsPublic", type.Name, score.ProposedNewName);
            return;
        }

        if (type.MatchHasAttribute(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            LogDiscard("HasAttribute", type.Name, score.ProposedNewName);
            return;
        }

        if (type.MatchMethods(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            LogDiscard("Methods", type.Name, score.ProposedNewName);
            return;
        }

        if (type.MatchFields(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            LogDiscard("Fields", type.Name, score.ProposedNewName);
            return;
        }

        if (type.MatchProperties(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            LogDiscard("Properties", type.Name, score.ProposedNewName);
            return;
        }

        if (type.MatchNestedTypes(remap.SearchParams, score) == EMatchResult.NoMatch)
        {
            LogDiscard("NestedTypes", type.Name, score.ProposedNewName);
            return;
        }

        ScoringModelExtensions.AddModelToResult(score);
    }

    private void HandleByDirectName(TypeDefinition type, RemapModel remap)
    {
        if (type.Name != remap.OriginalTypeName) { return; }

        var oldName = type.Name;
        type.Name = remap.NewTypeName;

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
        Logger.Log($"Renamed {oldName} to {type.Name} directly", ConsoleColor.Green);

        RenameService.RenameAllDirect(remap, type);

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
    }

    private void LogDiscard(string action, string type, string search)
    {
        if (DataProvider.AppSettings.Debug)
        {
            Logger.Log($"[{action}] Discarding type [{type}] for search [{search}]", ConsoleColor.Red);
        }
    }

    private void ChooseBestMatches()
    {
        foreach (var score in DataProvider.ScoringModels)
        {
            ChooseBestMatch(score.Value, true);
        }
    }

    private void ChooseBestMatch(HashSet<ScoringModel> scores, bool isBest = false)
    {
        if (DataProvider.ScoringModels.Count == 0)
        {
            return;
        }

        var highestScore = scores.OrderByDescending(model => model.Score).FirstOrDefault();
        var nextHighestScores = scores.OrderByDescending(model => model.Score).Skip(1);

        if (highestScore is null) { return; }

        var potentialText = isBest
            ? "Best potential"
            : "Next potential";

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
        Logger.Log($"Renaming {highestScore.Definition.Name} to {highestScore.ProposedNewName}", ConsoleColor.Green);

        // Rename type and all associated type members
        RenameService.RenameAll(highestScore);

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
    }

    private void WriteAssembly()
    {
        var filename = Path.GetFileNameWithoutExtension(DataProvider.AppSettings.AssemblyPath);
        var strippedPath = Path.GetDirectoryName(filename);

        filename = $"{filename}-Remapped.dll";

        var remappedPath = Path.Combine(strippedPath, filename);

        DataProvider.AssemblyDefinition.Write(remappedPath);

        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
        Logger.Log($"Complete: Assembly written to `{remappedPath}`", ConsoleColor.Green);
        Logger.Log("-----------------------------------------------", ConsoleColor.Green);
    }
}