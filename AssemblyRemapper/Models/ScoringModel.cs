using AssemblyRemapper.Enums;
using Mono.Cecil;

namespace AssemblyRemapper.Models;

public class ScoringModel
{
    public string ProposedNewName { get; set; }
    public int Score { get; set; } = 0;
    public TypeDefinition Definition { get; set; }
    public RemapModel ReMap { get; internal set; }

    public EFailureReason FailureReason { get; set; } = EFailureReason.None;

    public ScoringModel()
    {
    }
}