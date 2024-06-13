using AssemblyRemapper.Models;

namespace AssemblyRemapper.Utils;

internal static class ExtentionMethods
{
    public static void AddScoreToResult(this ScoringModel model)
    {
        try
        {
            if (DataProvider.ScoringModels.TryGetValue(model.ProposedNewName, out HashSet<ScoringModel> modelHashset))
            {
                foreach (var outVal in modelHashset)
                {
                    if (outVal.Definition.Name == model.Definition.Name)
                    {
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

    public static int CalculateMaxScore(this SearchParams parms)
    {
        var propInfos = typeof(SearchParams).GetProperties();

        int maxScore = 0;

        foreach (var propInfo in propInfos)
        {
            object value = propInfo.GetValue(parms);

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