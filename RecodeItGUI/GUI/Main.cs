/// This entire file is fucked beyond belief, its just hacked together to make things work for now

using ReCodeIt.AutoMapper;
using ReCodeIt.CrossCompiler;
using ReCodeIt.Enums;
using ReCodeIt.Models;
using ReCodeIt.ReMapper;
using ReCodeIt.Utils;
using System.Diagnostics;

namespace ReCodeIt.GUI;

public partial class ReCodeItForm : Form
{
    private static ReCodeItRemapper Remapper { get; set; } = new();
    private static ReCodeItAutoMapper AutoMapper { get; set; } = new();

    private static ReCodeItCrossCompiler CrossCompiler { get; set; }

    private static Settings AppSettings => DataProvider.Settings;

    private bool _isSearched = false;
    public static Dictionary<TreeNode, RemapModel> RemapNodes = [];

    private int _selectedRemapTreeIndex = 0;
    private int _selectedCCRemapTreeIndex = 0;

    private List<string> _cachedNewTypeNames = [];

    public ReCodeItForm()
    {
        InitializeComponent();

        CrossCompiler = new();

        SubscribeToEvents();
        PopulateDomainUpDowns();
        RefreshSettingsPage();
        RefreshAutoMapperPage();
        RefreshCrossCompilerPage();
        LoadMappingFile();

        var remaps = AppSettings.Remapper.UseProjectMappings
           ? CrossCompiler.ActiveProject?.RemapModels
           : DataProvider.Remaps;

        ReloadRemapTreeView(remaps);
    }

    private void SubscribeToEvents()
    {
        RemapTreeView.NodeMouseDoubleClick += ManualEditSelectedRemap;
        CCMappingTreeView.NodeMouseDoubleClick += CCEditSelectedRemap;
        Remapper.OnComplete += ReloadTreeAfterMapping;

        #region MANUAL_REMAPPER

        NewTypeName.GotFocus += (sender, e) =>
        {
            _cachedNewTypeNames.Add(NewTypeName.Text);
        };

        IncludeMethodTextBox.KeyDown += (sender, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                MethodIncludeAddButton_Click(sender, e);
            }
        };

        ExcludeMethodTextBox.KeyDown += (sender, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                MethodExcludeAddButton_Click(sender, e);
            }
        };

        FieldsIncludeTextInput.KeyDown += (sender, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                FIeldIncludeAddButton_Click(sender, e);
            }
        };

        FieldsExcludeTextInput.KeyDown += (sender, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                FieldExcludeAddButton_Click(sender, e);
            }
        };

        PropertiesIncludeTextField.KeyDown += (sender, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                PropertiesIncludeAddButton_Click(sender, e);
            }
        };

        PropertiesExcludeTextField.KeyDown += (sender, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                PropertiesExcludeAddButton_Click(sender, e);
            }
        };

        NestedTypesIncludeTextField.KeyDown += (sender, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                NestedTypesAddButton_Click(sender, e);
            }
        };

        NestedTypesExcludeTextField.KeyDown += (sender, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                NestedTypesExlcudeAddButton_Click(sender, e);
            }
        };

        #endregion MANUAL_REMAPPER

        #region AUTOMAPPER

        AutoMapperTypesToIgnoreTextField.KeyDown += (sender, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                AutoMapperExcludeAddButton_Click(sender, e);
            }
        };

        AutoMapperTokensTextField.KeyDown += (sender, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                AutoMapperTokensAddButton_Click(sender, e);
            }
        };

        AutoMapperFPTextField.KeyDown += (sender, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                AutoMapperFPAddButton_Click(sender, e);
            }
        };

        AutoMapperMethodTextBox.KeyDown += (sender, e) =>
        {
            if (e.KeyCode == Keys.Enter)
            {
                AutoMapperMethodAddButton_Click(sender, e);
            }
        };

        #endregion AUTOMAPPER
    }

    #region MANUAL_REMAPPER

    private void LoadMappingFile()
    {
        if (AppSettings.CrossCompiler.AutoLoadLastActiveProject
            && ActiveProjectMappingsCheckbox.Checked)
        {
            if (CrossCompiler.ActiveProject == null)
            {
                DataProvider.Remaps = DataProvider.LoadMappingFile(AppSettings.Remapper.MappingPath);
                LoadedMappingFilePath.Text = AppSettings.Remapper.MappingPath;
                return;
            }

            LoadedMappingFilePath.Text = $"Project Mode: ({CrossCompiler.ActiveProject.SolutionName})";

            ReloadRemapTreeView(CrossCompiler.ActiveProject.RemapModels);

            return;
        }

        DataProvider.Remaps = DataProvider.LoadMappingFile(AppSettings.Remapper.MappingPath);
        LoadedMappingFilePath.Text = AppSettings.Remapper.MappingPath;
    }

    private void UseProjectAutoMapping_Clicked(object sender, EventArgs e)
    {
        bool enabled = ActiveProjectMappingsCheckbox.Checked;

        AppSettings.Remapper.UseProjectMappings = enabled;

        var remaps = enabled && CrossCompiler?.ActiveProject?.RemapModels != null
            ? CrossCompiler?.ActiveProject?.RemapModels
            : DataProvider.Remaps;

        if (enabled && CrossCompiler?.ActiveProject != null)
        {
            LoadedMappingFilePath.Text = $"Project Mode: ({CrossCompiler.ActiveProject.SolutionName})";
        }
        else
        {
            LoadedMappingFilePath.Text = AppSettings.Remapper?.MappingPath;
        }

        ReloadRemapTreeView(remaps!);
    }

    #region BUTTONS

    #region MAIN_BUTTONS

    private void SearchTreeView(object sender, EventArgs e)
    {
        if (RemapTreeView.Nodes.Count == 0) { return; }
        if (RMSearchBox.Text == string.Empty) { return; }

        bool projectMode = AppSettings.Remapper.UseProjectMappings;

        var remaps = projectMode
            ? CrossCompiler.ActiveProject.RemapModels
            : DataProvider.Remaps;

        var matches = remaps
            .Where(x => x.NewTypeName == RMSearchBox.Text
            || x.NewTypeName.StartsWith(RMSearchBox.Text));

        if (!matches.Any()) { return; }

        RemapTreeView.Nodes.Clear();

        foreach (var match in matches)
        {
            RemapTreeView.Nodes.Add(GUIHelpers.GenerateTreeNode(match, this));
        }

        _isSearched = true;
    }

    private void ResetSearchButton_Click(object sender, EventArgs e)
    {
        bool projectMode = AppSettings.Remapper.UseProjectMappings;

        var remaps = projectMode
            ? CrossCompiler.ActiveProject.RemapModels
            : DataProvider.Remaps;

        RemapTreeView.Nodes.Clear();
        ReloadRemapTreeView(remaps);

        RMSearchBox.Clear();
        _isSearched = false;
    }

    /// <summary>
    /// Construct a new remap when the button is pressed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddRemapButton_Click(object sender, EventArgs e)
    {
        if (NewTypeName.Text == string.Empty)
        {
            MessageBox.Show("Please enter a new type name", "Invalid data");
            return;
        }

        var newRemap = new RemapModel
        {
            Succeeded = false,
            FailureReason = EFailureReason.None,
            NewTypeName = NewTypeName.Text,
            OriginalTypeName = OriginalTypeName.Text == string.Empty ? null : OriginalTypeName.Text,
            UseForceRename = RemapperUseForceRename.Checked,
            SearchParams = new SearchParams
            {
                IsPublic = IsPublicUpDown.GetEnabled(),
                IsAbstract = IsAbstractUpDown.GetEnabled(),
                IsInterface = IsInterfaceUpDown.GetEnabled(),
                IsEnum = IsEnumUpDown.GetEnabled(),
                IsNested = IsNestedUpDown.GetEnabled(),
                IsSealed = IsSealedUpDown.GetEnabled(),
                HasAttribute = HasAttributeUpDown.GetEnabled(),
                IsDerived = IsDerivedUpDown.GetEnabled(),
                HasGenericParameters = HasGenericParametersUpDown.GetEnabled(),

                ParentName = NestedTypeParentName.Text == string.Empty
                ? null
                : NestedTypeParentName.Text,

                MatchBaseClass = BaseClassIncludeTextFIeld.Text == string.Empty
                ? null
                : BaseClassIncludeTextFIeld.Text,

                IgnoreBaseClass = BaseClassExcludeTextField.Text == string.Empty
                ? null
                : BaseClassExcludeTextField.Text,

                // Constructor - TODO
                ConstructorParameterCount = ConstructorCountEnabled.GetCount(ConstuctorCountUpDown),
                MethodCount = MethodCountEnabled.GetCount(MethodCountUpDown),
                FieldCount = FieldCountEnabled.GetCount(FieldCountUpDown),
                PropertyCount = PropertyCountEnabled.GetCount(PropertyCountUpDown),
                NestedTypeCount = NestedTypeCountEnabled.GetCount(NestedTypeCountUpDown),
                IncludeMethods = GUIHelpers.GetAllEntriesFromListBox(MethodIncludeBox),
                ExcludeMethods = GUIHelpers.GetAllEntriesFromListBox(MethodExcludeBox),
                IncludeFields = GUIHelpers.GetAllEntriesFromListBox(FieldIncludeBox),
                ExcludeFields = GUIHelpers.GetAllEntriesFromListBox(FieldExcludeBox),
                IncludeProperties = GUIHelpers.GetAllEntriesFromListBox(PropertiesIncludeBox),
                ExcludeProperties = GUIHelpers.GetAllEntriesFromListBox(PropertiesExcludeBox),
                IncludeNestedTypes = GUIHelpers.GetAllEntriesFromListBox(NestedTypesIncludeBox),
                ExcludeNestedTypes = GUIHelpers.GetAllEntriesFromListBox(NestedTypesExcludeBox),
            }
        };

        bool projectMode = AppSettings.Remapper.UseProjectMappings;

        Logger.Log(projectMode);

        var remaps = projectMode
            ? CrossCompiler.ActiveProject.RemapModels
            : DataProvider.Remaps;

        var existingRemap = remaps
            .FirstOrDefault(remap => remap.NewTypeName == newRemap.NewTypeName);

        if (existingRemap == null)
        {
            existingRemap = remaps
                .FirstOrDefault(remap => _cachedNewTypeNames.Contains(remap.NewTypeName));
        }

        // Handle overwriting an existing remap
        if (existingRemap != null)
        {
            var index = remaps.IndexOf(existingRemap);

            remaps.Remove(existingRemap);
            RemapTreeView.Nodes.RemoveAt(index);

            remaps.Insert(index, newRemap);
            RemapTreeView.Nodes.Insert(index, GUIHelpers.GenerateTreeNode(newRemap, this));

            if (projectMode)
            {
                ProjectManager.SaveCrossCompilerProjectModel(CrossCompiler.ActiveProject);
            }
            else
            {
                DataProvider.SaveMapping();
            }

            ReloadRemapTreeView(remaps);

            ResetAllRemapFields();
            return;
        }

        if (projectMode)
        {
            CrossCompiler.ActiveProject.RemapModels.Add(newRemap);
            ProjectManager.SaveCrossCompilerProjectModel(CrossCompiler.ActiveProject);
        }
        else
        {
            DataProvider.Remaps.Add(newRemap);
            DataProvider.SaveMapping();
        }

        var node = GUIHelpers.GenerateTreeNode(newRemap, this);

        node.Clone();

        //RemapTreeView.Nodes.Remove(node);
        RemapTreeView.Nodes.Add(node);

        _cachedNewTypeNames.Clear();

        ReloadRemapTreeView(remaps);

        ResetAllRemapFields();
    }

    private void RemoveRemapButton_Click(object sender, EventArgs e)
    {
        foreach (var node in RemapNodes.ToArray())
        {
            if (node.Key == RemapTreeView.SelectedNode)
            {
                bool projectMode = AppSettings.Remapper.UseProjectMappings;

                var remaps = projectMode
                    ? CrossCompiler.ActiveProject.RemapModels
                    : DataProvider.Remaps;

                remaps.Remove(node.Value);
                RemapNodes.Remove(node.Key);
                RemapTreeView.Nodes.Remove(node.Key);
            }
        }

        ResetAllRemapFields();

        if (AppSettings.Remapper.UseProjectMappings)
        {
            ProjectManager.SaveCrossCompilerProjectModel(CrossCompiler.ActiveProject);
            return;
        }

        DataProvider.SaveMapping();
    }

    private void RunRemapButton_Click(object sender, EventArgs e)
    {
        if (ReCodeItRemapper.IsRunning) { return; }

        if (AppSettings.Remapper.UseProjectMappings)
        {
            Remapper.InitializeRemap(
                CrossCompiler.ActiveProject.RemapModels,
                CrossCompiler.ActiveProject.OriginalAssemblyPath,
                CrossCompiler.ActiveProject.VisualStudioDependencyPath);

            ReloadRemapTreeView(CrossCompiler.ActiveProject.RemapModels);
            return;
        }

        if (string.IsNullOrEmpty(AppSettings.Remapper.AssemblyPath))
        {
            MessageBox.Show("Please select an assembly path", "Assembly not loaded");
            return;
        }

        Remapper.InitializeRemap(
            DataProvider.Remaps,
            AppSettings.Remapper.AssemblyPath,
            AppSettings.Remapper.OutputPath);

        ReloadRemapTreeView(DataProvider.Remaps);
    }

    /// <summary>
    /// Only used by the manual remap process, not apart of the cross compiler process
    /// </summary>
    private void ReloadTreeAfterMapping()
    {
        ReloadRemapTreeView(DataProvider.Remaps);
    }

    private void SaveMappingFileButton_Click(object sender, EventArgs e)
    {
        DataProvider.SaveMapping();
    }

    private void LoadMappingFileButton_Click(object sender, EventArgs e)
    {
        var result = GUIHelpers.OpenFileDialog("Select a mapping file",
                "JSON Files (*.json)|*.json|JSONC Files (*.jsonc)|*.jsonc|All Files (*.*)|*.*");

        if (result == string.Empty) { return; }

        DataProvider.Remaps = DataProvider.LoadMappingFile(result);
        AppSettings.Remapper.MappingPath = result;
        AppSettings.Remapper.UseProjectMappings = false;
        ActiveProjectMappingsCheckbox.Checked = false;

        LoadedMappingFilePath.Text = result;

        RemapTreeView.Nodes.Clear();

        foreach (var remap in DataProvider.Remaps)
        {
            RemapTreeView.Nodes.Add(GUIHelpers.GenerateTreeNode(remap, this));
        }
    }

    private void PickAssemblyPathButton_Click_1(object sender, EventArgs e)
    {
        var result = GUIHelpers.OpenFileDialog("Select a DLL file",
                "DLL Files (*.dll)|*.dll|All Files (*.*)|*.*");

        if (result != string.Empty)
        {
            AppSettings.Remapper.AssemblyPath = result;
            TargetAssemblyPath.Text = result;
        }
    }

    private void OutputDirectoryButton_Click_1(object sender, EventArgs e)
    {
        var result = GUIHelpers.OpenFolderDialog("Select an output directory");

        if (result != string.Empty)
        {
            AppSettings.Remapper.OutputPath = result;
            RemapperOutputDirectoryPath.Text = result;
        }
    }

    private void RunAutoMapButton_Click(object sender, EventArgs e)
    {
        AutoMapper.InitializeAutoMapping();
    }

    #endregion MAIN_BUTTONS

    #region LISTBOX_BUTTONS

    private void MethodIncludeAddButton_Click(object sender, EventArgs e)
    {
        if (!MethodIncludeBox.Items.Contains(IncludeMethodTextBox.Text))
        {
            MethodIncludeBox.Items.Add(IncludeMethodTextBox.Text);
            IncludeMethodTextBox.Clear();
        }
    }

    private void MethodIncludeRemoveButton_Click(object sender, EventArgs e)
    {
        if (MethodIncludeBox.SelectedItem != null)
        {
            MethodIncludeBox.Items.Remove(MethodIncludeBox.SelectedItem);
        }
    }

    private void MethodExcludeAddButton_Click(object sender, EventArgs e)
    {
        if (!MethodExcludeBox.Items.Contains(ExcludeMethodTextBox.Text))
        {
            MethodExcludeBox.Items.Add(ExcludeMethodTextBox.Text);
            ExcludeMethodTextBox.Clear();
        }
    }

    private void MethodExcludeRemoveButton_Click(object sender, EventArgs e)
    {
        if (MethodExcludeBox.SelectedItem != null)
        {
            MethodExcludeBox.Items.Remove(MethodExcludeBox.SelectedItem);
        }
    }

    private void FIeldIncludeAddButton_Click(object sender, EventArgs e)
    {
        if (!FieldIncludeBox.Items.Contains(FieldsIncludeTextInput.Text))
        {
            FieldIncludeBox.Items.Add(FieldsIncludeTextInput.Text);
            FieldsIncludeTextInput.Clear();
        }
    }

    private void FieldIncludeRemoveButton_Click(object sender, EventArgs e)
    {
        if (FieldIncludeBox.SelectedItem != null)
        {
            FieldIncludeBox.Items.Remove(FieldIncludeBox.SelectedItem);
        }
    }

    private void FieldExcludeAddButton_Click(object sender, EventArgs e)
    {
        if (!FieldExcludeBox.Items.Contains(FieldsExcludeTextInput.Text))
        {
            FieldExcludeBox.Items.Add(FieldsExcludeTextInput.Text);
            FieldsExcludeTextInput.Clear();
        }
    }

    private void FieldExcludeRemoveButton_Click(object sender, EventArgs e)
    {
        if (FieldExcludeBox.SelectedItem != null)
        {
            FieldExcludeBox.Items.Remove(FieldExcludeBox.SelectedItem);
        }
    }

    private void PropertiesIncludeAddButton_Click(object sender, EventArgs e)
    {
        if (!PropertiesIncludeBox.Items.Contains(PropertiesIncludeTextField.Text))
        {
            PropertiesIncludeBox.Items.Add(PropertiesIncludeTextField.Text);
            PropertiesIncludeTextField.Clear();
        }
    }

    private void PropertiesIncludeRemoveButton_Click(object sender, EventArgs e)
    {
        if (PropertiesIncludeBox.SelectedItem != null)
        {
            PropertiesIncludeBox.Items.Remove(PropertiesIncludeBox.SelectedItem);
        }
    }

    private void PropertiesExcludeAddButton_Click(object sender, EventArgs e)
    {
        if (!PropertiesExcludeBox.Items.Contains(PropertiesExcludeTextField.Text))
        {
            PropertiesExcludeBox.Items.Add(PropertiesExcludeTextField.Text);
            PropertiesExcludeTextField.Clear();
        }
    }

    private void PropertiesExcludeRemoveButton_Click(object sender, EventArgs e)
    {
        if (PropertiesExcludeBox.SelectedItem != null)
        {
            PropertiesExcludeBox.Items.Remove(PropertiesExcludeBox.SelectedItem);
        }
    }

    private void NestedTypesAddButton_Click(object sender, EventArgs e)
    {
        if (!NestedTypesIncludeBox.Items.Contains(NestedTypesIncludeTextField.Text))
        {
            NestedTypesIncludeBox.Items.Add(NestedTypesIncludeTextField.Text);
            NestedTypesIncludeTextField.Clear();
        }
    }

    private void NestedTypesRemoveButton_Click(object sender, EventArgs e)
    {
        if (NestedTypesIncludeBox.SelectedItem != null)
        {
            NestedTypesIncludeBox.Items.Remove(NestedTypesIncludeBox.SelectedItem);
        }
    }

    private void NestedTypesExlcudeAddButton_Click(object sender, EventArgs e)
    {
        if (!NestedTypesExcludeBox.Items.Contains(NestedTypesExcludeTextField.Text))
        {
            NestedTypesExcludeBox.Items.Add(NestedTypesExcludeTextField.Text);
            NestedTypesExcludeTextField.Clear();
        }
    }

    private void NestedTypesExcludeRemoveButton_Click(object sender, EventArgs e)
    {
        if (NestedTypesExcludeBox.SelectedItem != null)
        {
            NestedTypesExcludeBox.Items.Remove(NestedTypesExcludeBox.SelectedItem);
        }
    }

    private void AutoMapperExcludeAddButton_Click(object sender, EventArgs e)
    {
        if (!AutoMapperTypesExcludeBox.Items.Contains(AutoMapperTypesToIgnoreTextField.Text))
        {
            DataProvider.Settings.AutoMapper.TypesToIgnore.Add(AutoMapperTypesToIgnoreTextField.Text);
            AutoMapperTypesExcludeBox.Items.Add(AutoMapperTypesToIgnoreTextField.Text);
            AutoMapperTypesToIgnoreTextField.Clear();

            DataProvider.SaveAppSettings();
        }
    }

    private void AutoMapperExcludeRemoveButton_Click(object sender, EventArgs e)
    {
        if (AutoMapperTypesExcludeBox.SelectedItem != null)
        {
            DataProvider.Settings.AutoMapper.TypesToIgnore.RemoveAt(AutoMapperTypesExcludeBox.SelectedIndex);
            AutoMapperTypesExcludeBox.Items.Remove(AutoMapperTypesExcludeBox.SelectedItem);

            DataProvider.SaveAppSettings();
        }
    }

    private void RunAutoRemapButton_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(DataProvider.Settings.AutoMapper.AssemblyPath))
        {
            MessageBox.Show("Please go to the settings tab and load an assembly and select and output location", "Assembly not loaded");
            return;
        }

        AutoMapper.InitializeAutoMapping();
    }

    #endregion LISTBOX_BUTTONS

    #region CHECKBOX

    private void RemapperUnseal_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.Remapper.MappingSettings.Unseal = RemapperUnseal.Checked;
    }

    private void RemapperPublicicize_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.Remapper.MappingSettings.Publicize = RemapperPublicicize.Checked;
    }

    private void RenameFieldsCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.Remapper.MappingSettings.RenameFields = RenameFieldsCheckbox.Checked;
    }

    private void RenamePropertiesCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.Remapper.MappingSettings.RenameProperties = RenamePropertiesCheckbox.Checked;
    }

    #endregion CHECKBOX

    #endregion BUTTONS

    #endregion MANUAL_REMAPPER

    #region SETTINGS_TAB

    public void RefreshSettingsPage()
    {
        // Settings page
        DebugLoggingCheckbox.Checked = AppSettings.AppSettings.Debug;
        SilentModeCheckbox.Checked = AppSettings.AppSettings.SilentMode;

        // Remapper page
        TargetAssemblyPath.Text = AppSettings.Remapper.AssemblyPath;
        RemapperOutputDirectoryPath.Text = AppSettings.Remapper.OutputPath;
        RenameFieldsCheckbox.Checked = AppSettings.Remapper.MappingSettings.RenameFields;
        RenamePropertiesCheckbox.Checked = AppSettings.Remapper.MappingSettings.RenameProperties;
        RemapperPublicicize.Checked = AppSettings.Remapper.MappingSettings.Publicize;
        RemapperUnseal.Checked = AppSettings.Remapper.MappingSettings.Unseal;
        ActiveProjectMappingsCheckbox.Checked = AppSettings.Remapper.UseProjectMappings;
    }

    #region CHECKBOXES

    private void DebugLoggingCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        DataProvider.Settings.AppSettings.Debug = DebugLoggingCheckbox.Checked;
    }

    private void SilentModeCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        DataProvider.Settings.AppSettings.SilentMode = SilentModeCheckbox.Checked;
    }

    #endregion CHECKBOXES

    #region UPDOWNS

    private void AutoMapperRequiredMatchesUpDown_ValueChanged(object sender, EventArgs e)
    {
        DataProvider.Settings.AutoMapper.RequiredMatches = (int)AutoMapperRequiredMatchesUpDown.Value;
    }

    #endregion UPDOWNS

    #endregion SETTINGS_TAB

    #region AUTOMAPPER

    public void RefreshAutoMapperPage()
    {
        AutoMapperTypesExcludeBox.Items.Clear();
        AutoMapperTokensBox.Items.Clear();
        AutoMapperFPBox.Items.Clear();

        var settings = AppSettings.AutoMapper;

        AutoMapperRequiredMatchesUpDown.Value = settings.RequiredMatches;
        AutoMapperMinLengthUpDown.Value = settings.MinLengthToMatch;
        AutoMapperSearchMethodsCheckBox.Checked = settings.SearchMethods;
        AutoMapperRenameFields.Checked = settings.MappingSettings.RenameFields;
        AutoMapperRenameProps.Checked = settings.MappingSettings.RenameProperties;
        AutoMapperPublicize.Checked = settings.MappingSettings.Publicize;
        AutoMapperUnseal.Checked = settings.MappingSettings.Unseal;

        foreach (var type in settings.TypesToIgnore)
        {
            AutoMapperTypesExcludeBox.Items.Add(type);
        }

        foreach (var token in settings.TokensToMatch)
        {
            AutoMapperTokensBox.Items.Add(token);
        }

        foreach (var fp in settings.PropertyFieldBlackList)
        {
            AutoMapperFPBox.Items.Add(fp);
        }

        foreach (var mp in settings.MethodParamaterBlackList)
        {
            AutoMapperMethodBox.Items.Add(mp);
        }
    }

    private void AutoMapperChooseTargetPathButton_Click(object sender, EventArgs e)
    {
        var result = GUIHelpers.OpenFileDialog("Select a DLL file",
            "DLL Files (*.dll)|*.dll|All Files (*.*)|*.*");

        if (result != string.Empty)
        {
            AppSettings.AutoMapper.AssemblyPath = result;
            TargetAssemblyPath.Text = result;
        }
    }

    private void AutoMapperChooseOutpathButton_Click(object sender, EventArgs e)
    {
        var result = GUIHelpers.OpenFolderDialog("Select an output directory");

        if (result != string.Empty)
        {
            AppSettings.AutoMapper.OutputPath = result;
            RemapperOutputDirectoryPath.Text = result;
        }
    }

    private void AutoMapperRequiredMatchesUpDown_ValueChanged_1(object sender, EventArgs e)
    {
        AppSettings.AutoMapper.RequiredMatches = (int)AutoMapperRequiredMatchesUpDown.Value;
    }

    private void AutoMapperMinLengthUpDown_ValueChanged(object sender, EventArgs e)
    {
        AppSettings.AutoMapper.MinLengthToMatch = (int)AutoMapperMinLengthUpDown.Value;
    }

    private void AutoMapperTokensAddButton_Click(object sender, EventArgs e)
    {
        if (!AutoMapperTokensBox.Items.Contains(AutoMapperTokensTextField.Text))
        {
            AutoMapperTokensBox.Items.Add(AutoMapperTokensTextField.Text);
            AppSettings.AutoMapper.TokensToMatch.Add(AutoMapperTokensTextField.Text);

            DataProvider.SaveAppSettings();
            AutoMapperTokensTextField.Clear();
        }
    }

    private void AutoMapperTokensRemoveButton_Click(object sender, EventArgs e)
    {
        if (AutoMapperTokensBox.SelectedItem != null)
        {
            AppSettings.AutoMapper.TokensToMatch.RemoveAt(AutoMapperTokensBox.SelectedIndex);
            AutoMapperTokensBox.Items.Remove(AutoMapperTokensBox.SelectedItem);
            DataProvider.SaveAppSettings();
        }
    }

    private void AutoMapperFPAddButton_Click(object sender, EventArgs e)
    {
        if (!AutoMapperFPBox.Items.Contains(AutoMapperFPTextField.Text))
        {
            AutoMapperFPBox.Items.Add(AutoMapperFPTextField.Text);
            AppSettings.AutoMapper.PropertyFieldBlackList.Add(AutoMapperFPTextField.Text);

            DataProvider.SaveAppSettings();
            AutoMapperFPTextField.Clear();
        }
    }

    private void AutoMapperFPRemoveButton_Click(object sender, EventArgs e)
    {
        if (AutoMapperFPBox.SelectedItem != null)
        {
            AppSettings.AutoMapper.PropertyFieldBlackList.RemoveAt(AutoMapperFPBox.SelectedIndex);
            AutoMapperFPBox.Items.Remove(AutoMapperFPBox.SelectedItem);

            DataProvider.SaveAppSettings();
        }
    }

    private void AutoMapperMethodAddButton_Click(object sender, EventArgs e)
    {
        if (!AutoMapperMethodBox.Items.Contains(AutoMapperMethodTextBox.Text))
        {
            AutoMapperMethodBox.Items.Add(AutoMapperMethodTextBox.Text);

            AppSettings.AutoMapper.MethodParamaterBlackList.Add(AutoMapperMethodTextBox.Text);

            DataProvider.SaveAppSettings();
            AutoMapperMethodTextBox.Clear();
        }
    }

    private void AutoMapperMethodRemoveButton_Click(object sender, EventArgs e)
    {
        if (AutoMapperMethodBox.SelectedItem != null)
        {
            AppSettings.AutoMapper.MethodParamaterBlackList
                .RemoveAt(AutoMapperMethodBox.SelectedIndex);

            AutoMapperMethodBox.Items
                .Remove(AutoMapperMethodBox.SelectedItem);

            DataProvider.SaveAppSettings();
        }
    }

    private void SearchMethodsCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.AutoMapper.SearchMethods = AutoMapperSearchMethodsCheckBox.Checked;
    }

    private void AutoMapperRenameFields_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.AutoMapper.MappingSettings.RenameFields = AutoMapperRenameFields.Checked;
    }

    private void AutoMapperRenameProps_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.AutoMapper.MappingSettings.RenameProperties = AutoMapperRenameProps.Checked;
    }

    private void AutoMapperPublicize_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.AutoMapper.MappingSettings.Publicize = AutoMapperPublicize.Checked;
    }

    private void AutoMapperUnseal_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.AutoMapper.MappingSettings.Unseal = AutoMapperUnseal.Checked;
    }

    #endregion AUTOMAPPER

    #region CROSS_COMPILER

    private void OnCCTabPageValidate(object sender, EventArgs e)
    {
        RefreshCrossCompilerPage();
    }

    private void RefreshCrossCompilerPage()
    {
        var ccSettings = AppSettings.CrossCompiler;

        CCAutoLoadLastProj.Checked = ccSettings.AutoLoadLastActiveProject;

        if (ccSettings.AutoLoadLastActiveProject)
        {
            // Dont continue if its an empty string, it hasnt been set yet
            if (ccSettings.LastLoadedProject == string.Empty)
            {
                return;
            }

            if (!File.Exists(ccSettings.LastLoadedProject))
            {
                ccSettings.LastLoadedProject = string.Empty;
                MessageBox.Show("Couldnt find last loaded project");
                return;
            }

            ProjectManager.LoadProject(ccSettings.LastLoadedProject);
        }

        if (CrossCompiler.ActiveProject == null)
        {
            return;
        }

        var activeProj = CrossCompiler.ActiveProject;

        CCOriginalAssemblyText.Text = activeProj.OriginalAssemblyPath;
        CCProjectDepdendencyText.Text = activeProj.VisualStudioDependencyPath;
        CCVisualStudioProjDirText.Text = activeProj.VisualStudioSolutionPath;
        CCBuildDirText.Text = activeProj.BuildDirectory;

        ReloadCCRemapTreeView(activeProj.RemapModels);
    }

    private void CCOriginalAssemblyButton_Click(object sender, EventArgs e)
    {
        var result = GUIHelpers.OpenFileDialog("Select a DLL file",
            "DLL Files (*.dll)|*.dll|All Files (*.*)|*.*");

        if (result != string.Empty)
        {
            CCOriginalAssemblyText.Text = result;
        }
    }

    private void CCProjectDependencyButton_Click(object sender, EventArgs e)
    {
        var result = GUIHelpers.OpenFolderDialog("Select your projects reference folder, this is where the Re-Mapped output will be placed as well.");

        if (result != string.Empty)
        {
            CCProjectDepdendencyText.Text = result;
        }
    }

    private void CCVisualStudioProjDirButton_Click(object sender, EventArgs e)
    {
        var result = GUIHelpers.OpenFileDialog("Select a Visual Studio solution file",
            "Solution Files (*.sln)|*.sln|All Files (*.*)|*.*");

        if (result != string.Empty)
        {
            CCVisualStudioProjDirText.Text = result;
        }
    }

    private void CCBuildDirButton_Click(object sender, EventArgs e)
    {
        var result = GUIHelpers.OpenFolderDialog("Select where you want to final dll built at");

        if (result != string.Empty)
        {
            CCBuildDirText.Text = result;
        }
    }

    private void CrossPatchRemapButton_Click(object sender, EventArgs e)
    {
        if (CrossCompiler.ActiveProject.RemapModels.Count == 0)
        {
            MessageBox.Show("You cannot generate a remapped dll without creating remaps first");
            return;
        }

        CrossCompiler.StartRemap();
    }

    private void CrossPatchRunButton_Click(object sender, EventArgs e)
    {
        if (CrossCompiler.ActiveProject.RemapModels.Count == 0)
        {
            MessageBox.Show("You cannot compile without having created remaps first");
            return;
        }

        CrossCompiler.StartCrossCompile();
    }

    private void CrossCompilerNewProjectButton_Click(object sender, EventArgs e)
    {
        if (CCOriginalAssemblyText.Text == string.Empty
            || CCProjectDepdendencyText.Text == string.Empty
            || CCVisualStudioProjDirText.Text == string.Empty
            || CCBuildDirText.Text == string.Empty)
        {
            // Dont create a project if any required fields are empty
            MessageBox.Show("Cannot create a project without setting all paths in the project settings");
            return;
        }

        ProjectManager.CreateProject(
            CCOriginalAssemblyText.Text,
            CCVisualStudioProjDirText.Text,
            CCProjectDepdendencyText.Text,
            CCBuildDirText.Text);
    }

    private void CCLoadProjButton_Click(object sender, EventArgs e)
    {
        var result = GUIHelpers.OpenFileDialog("select a ReCodeItProj.json File",
               "JSON Files (*.json)|*.json|JSONC Files (*.jsonc)|*.jsonc|All Files (*.*)|*.*");

        if (result != string.Empty)
        {
            ProjectManager.LoadProject(result);
            ReloadCCRemapTreeView(ProjectManager.ActiveProject.RemapModels);
        }
    }

    private void CCAutoLoadLastProj_CheckedChanged_1(object sender, EventArgs e)
    {
        AppSettings.CrossCompiler.AutoLoadLastActiveProject = CCAutoLoadLastProj.Checked;
    }

    // Use the projects remap list on the remap tab
    private void ActiveProjectMappingsCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        // TODO
    }

    private void CCImportMappings_Click(object sender, EventArgs e)
    {
        if (CrossCompiler.ActiveProject == null)
        {
            MessageBox.Show("No project is loaded to add mappings too.");
            return;
        }

        var answer = MessageBox.Show(
            "'Yes' to Add the items to the existing list, or 'No' to clear the list before adding these.",
            "Add Items to existing list?",
            MessageBoxButtons.YesNoCancel);

        switch (answer)
        {
            case DialogResult.Yes:
                break;

            case DialogResult.No:
                CrossCompiler.ActiveProject.RemapModels.Clear();
                break;

            case DialogResult.Cancel:
                return;

            default:
                break;
        }

        var result = GUIHelpers.OpenFileDialog("Select a mapping file",
            "JSON Files (*.json)|*.json|JSONC Files (*.jsonc)|*.jsonc|All Files (*.*)|*.*");

        if (result == string.Empty) { return; }

        var remaps = DataProvider.LoadMappingFile(result);

        CrossCompiler.ActiveProject.RemapModels.AddRange(remaps);

        ReloadCCRemapTreeView(CrossCompiler.ActiveProject.RemapModels);
    }

    #endregion CROSS_COMPILER

    // Reset All UI elements to default
    private void ResetAllRemapFields()
    {
        PopulateDomainUpDowns();

        // Text fields

        NewTypeName.Clear();
        OriginalTypeName.Clear();
        BaseClassIncludeTextFIeld.Clear();
        BaseClassExcludeTextField.Clear();
        NestedTypeParentName.Clear();
        BaseClassExcludeTextField.Clear();
        IncludeMethodTextBox.Clear();
        ExcludeMethodTextBox.Clear();
        FieldsIncludeTextInput.Clear();
        FieldsExcludeTextInput.Clear();
        PropertiesIncludeTextField.Clear();
        PropertiesExcludeTextField.Clear();
        NestedTypesIncludeTextField.Clear();
        NestedTypesExcludeTextField.Clear();

        // Numeric UpDowns

        ConstuctorCountUpDown.Value = 0;
        MethodCountUpDown.Value = 0;
        FieldCountUpDown.Value = 0;
        PropertyCountUpDown.Value = 0;
        NestedTypeCountUpDown.Value = 0;

        // Check boxes

        RemapperUseForceRename.Checked = false;
        ConstructorCountEnabled.Checked = false;
        MethodCountEnabled.Checked = false;
        FieldCountEnabled.Checked = false;
        PropertyCountEnabled.Checked = false;
        NestedTypeCountEnabled.Checked = false;

        // List boxes

        MethodIncludeBox.Items.Clear();
        MethodExcludeBox.Items.Clear();
        FieldIncludeBox.Items.Clear();
        FieldExcludeBox.Items.Clear();
        PropertiesIncludeBox.Items.Clear();
        PropertiesExcludeBox.Items.Clear();
        NestedTypesIncludeBox.Items.Clear();
        NestedTypesExcludeBox.Items.Clear();
    }

    private void CCEditSelectedRemap(object? sender, TreeNodeMouseClickEventArgs e)
    {
        if (e?.Node.Level != 0 || CCMappingTreeView?.SelectedNode?.Index < 0 || CCMappingTreeView?.SelectedNode?.Index == null)
        {
            return;
        }

        _selectedCCRemapTreeIndex = CCMappingTreeView.SelectedNode.Index;

        // Go to remapper page
        TabControlMain.SelectedIndex = 0;

        DataProvider.Settings.Remapper.UseProjectMappings = true;
        ActiveProjectMappingsCheckbox.Checked = true;

        EditSelectedRemap(this, e, true);
    }

    private void ManualEditSelectedRemap(object? sender, TreeNodeMouseClickEventArgs e)
    {
        EditSelectedRemap(this, e);
    }

    private void EditSelectedRemap(
        object? sender,
        TreeNodeMouseClickEventArgs e,
        bool isComingFromOtherTab = false)
    {
        if (e?.Node.Level != 0 || RemapTreeView?.SelectedNode?.Index < 0 || RemapTreeView?.SelectedNode?.Index == null)
        {
            return;
        }

        RemapModel remap = null;

        foreach (var node in RemapNodes.ToArray())
        {
            if (node.Key == RemapTreeView.SelectedNode)
            {
                bool projectMode = AppSettings.Remapper.UseProjectMappings;

                var remaps = projectMode
                    ? CrossCompiler.ActiveProject.RemapModels
                    : DataProvider.Remaps;

                remap = remaps.FirstOrDefault(x => x.NewTypeName == node.Value.NewTypeName);

                break;
            }
        }

        if (remap == null)
        {
            return;
        }

        _selectedRemapTreeIndex = RemapTreeView.SelectedNode.Index;

        ResetAllRemapFields();

        NewTypeName.Text = remap.NewTypeName;
        OriginalTypeName.Text = remap.OriginalTypeName;
        RemapperUseForceRename.Checked = remap.UseForceRename;

        BaseClassIncludeTextFIeld.Text = remap.SearchParams.MatchBaseClass;
        BaseClassExcludeTextField.Text = remap.SearchParams.IgnoreBaseClass;
        NestedTypeParentName.Text = remap.SearchParams.ParentName;

        ConstructorCountEnabled.Checked = remap.SearchParams.ConstructorParameterCount != null ? remap.SearchParams.ConstructorParameterCount > 0 : false;
        MethodCountEnabled.Checked = remap.SearchParams.MethodCount != null ? remap.SearchParams.MethodCount >= 0 : false;
        FieldCountEnabled.Checked = remap.SearchParams.FieldCount != null ? remap.SearchParams.FieldCount >= 0 : false;
        PropertyCountEnabled.Checked = remap.SearchParams.PropertyCount != null ? remap.SearchParams.PropertyCount >= 0 : false;
        NestedTypeCountEnabled.Checked = remap.SearchParams.NestedTypeCount != null ? remap.SearchParams.NestedTypeCount >= 0 : false;

        ConstuctorCountUpDown.Value = (decimal)((remap.SearchParams.ConstructorParameterCount != null ? remap.SearchParams.ConstructorParameterCount : 0));
        MethodCountUpDown.Value = (decimal)(remap.SearchParams.MethodCount != null ? remap.SearchParams.MethodCount : 0);
        FieldCountUpDown.Value = (decimal)(remap.SearchParams.FieldCount != null ? remap.SearchParams.FieldCount : 0);
        PropertyCountUpDown.Value = (decimal)(remap.SearchParams.PropertyCount != null ? remap.SearchParams.PropertyCount : 0);
        NestedTypeCountUpDown.Value = (decimal)(remap.SearchParams.NestedTypeCount != null ? remap.SearchParams.NestedTypeCount : 0);

        IsPublicUpDown.BuildStringList("IsPublic", remap.SearchParams.IsPublic);
        IsAbstractUpDown.BuildStringList("IsAbstract", remap.SearchParams.IsAbstract);
        IsInterfaceUpDown.BuildStringList("IsInterface", remap.SearchParams.IsInterface);
        IsEnumUpDown.BuildStringList("IsEnum", remap.SearchParams.IsEnum);
        IsNestedUpDown.BuildStringList("IsNested", remap.SearchParams.IsNested);
        IsSealedUpDown.BuildStringList("IsSealed", remap.SearchParams.IsSealed);
        HasAttributeUpDown.BuildStringList("HasAttribute", remap.SearchParams.HasAttribute);
        IsDerivedUpDown.BuildStringList("IsDerived", remap.SearchParams.IsDerived);
        HasGenericParametersUpDown.BuildStringList("HasGenericParams", remap.SearchParams.HasGenericParameters);

        foreach (var method in remap.SearchParams.IncludeMethods)
        {
            MethodIncludeBox.Items.Add(method);
        }

        foreach (var method in remap.SearchParams.ExcludeMethods)
        {
            MethodExcludeBox.Items.Add(method);
        }

        foreach (var method in remap.SearchParams.IncludeFields)
        {
            FieldIncludeBox.Items.Add(method);
        }

        foreach (var method in remap.SearchParams.ExcludeFields)
        {
            FieldExcludeBox.Items.Add(method);
        }

        foreach (var method in remap.SearchParams.IncludeProperties)
        {
            PropertiesIncludeBox.Items.Add(method);
        }

        foreach (var method in remap.SearchParams.ExcludeProperties)
        {
            PropertiesExcludeBox.Items.Add(method);
        }

        foreach (var method in remap.SearchParams.IncludeNestedTypes)
        {
            NestedTypesIncludeBox.Items.Add(method);
        }

        foreach (var method in remap.SearchParams.ExcludeNestedTypes)
        {
            NestedTypesExcludeBox.Items.Add(method);
        }
    }

    private void PopulateDomainUpDowns()
    {
        // Clear them all first just incase
        IsPublicUpDown.BuildStringList("IsPublic");
        IsAbstractUpDown.BuildStringList("IsAbstract");
        IsInterfaceUpDown.BuildStringList("IsInterface");
        IsEnumUpDown.BuildStringList("IsEnum");
        IsNestedUpDown.BuildStringList("IsNested");
        IsSealedUpDown.BuildStringList("IsSealed");
        HasAttributeUpDown.BuildStringList("HasAttribute");
        IsDerivedUpDown.BuildStringList("IsDerived");
        HasGenericParametersUpDown.BuildStringList("HasGenericParams");
    }

    /// <summary>
    /// Subscribes the the remappers OnComplete event
    /// </summary>
    /// <param name="remaps"></param>
    private void ReloadRemapTreeView(List<RemapModel>? remaps)
    {
        RemapTreeView.Nodes.Clear();
        RemapNodes.Clear();

        if (remaps is null)
        {
            return;
        }

        foreach (var remap in remaps)
        {
            RemapTreeView.Nodes.Add(GUIHelpers.GenerateTreeNode(remap, this));
        }
    }

    private void ReloadCCRemapTreeView(List<RemapModel> remaps)
    {
        CCMappingTreeView.Nodes.Clear();
        RemapNodes.Clear();

        foreach (var remap in remaps)
        {
            CCMappingTreeView.Nodes.Add(GUIHelpers.GenerateTreeNode(remap, this));
        }
    }

    private void GithubLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = GithubLinkLabel.Text,
            UseShellExecute = true
        });
    }
}