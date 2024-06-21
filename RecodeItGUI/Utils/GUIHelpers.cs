using ReCodeIt.Models;

namespace ReCodeIt.GUI;

internal static class GUIHelpers
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
    public static void BuildStringList(this DomainUpDown domainUpDown, string name, bool? update = null)
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

        if (update != null)
        {
            domainUpDown.Text = update.ToString();
        }

        domainUpDown.Items.AddRange(list);
    }

    /// <summary>
    /// Generates a tree node to display on the GUI
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static TreeNode GenerateTreeNode(RemapModel model, ReCodeItForm gui)
    {
        var isPublic = model.SearchParams.IsPublic == null ? null : model.SearchParams.IsPublic;
        var isAbstract = model.SearchParams.IsAbstract == null ? null : model.SearchParams.IsAbstract;
        var isInterface = model.SearchParams.IsInterface == null ? null : model.SearchParams.IsInterface;
        var isEnum = model.SearchParams.IsEnum == null ? null : model.SearchParams.IsEnum;
        var isNested = model.SearchParams.IsNested == null ? null : model.SearchParams.IsNested;
        var isSealed = model.SearchParams.IsSealed == null ? null : model.SearchParams.IsSealed;
        var HasAttribute = model.SearchParams.HasAttribute == null ? null : model.SearchParams.HasAttribute;
        var IsDerived = model.SearchParams.IsDerived == null ? null : model.SearchParams.IsDerived;
        var HasGenericParameters = model.SearchParams.HasGenericParameters == null ? null : model.SearchParams.HasGenericParameters;

        var remapTreeItem = new TreeNode($"{model.NewTypeName}");

        var originalTypeName = new TreeNode($"Original Name: {model.OriginalTypeName}");

        remapTreeItem.Nodes.Add(originalTypeName);

        if (model.UseForceRename)
        {
            remapTreeItem.Nodes.Add(new TreeNode($"Force Rename: {model.UseForceRename}"));
        }

        if (isPublic is not null)
        {
            remapTreeItem.Nodes.Add(new TreeNode($"IsPublic: {isPublic}"));
        }

        if (isAbstract is not null)
        {
            remapTreeItem.Nodes.Add(new TreeNode($"IsAbstract: {isAbstract}"));
        }

        if (isInterface is not null)
        {
            remapTreeItem.Nodes.Add(new TreeNode($"IsInterface: {isInterface}"));
        }

        if (isEnum is not null)
        {
            remapTreeItem.Nodes.Add(new TreeNode($"isEnum: {isEnum}"));
        }

        if (isNested is not null)
        {
            remapTreeItem.Nodes.Add(new TreeNode($"IsNested: {isEnum}"));
        }

        if (isSealed is not null)
        {
            remapTreeItem.Nodes.Add(new TreeNode($"IsSealed: {isSealed}"));
        }

        if (HasAttribute is not null)
        {
            remapTreeItem.Nodes.Add(new TreeNode($"HasAttribute: {HasAttribute}"));
        }

        if (IsDerived is not null)
        {
            remapTreeItem.Nodes.Add(new TreeNode($"IsDerived: {IsDerived}"));
        }

        if (HasGenericParameters is not null)
        {
            remapTreeItem.Nodes.Add(new TreeNode($"HasGenericParameters: {HasGenericParameters}"));
        }

        if (model.SearchParams.ConstructorParameterCount > 0)
        {
            remapTreeItem.Nodes.Add(new TreeNode($"Constructor Parameter Count: {model.SearchParams.ConstructorParameterCount}"));
        }

        if (model.SearchParams.MethodCount is not null)
        {
            remapTreeItem.Nodes.Add(new TreeNode($"Method Count: {model.SearchParams.MethodCount}"));
        }

        if (model.SearchParams.FieldCount is not null)
        {
            remapTreeItem.Nodes.Add(new TreeNode($"Field Count: {model.SearchParams.FieldCount}"));
        }

        if (model.SearchParams.PropertyCount is not null)
        {
            remapTreeItem.Nodes.Add(new TreeNode($"Property Count: {model.SearchParams.PropertyCount}"));
        }

        if (model.SearchParams.NestedTypeCount is not null)
        {
            remapTreeItem.Nodes.Add(new TreeNode($"Nested OriginalTypeRef Count: {model.SearchParams.NestedTypeCount}"));
        }

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

    /// <summary>
    /// Opens and returns a path from a file dialogue
    /// </summary>
    /// <param name="title"></param>
    /// <param name="filter"></param>
    /// <returns>Path if selected, or empty string</returns>
    public static string OpenFileDialog(string title, string filter)
    {
        OpenFileDialog fDialog = new()
        {
            Title = title,
            Filter = filter,
            Multiselect = false
        };

        if (fDialog.ShowDialog() == DialogResult.OK)
        {
            return fDialog.FileName;
        }

        return string.Empty;
    }

    /// <summary>
    /// Opens and returns a path from a folder dialogue
    /// </summary>
    /// <param name="description"></param>
    /// <returns>Path if selected, or empty string</returns>
    public static string OpenFolderDialog(string description)
    {
        using FolderBrowserDialog fDialog = new();

        fDialog.Description = description;
        fDialog.ShowNewFolderButton = true;

        if (fDialog.ShowDialog() == DialogResult.OK)
        {
            return fDialog.SelectedPath;
        }

        return string.Empty;
    }
}