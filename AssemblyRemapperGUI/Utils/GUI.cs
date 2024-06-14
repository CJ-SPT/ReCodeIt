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
    /// Builds the name list for the this updown
    /// </summary>
    /// <param name="domainUpDown"></param>
    /// <param name="name"></param>
    public static void BuildStringList(this DomainUpDown domainUpDown, string name)
    {
        domainUpDown.Items.Clear();
        domainUpDown.Text = name + " (Disabled)";
        domainUpDown.ReadOnly = true;

        var list = new List<string>
        {
            name + " (Disabled)",
            "True",
            "False",
        };

        domainUpDown.Items.AddRange(list);
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

        var originalTypeName = new TreeNode($"Original Name: {model.OriginalTypeName}");

        var forceRenameNode = new TreeNode($"Force Rename: {(model.UseForceRename ? model.UseForceRename : "Disabled")}");

        var ispublicNode = new TreeNode($"IsPublic: {(isPublic != null ? isPublic : "Disabled")}");

        var isAbstractNode = new TreeNode($"IsAbstract: {(isAbstract != null ? isAbstract : "Disabled")}");

        var isInterfaceNode = new TreeNode($"IsInterface: {(isInterface != null ? isInterface : "Disabled")}");

        var IsEnumNode = new TreeNode($"IsEnum: {(isEnum != null ? isEnum : "Disabled")}");

        var IsNestedNode = new TreeNode($"IsNested: {(isNested != null ? isNested : "Disabled")}");

        var IsSealedNode = new TreeNode($"IsSealed: {(IsSealed != null ? IsSealed : "Disabled")}");

        var HasAttrNode = new TreeNode($"HasAttribute: {(HasAttribute != null ? HasAttribute : "Disabled")}");

        var IsDerivedNode = new TreeNode($"IsDerived: {(IsDerived != null ? IsDerived : "Disabled")}");

        var HasGenericsNode = new TreeNode($"HasGenericParameters: {(HasGenericParameters != null ? HasGenericParameters : "Disabled")}");

        if (model.SearchParams.ConstructorParameterCount > 0)
        {
            remapTreeItem.Nodes.Add(new TreeNode($"Constructor Parameter Count: {model.SearchParams.ConstructorParameterCount}"));
        }

        if (model.SearchParams.MethodCount > 0)
        {
            remapTreeItem.Nodes.Add(new TreeNode($"Method Count: {model.SearchParams.MethodCount}"));
        }

        if (model.SearchParams.FieldCount > 0)
        {
            remapTreeItem.Nodes.Add(new TreeNode($"Field Count: {model.SearchParams.FieldCount}"));
        }

        if (model.SearchParams.PropertyCount > 0)
        {
            remapTreeItem.Nodes.Add(new TreeNode($"Property Count: {model.SearchParams.PropertyCount}"));
        }

        if (model.SearchParams.NestedTypeCount > 0)
        {
            remapTreeItem.Nodes.Add(new TreeNode($"Nested Type Count: {model.SearchParams.NestedTypeCount}"));
        }

        remapTreeItem.Nodes.Add(originalTypeName);
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

        if (model.SearchParams.IncludeMethods?.Count > 0)
        {
            var includeMethodsNode =
                GenerateNodeFromList(model.SearchParams.IncludeMethods, "Include Methods");

            remapTreeItem.Nodes.Add(includeMethodsNode);
        }

        if (model.SearchParams.ExcludeMethods?.Count > 0)
        {
            var excludeMethodsNode =
                GenerateNodeFromList(model.SearchParams.ExcludeMethods, "Exclude Methods");

            remapTreeItem.Nodes.Add(excludeMethodsNode);
        }

        if (model.SearchParams.IncludeFields?.Count > 0)
        {
            var includeFieldsNode =
                GenerateNodeFromList(model.SearchParams.IncludeFields, "Include Fields");

            remapTreeItem.Nodes.Add(includeFieldsNode);
        }

        if (model.SearchParams.ExcludeFields?.Count > 0)
        {
            var excludeFieldsNode =
                GenerateNodeFromList(model.SearchParams.ExcludeFields, "Exclude Fields");

            remapTreeItem.Nodes.Add(excludeFieldsNode);
        }

        if (model.SearchParams.IncludeProperties?.Count > 0)
        {
            var includeProperties =
                GenerateNodeFromList(model.SearchParams.IncludeProperties, "Include Properties");

            remapTreeItem.Nodes.Add(includeProperties);
        }

        if (model.SearchParams.ExcludeProperties?.Count > 0)
        {
            var excludeProperties =
                GenerateNodeFromList(model.SearchParams.ExcludeProperties, "Exclude Properties");

            remapTreeItem.Nodes.Add(excludeProperties);
        }

        if (model.SearchParams.IncludeNestedTypes?.Count > 0)
        {
            var includeNestedTypes =
                GenerateNodeFromList(model.SearchParams.IncludeNestedTypes, "Include Nested Types");

            remapTreeItem.Nodes.Add(includeNestedTypes);
        }

        if (model.SearchParams.IncludeNestedTypes?.Count > 0)
        {
            var excludeNestedTypes =
                GenerateNodeFromList(model.SearchParams.ExcludeNestedTypes, "Exclude Nested Types");

            remapTreeItem.Nodes.Add(excludeNestedTypes);
        }

        return remapTreeItem;
    }

    /// <summary>
    /// Generates a new node from a list of strings
    /// </summary>
    /// <param name="items"></param>
    /// <param name="name"></param>
    /// <returns>A new tree node, or null if the provided list is empty</returns>
    private static TreeNode GenerateNodeFromList(List<string> items, string name)
    {
        var node = new TreeNode(name);

        foreach (var item in items)
        {
            node.Nodes.Add(item);
        }

        return node;
    }

    /// <summary>
    /// Buils a list of strings from list box entries
    /// </summary>
    /// <param name="lb"></param>
    /// <returns></returns>
    public static List<string> GetAllEntriesFromListBox(ListBox lb)
    {
        var tmp = new List<string>();

        foreach (var entry in lb.Items)
        {
            tmp.Add((string)entry);
        }

        return tmp;
    }
}