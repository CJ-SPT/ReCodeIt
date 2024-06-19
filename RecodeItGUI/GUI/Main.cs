/// This entire file is fucked beyond belief, its just hacked together to make things work for now

using ReCodeIt.AutoMapper;
using ReCodeIt.CrossCompiler;
using ReCodeIt.Enums;
using ReCodeIt.Models;
using ReCodeIt.ReMapper;
using ReCodeIt.Utils;

namespace ReCodeIt.GUI;

public partial class ReCodeItForm : Form
{
    private static ReCodeItRemapper Remapper { get; set; } = new();
    private static ReCodeItAutoMapper AutoMapper { get; set; } = new();

    private static ReCodeItCrossCompiler CrossCompiler { get; set; }

    private static Settings AppSettings => DataProvider.Settings;

    private int _selectedRemapTreeIndex = 0;

    public ReCodeItForm()
    {
        InitializeComponent();

        CrossCompiler = new();

        PopulateDomainUpDowns();
        RefreshSettingsPage();
        RefreshAutoMapperPage();
        RefreshCrossCompilerPage();
        LoadMappingFile();

        RemapTreeView.NodeMouseDoubleClick += EditSelectedRemap;
        Remapper.OnComplete += ReloadTreeAfterMapping;
    }

    #region MANUAL_REMAPPER

    private void LoadMappingFile()
    {
        if (AppSettings.CrossCompiler.AutoLoadLastActiveProject
            && ActiveProjectMappingsCheckbox.Checked)
        {
            LoadedMappingFilePath.Text = $"Project Mode: ({CrossCompiler.ActiveProject.SolutionName})";

            ReloadTreeView(CrossCompiler.ActiveProject.RemapModels);

            return;
        }

        DataProvider.LoadMappingFile(AppSettings.Remapper.MappingPath);
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

        ReloadTreeView(remaps!);

        DataProvider.SaveAppSettings();
    }

    #region BUTTONS

    #region MAIN_BUTTONS

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
            .Where(remap => remap.NewTypeName == newRemap.NewTypeName)
            .FirstOrDefault();

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

            ResetAll();
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

        RemapTreeView.Nodes.Add(GUIHelpers.GenerateTreeNode(newRemap, this));
        ResetAll();
    }

    private void RemoveRemapButton_Click(object sender, EventArgs e)
    {
        if (AppSettings.Remapper.UseProjectMappings)
        {
            CrossCompiler.ActiveProject.RemapModels.RemoveAt(RemapTreeView.SelectedNode.Index);
            ProjectManager.SaveCrossCompilerProjectModel(CrossCompiler.ActiveProject);
        }
        else
        {
            DataProvider.Remaps?.RemoveAt(RemapTreeView.SelectedNode.Index);
            DataProvider.SaveMapping();
        }

        RemapTreeView.SelectedNode?.Remove();
    }

    private void EditRemapButton_Click(object sender, EventArgs e)
    {
        EditSelectedRemap(this, null);
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
                CrossCompiler.ActiveProject.RemappedAssemblyPath);

            ReloadTreeView(CrossCompiler.ActiveProject.RemapModels);
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

        ReloadTreeView(DataProvider.Remaps);
    }

    /// <summary>
    /// Only used by the manual remap process, not apart of the cross compiler process
    /// </summary>
    private void ReloadTreeAfterMapping()
    {
        ReloadTreeView(DataProvider.Remaps);
    }

    private void SaveMappingFileButton_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show(
            "Are you sure?",
            "Confirmation",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Exclamation,
            MessageBoxDefaultButton.Button2) == DialogResult.Yes)
        {
            DataProvider.SaveMapping();
        }
    }

    private void LoadMappingFileButton_Click(object sender, EventArgs e)
    {
        var result = GUIHelpers.OpenFileDialog("Select a mapping file",
                "JSON Files (*.json)|*.json|JSONC Files (*.jsonc)|*.jsonc|All Files (*.*)|*.*");

        if (result == string.Empty) { return; }

        DataProvider.LoadMappingFile(result);
        AppSettings.Remapper.MappingPath = result;
        AppSettings.Remapper.UseProjectMappings = false;
        ActiveProjectMappingsCheckbox.Checked = false;
        DataProvider.SaveAppSettings();

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
        DataProvider.SaveAppSettings();
    }

    private void SilentModeCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        DataProvider.Settings.AppSettings.SilentMode = SilentModeCheckbox.Checked;
        DataProvider.SaveAppSettings();
    }

    private void RenameFieldsCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        DataProvider.Settings.AppSettings.RenameFields = RenameFieldsCheckbox.Checked;
        DataProvider.SaveAppSettings();
    }

    private void RenamePropertiesCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        DataProvider.Settings.AppSettings.RenameProperties = RenamePropertiesCheckbox.Checked;
        DataProvider.SaveAppSettings();
    }

    private void PublicizeCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        DataProvider.Settings.Remapper.MappingSettings.Publicize = PublicizeCheckbox.Checked;
        DataProvider.SaveAppSettings();
    }

    private void UnsealCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        DataProvider.Settings.Remapper.MappingSettings.Unseal = UnsealCheckbox.Checked;
        DataProvider.SaveAppSettings();
    }

    #endregion CHECKBOXES

    #region UPDOWNS

    private void AutoMapperRequiredMatchesUpDown_ValueChanged(object sender, EventArgs e)
    {
        DataProvider.Settings.AutoMapper.RequiredMatches = (int)AutoMapperRequiredMatchesUpDown.Value;
        DataProvider.SaveAppSettings();
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
            DataProvider.SaveAppSettings();
        }
    }

    private void AutoMapperChooseOutpathButton_Click(object sender, EventArgs e)
    {
        var result = GUIHelpers.OpenFolderDialog("Select an output directory");

        if (result != string.Empty)
        {
            AppSettings.AutoMapper.OutputPath = result;
            RemapperOutputDirectoryPath.Text = result;
            DataProvider.SaveAppSettings();
        }
    }

    private void AutoMapperRequiredMatchesUpDown_ValueChanged_1(object sender, EventArgs e)
    {
        AppSettings.AutoMapper.RequiredMatches = (int)AutoMapperRequiredMatchesUpDown.Value;
        DataProvider.SaveAppSettings();
    }

    private void AutoMapperMinLengthUpDown_ValueChanged(object sender, EventArgs e)
    {
        AppSettings.AutoMapper.MinLengthToMatch = (int)AutoMapperMinLengthUpDown.Value;
        DataProvider.SaveAppSettings();
    }

    private void AutoMapperTokensAddButton_Click(object sender, EventArgs e)
    {
        if (!AutoMapperTokensBox.Items.Contains(AutoMapperTokensTextField.Text))
        {
            AutoMapperTokensBox.Items.Add(AutoMapperTokensTextField.Text);
            AppSettings.AutoMapper.TokensToMatch.Add(AutoMapperTokensTextField.Text);

            AutoMapperTokensTextField.Clear();
            DataProvider.SaveAppSettings();
        }
    }

    private void AutoMapperTokensRemoveButton_Click(object sender, EventArgs e)
    {
        if (AutoMapperTokensBox.SelectedItem != null)
        {
            AutoMapperTokensBox.Items.Remove(AutoMapperTokensBox.SelectedItem);
            AppSettings.AutoMapper.TokensToMatch.RemoveAt(AutoMapperTokensBox.SelectedIndex + 1);
            DataProvider.SaveAppSettings();
        }
    }

    private void AutoMapperFPAddButton_Click(object sender, EventArgs e)
    {
        if (!AutoMapperFPBox.Items.Contains(AutoMapperFPTextField.Text))
        {
            AutoMapperFPBox.Items.Add(AutoMapperFPTextField.Text);
            AppSettings.AutoMapper.PropertyFieldBlackList.Add(AutoMapperFPTextField.Text);

            AutoMapperFPTextField.Clear();
            DataProvider.SaveAppSettings();
        }
    }

    private void AutoMapperFPRemoveButton_Click(object sender, EventArgs e)
    {
        if (AutoMapperFPBox.SelectedItem != null)
        {
            AutoMapperFPBox.Items.Remove(AutoMapperFPBox.SelectedItem);
            AppSettings.AutoMapper.PropertyFieldBlackList.RemoveAt(AutoMapperFPBox.SelectedIndex);
            DataProvider.SaveAppSettings();
        }
    }

    private void AutoMapperMethodAddButton_Click(object sender, EventArgs e)
    {
        if (!AutoMapperMethodBox.Items.Contains(AutoMapperMethodTextBox.Text))
        {
            AutoMapperMethodBox.Items.Add(AutoMapperMethodTextBox.Text);
            AppSettings.AutoMapper.MethodParamaterBlackList.Add(AutoMapperMethodTextBox.Text);

            AutoMapperMethodTextBox.Clear();
            DataProvider.SaveAppSettings();
        }
    }

    private void AutoMapperMethodRemoveButton_Click(object sender, EventArgs e)
    {
        if (AutoMapperMethodBox.SelectedItem != null)
        {
            AutoMapperMethodBox.Items.Remove(AutoMapperMethodBox.SelectedItem);
            AppSettings.AutoMapper.MethodParamaterBlackList.RemoveAt(AutoMapperMethodBox.SelectedIndex > 0 ? AutoMapperMethodBox.SelectedIndex : 0);
            DataProvider.SaveAppSettings();
        }
    }

    private void SearchMethodsCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.AutoMapper.SearchMethods = AutoMapperSearchMethodsCheckBox.Checked;
        DataProvider.SaveAppSettings();
    }

    private void AutoMapperRenameFields_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.AutoMapper.MappingSettings.RenameFields = AutoMapperRenameFields.Checked;
        DataProvider.SaveAppSettings();
    }

    private void AutoMapperRenameProps_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.AutoMapper.MappingSettings.RenameProperties = AutoMapperRenameProps.Checked;
        DataProvider.SaveAppSettings();
    }

    private void AutoMapperPublicize_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.AutoMapper.MappingSettings.Publicize = AutoMapperPublicize.Checked;
        DataProvider.SaveAppSettings();
    }

    private void AutoMapperUnseal_CheckedChanged(object sender, EventArgs e)
    {
        AppSettings.AutoMapper.MappingSettings.Unseal = AutoMapperUnseal.Checked;
        DataProvider.SaveAppSettings();
    }

    #endregion AUTOMAPPER

    #region CROSS_COMPILER

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
        CCRemappedOutputText.Text = activeProj.RemappedAssemblyPath;
        CCVisualStudioProjDirText.Text = activeProj.VisualStudioSolutionPath;
        CCBuildDirText.Text = activeProj.BuildDirectory;
    }

    private void CCOriginalAssemblyButton_Click(object sender, EventArgs e)
    {
        var result = GUIHelpers.OpenFileDialog("Select a DLL file",
            "DLL Files (*.dll)|*.dll|All Files (*.*)|*.*");

        if (result != string.Empty)
        {
            CCOriginalAssemblyText.Text = result;
            DataProvider.SaveAppSettings();
        }
    }

    private void CCRemappedOutputButton_Click(object sender, EventArgs e)
    {
        var result = GUIHelpers.OpenFolderDialog("Select a Folder for the remapped reference dll");

        if (result != string.Empty)
        {
            CCRemappedOutputText.Text = result;
            DataProvider.SaveAppSettings();
        }
    }

    private void CCVisualStudioProjDirButton_Click(object sender, EventArgs e)
    {
        var result = GUIHelpers.OpenFileDialog("Select a Visual Studio solution file",
            "Solution Files (*.sln)|*.sln|All Files (*.*)|*.*");

        if (result != string.Empty)
        {
            CCVisualStudioProjDirText.Text = result;
            DataProvider.SaveAppSettings();
        }
    }

    private void CCBuildDirButton_Click(object sender, EventArgs e)
    {
        var result = GUIHelpers.OpenFolderDialog("Select where you want to final dll built at");

        if (result != string.Empty)
        {
            CCBuildDirText.Text = result;
            DataProvider.SaveAppSettings();
        }
    }

    private void CrossPatchRemapButton_Click(object sender, EventArgs e)
    {
        CrossCompiler.StartRemap();
    }

    private void CrossPatchRunButton_Click(object sender, EventArgs e)
    {
        CrossCompiler.StartCrossCompile();
    }

    private void CrossCompilerNewProjectButton_Click(object sender, EventArgs e)
    {
        if (CCOriginalAssemblyText.Text == string.Empty
            || CCRemappedOutputText.Text == string.Empty
            || CCVisualStudioProjDirText.Text == string.Empty
            || CCBuildDirText.Text == string.Empty)
        {
            // Dont create a project if any required fields are empty
            MessageBox.Show("Cannot create a project without setting all paths in the project settings");
            return;
        }

        ProjectManager.CreateProject(
            CCOriginalAssemblyText.Text,
            CCRemappedOutputText.Text,
            CCVisualStudioProjDirText.Text,
            CCBuildDirText.Text);
    }

    private void CCLoadProjButton_Click(object sender, EventArgs e)
    {
        var result = GUIHelpers.OpenFileDialog("select a ReCodeItProj.json File",
               "JSON Files (*.json)|*.json|JSONC Files (*.jsonc)|*.jsonc|All Files (*.*)|*.*");

        if (result != string.Empty)
        {
            ProjectManager.LoadProject(result);
        }
    }

    private void CCAutoLoadLastProj_CheckedChanged_1(object sender, EventArgs e)
    {
        AppSettings.CrossCompiler.AutoLoadLastActiveProject = CCAutoLoadLastProj.Checked;
        DataProvider.SaveAppSettings();
    }

    // Use the projects remap list on the remap tab
    private void ActiveProjectMappingsCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        // TODO
    }

    #endregion CROSS_COMPILER

    // Reset All UI elements to default
    private void ResetAll()
    {
        PopulateDomainUpDowns();

        // Text fields

        NewTypeName.Clear();
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

    private void EditSelectedRemap(object? sender, TreeNodeMouseClickEventArgs e)
    {
        if (e?.Node.Level != 0 || RemapTreeView?.SelectedNode?.Index < 0 || RemapTreeView?.SelectedNode?.Index == null)
        {
            return;
        }

        _selectedRemapTreeIndex = RemapTreeView.SelectedNode.Index;

        ResetAll();

        var remap = AppSettings.Remapper.UseProjectMappings
            ? CrossCompiler.ActiveProject.RemapModels.ElementAt(_selectedRemapTreeIndex)
            : DataProvider.Remaps.ElementAt(_selectedRemapTreeIndex);

        NewTypeName.Text = remap.NewTypeName;
        OriginalTypeName.Text = remap.OriginalTypeName;
        RemapperUseForceRename.Checked = remap.UseForceRename;

        BaseClassIncludeTextFIeld.Text = remap.SearchParams.MatchBaseClass;
        BaseClassExcludeTextField.Text = remap.SearchParams.IgnoreBaseClass;
        NestedTypeParentName.Text = remap.SearchParams.ParentName;

        ConstructorCountEnabled.Checked = remap.SearchParams.ConstructorParameterCount != null ? remap.SearchParams.ConstructorParameterCount > 0 : false;
        MethodCountEnabled.Checked = remap.SearchParams.MethodCount != null ? remap.SearchParams.MethodCount > 0 : false;
        FieldCountEnabled.Checked = remap.SearchParams.FieldCount != null ? remap.SearchParams.FieldCount > 0 : false;
        PropertyCountEnabled.Checked = remap.SearchParams.PropertyCount != null ? remap.SearchParams.PropertyCount > 0 : false;
        NestedTypeCountEnabled.Checked = remap.SearchParams.NestedTypeCount != null ? remap.SearchParams.NestedTypeCount > 0 : false;

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

    #region TAB_REFRESH

    private void AutoMapperTab_Click(object sender, EventArgs e)
    {
        RefreshAutoMapperPage();
    }

    private void tabPage5_Click(object sender, EventArgs e)
    {
        RefreshCrossCompilerPage();
    }

    private void SettingsTab_Click(object sender, EventArgs e)
    {
        RefreshSettingsPage();
    }

    #endregion TAB_REFRESH

    /// <summary>
    /// Subscribes the the remappers OnComplete event
    /// </summary>
    /// <param name="remaps"></param>
    private void ReloadTreeView(List<RemapModel> remaps)
    {
        RemapTreeView.Nodes.Clear();

        foreach (var remap in remaps)
        {
            RemapTreeView.Nodes.Add(GUIHelpers.GenerateTreeNode(remap, this));
        }
    }
}