using AssemblyRemapper.Enums;
using AssemblyRemapper.Utils;
using Mono.Cecil;

namespace AssemblyRemapper.Models;

internal class ScoringModel
{
    public string ProposedNewName { get; set; }
    public int Score { get; set; } = 0;
    public TypeDefinition Definition { get; set; }
    public RemapModel RemapModel { get; internal set; }

    public EFailureReason FailureReason { get; set; } = EFailureReason.None;

    public ScoringModel()
    {
    }
}

internal static class ScoringModelExtensions
{
    public static void AddModelToResult(this ScoringModel model)
    {
        try
        {
            if (DataProvider.ScoringModels.TryGetValue(model.ProposedNewName, out HashSet<ScoringModel> modelHashset))
            {
                foreach (var outVal in modelHashset)
                {
                    if (outVal.Definition.FullName == model.Definition.FullName)
                    {
                        Logger.Log("Skipping adding duplicate type match to list", ConsoleColor.Yellow);
                        return;
                    }
                }

                modelHashset.Add(model);
                return;
            }

            var newHash = new HashSet<ScoringModel>
            {
                model
            };

            DataProvider.ScoringModels.Add(model.ProposedNewName, newHash);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static int CalculateMaxScore(this ScoringModel score)
    {
        // Score should never be null here, but if it is we're fucked so just return 0.
        if (score == null) { return 0; }

        var propInfos = typeof(SearchParams).GetProperties();

        int maxScore = 0;

        foreach (var propInfo in propInfos)
        {
            object value = propInfo.GetValue(score.RemapModel.SearchParams);

            if (value == null) continue;

            if (value is List<string> list)
            {
                maxScore += list.Count;
            }
            else
            {
                maxScore++;
            }
        }

        return maxScore;
    }
}