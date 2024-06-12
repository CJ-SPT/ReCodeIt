using AssemblyRemapper.Utils;
using Mono.Cecil;

namespace AssemblyRemapper.Models;

internal class ScoringModel
{
    public int Score { get; set; } = 0;

    public string ProposedNewName { get; set; }

    public TypeDefinition Definition { get; set; }

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
}