using AssemblyRemapper.Reflection;
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
            if (Remapper.ScoringModels.TryGetValue(model.ProposedNewName, out HashSet<ScoringModel> modelHashset))
            {
                modelHashset.Add(model);
                return;
            }

            var newHash = new HashSet<ScoringModel>
            {
                model
            };

            Remapper.ScoringModels.Add(model.ProposedNewName, newHash);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}