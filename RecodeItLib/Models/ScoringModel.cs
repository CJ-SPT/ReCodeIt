using dnlib.DotNet;
using ReCodeIt.Enums;

namespace ReCodeIt.Models;

public class ScoringModel
{
    public string ProposedNewName { get; set; }
    public int Score { get; set; } = 0;
    public TypeDef Definition { get; set; }
    public RemapModel ReMap { get; internal set; }

    public EFailureReason FailureReason { get; set; } = EFailureReason.None;

    public ScoringModel()
    {
    }
}