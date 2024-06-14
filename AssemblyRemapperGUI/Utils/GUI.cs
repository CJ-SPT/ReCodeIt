using AssemblyRemapper.Models;

namespace RemapperGUI.Utils;

internal static class GUI
{
    /// <summary>
    /// Returns the value of the count or null if disabled
    /// </summary>
    /// <param name="box"></param>
    /// <returns></returns>
    public static int? GetCount(this CheckBox box, NumericUpDown upDown)
    {
        if (box.Checked)
        {
            return (int?)upDown.Value;
        }

        return null;
    }

    public static bool? GetEnabled(this DomainUpDown domainUpDown)
    {
        if (domainUpDown.Text == "True")
        {
            return true;
        }
        else if (domainUpDown.Text == "False")
        {
            return false;
        }

        return null;
    }

    /// <summary>
    /// Generates a tree node to display on the GUI
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static TreeNode GenerateTreeNode(RemapModel model)
    {
        var isPublic = model.SearchParams.IsPublic == null ? null : model.SearchParams.IsPublic;
        var isAbstract = model.SearchParams.IsAbstract == null ? null : model.SearchParams.IsAbstract;
        var isInterface = model.SearchParams.IsInterface == null ? null : model.SearchParams.IsInterface;
        var isEnum = model.SearchParams.IsEnum == null ? null : model.SearchParams.IsEnum;
        var isNested = model.SearchParams.IsNested == null ? null : model.SearchParams.IsNested;
        var IsSealed = model.SearchParams.IsSealed == null ? null : model.SearchParams.IsSealed;
        var HasAttribute = model.SearchParams.HasAttribute == null ? null : model.SearchParams.HasAttribute;
        var IsDerived = model.SearchParams.IsDerived == null ? null : model.SearchParams.IsDerived;
        var HasGenericParameters = model.SearchParams.HasGenericParameters == null ? null : model.SearchParams.HasGenericParameters;

        var remapTreeItem = new TreeNode($"Remap: {model.NewTypeName}");

        var forceRenameNode = new TreeNode($"Force Rename: {model.UseForceRename}");
        var ispublicNode = new TreeNode($"IsPublic: {isPublic}");
        var isAbstractNode = new TreeNode($"IsAbstract: {isAbstract}");
        var isInterfaceNode = new TreeNode($"IsInterface: {isInterface}");
        var IsEnumNode = new TreeNode($"IsEnum: {isEnum}");
        var IsNestedNode = new TreeNode($"IsNested: {isNested}");
        var IsSealedNode = new TreeNode($"IsSealed: {IsSealed}");
        var HasAttrNode = new TreeNode($"HasAttribute: {HasAttribute}");
        var IsDerivedNode = new TreeNode($"IsDerived: {IsDerived}");
        var HasGenericsNode = new TreeNode($"HasGenericParameters: {HasGenericParameters}");

        remapTreeItem.Nodes.Add(forceRenameNode);
        remapTreeItem.Nodes.Add(ispublicNode);
        remapTreeItem.Nodes.Add(isAbstractNode);
        remapTreeItem.Nodes.Add(isInterfaceNode);
        remapTreeItem.Nodes.Add(IsEnumNode);
        remapTreeItem.Nodes.Add(IsNestedNode);
        remapTreeItem.Nodes.Add(IsSealedNode);
        remapTreeItem.Nodes.Add(HasAttrNode);
        remapTreeItem.Nodes.Add(IsDerivedNode);
        remapTreeItem.Nodes.Add(HasGenericsNode);

        return remapTreeItem;
    }
}