namespace ReCodeIt.GUI;

partial class ReCodeItForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        TabPageRemapper = new TabPage();
        RemapTreeView = new TreeView();
        groupBox1 = new GroupBox();
        ActiveProjectMappingsCheckbox = new CheckBox();
        LoadedMappingFilePath = new TextBox();
        RemapperUseForceRename = new CheckBox();
        RemapperUnseal = new CheckBox();
        RemapperPublicicize = new CheckBox();
        RemapperOutputDirectoryPath = new TextBox();
        TargetAssemblyPath = new TextBox();
        RenamePropertiesCheckbox = new CheckBox();
        OutputDirectoryButton = new Button();
        PickAssemblyPathButton = new Button();
        ConstuctorCountUpDown = new NumericUpDown();
        RenameFieldsCheckbox = new CheckBox();
        ConstructorCountEnabled = new CheckBox();
        LoadMappingFileButton = new Button();
        RunRemapButton = new Button();
        Inclusions = new TabControl();
        tabPage1 = new TabPage();
        ExcludeMethodTextBox = new TextBox();
        IncludeMethodTextBox = new TextBox();
        MethodExcludeRemoveButton = new Button();
        MethodExcludeAddButton = new Button();
        MethodIncludeRemoveButton = new Button();
        MethodIncludeAddButton = new Button();
        MethodExcludeBox = new ListBox();
        MethodIncludeBox = new ListBox();
        tabPage2 = new TabPage();
        FieldsExcludeTextInput = new TextBox();
        FieldsIncludeTextInput = new TextBox();
        FieldExcludeRemoveButton = new Button();
        FieldExcludeAddButton = new Button();
        FieldIncludeRemoveButton = new Button();
        FIeldIncludeAddButton = new Button();
        FieldExcludeBox = new ListBox();
        FieldIncludeBox = new ListBox();
        tabPage3 = new TabPage();
        PropertiesExcludeTextField = new TextBox();
        PropertiesIncludeTextField = new TextBox();
        PropertiesExcludeRemoveButton = new Button();
        PropertiesExcludeAddButton = new Button();
        PropertiesIncludeRemoveButton = new Button();
        PropertiesIncludeAddButton = new Button();
        PropertiesExcludeBox = new ListBox();
        PropertiesIncludeBox = new ListBox();
        tabPage4 = new TabPage();
        NestedTypesExcludeTextField = new TextBox();
        NestedTypesIncludeTextField = new TextBox();
        NestedTypesExcludeRemoveButton = new Button();
        NestedTypesExlcudeAddButton = new Button();
        NestedTypesRemoveButton = new Button();
        NestedTypesAddButton = new Button();
        NestedTypesExcludeBox = new ListBox();
        NestedTypesIncludeBox = new ListBox();
        NewTypeName = new TextBox();
        PropertyCountEnabled = new CheckBox();
        IsInterfaceUpDown = new DomainUpDown();
        NestedTypeCountEnabled = new CheckBox();
        PropertyCountUpDown = new NumericUpDown();
        RemoveRemapButton = new Button();
        FieldCountUpDown = new NumericUpDown();
        IsPublicUpDown = new DomainUpDown();
        FieldCountEnabled = new CheckBox();
        NestedTypeParentName = new TextBox();
        MethodCountUpDown = new NumericUpDown();
        IsAbstractUpDown = new DomainUpDown();
        BaseClassIncludeTextFIeld = new TextBox();
        OriginalTypeName = new TextBox();
        HasGenericParametersUpDown = new DomainUpDown();
        IsEnumUpDown = new DomainUpDown();
        NestedTypeCountUpDown = new NumericUpDown();
        SaveRemapButton = new Button();
        IsDerivedUpDown = new DomainUpDown();
        IsNestedUpDown = new DomainUpDown();
        HasAttributeUpDown = new DomainUpDown();
        BaseClassExcludeTextField = new TextBox();
        MethodCountEnabled = new CheckBox();
        IsSealedUpDown = new DomainUpDown();
        TabControlMain = new TabControl();
        AutoMapperTab = new TabPage();
        AutoMapperUnseal = new CheckBox();
        AutoMapperPublicize = new CheckBox();
        AutoMapperRenameProps = new CheckBox();
        AutoMapperRenameFields = new CheckBox();
        AutoMapperOuputPath = new TextBox();
        AutoMapperTargetPath = new TextBox();
        AutoMapperChooseOutpathButton = new Button();
        AutoMapperChooseTargetPathButton = new Button();
        AutoMapperMethodTextBox = new TextBox();
        AutoMapperMethodRemoveButton = new Button();
        AutoMapperMethodAddButton = new Button();
        AutoMapperMethodBox = new ListBox();
        AutoMapperSearchMethodsCheckBox = new CheckBox();
        AutoMapperFPTextField = new TextBox();
        AutoMapperFPRemoveButton = new Button();
        AutoMapperFPAddButton = new Button();
        AutoMapperFPBox = new ListBox();
        AutoMapperTokensTextField = new TextBox();
        AutoMapperTokensRemoveButton = new Button();
        AutoMapperTokensAddButton = new Button();
        AutoMapperTokensBox = new ListBox();
        label3 = new Label();
        AutoMapperMinLengthUpDown = new NumericUpDown();
        RunAutoRemapButton = new Button();
        label2 = new Label();
        AutoMapperRequiredMatchesUpDown = new NumericUpDown();
        AutoMapperTypesToIgnoreTextField = new TextBox();
        AutoMapperExcludeTypesRemoveButton = new Button();
        AutoMapperExcludeTypesAddButton = new Button();
        AutoMapperTypesExcludeBox = new ListBox();
        tabPage5 = new TabPage();
        groupBox4 = new GroupBox();
        groupBox5 = new GroupBox();
        CCBuildConfiguration = new TextBox();
        CrossPatchRemapButton = new Button();
        CrossPatchRunButton = new Button();
        label4 = new Label();
        CCMappingTreeView = new TreeView();
        groupBox3 = new GroupBox();
        CCImportMappings = new Button();
        CCAutoLoadLastProj = new CheckBox();
        CCLoadProjButton = new Button();
        CCRemappedOutputButton = new Button();
        CCRemappedOutputText = new TextBox();
        CCOriginalAssemblyText = new TextBox();
        CCVisualStudioProjDirText = new TextBox();
        CrossCompilerNewProjectButton = new Button();
        CCVisualStudioProjDirButton = new Button();
        CCBuildDirButton = new Button();
        CCOriginalAssemblyButton = new Button();
        CCBuildDirText = new TextBox();
        SettingsTab = new TabPage();
        groupBox2 = new GroupBox();
        GithubLinkLabel = new LinkLabel();
        SilentModeCheckbox = new CheckBox();
        DebugLoggingCheckbox = new CheckBox();
        TabPageRemapper.SuspendLayout();
        groupBox1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)ConstuctorCountUpDown).BeginInit();
        Inclusions.SuspendLayout();
        tabPage1.SuspendLayout();
        tabPage2.SuspendLayout();
        tabPage3.SuspendLayout();
        tabPage4.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)PropertyCountUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)FieldCountUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)MethodCountUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)NestedTypeCountUpDown).BeginInit();
        TabControlMain.SuspendLayout();
        AutoMapperTab.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)AutoMapperMinLengthUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)AutoMapperRequiredMatchesUpDown).BeginInit();
        tabPage5.SuspendLayout();
        groupBox4.SuspendLayout();
        groupBox5.SuspendLayout();
        groupBox3.SuspendLayout();
        SettingsTab.SuspendLayout();
        groupBox2.SuspendLayout();
        SuspendLayout();
        // 
        // TabPageRemapper
        // 
        TabPageRemapper.BackColor = SystemColors.ControlDarkDark;
        TabPageRemapper.Controls.Add(RemapTreeView);
        TabPageRemapper.Controls.Add(groupBox1);
        TabPageRemapper.Location = new Point(4, 34);
        TabPageRemapper.Name = "TabPageRemapper";
        TabPageRemapper.Padding = new Padding(3);
        TabPageRemapper.Size = new Size(1336, 953);
        TabPageRemapper.TabIndex = 1;
        TabPageRemapper.Text = "Remapper";
        // 
        // RemapTreeView
        // 
        RemapTreeView.BackColor = Color.Gray;
        RemapTreeView.Location = new Point(781, 20);
        RemapTreeView.Name = "RemapTreeView";
        RemapTreeView.Size = new Size(487, 890);
        RemapTreeView.TabIndex = 1;
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(ActiveProjectMappingsCheckbox);
        groupBox1.Controls.Add(LoadedMappingFilePath);
        groupBox1.Controls.Add(RemapperUseForceRename);
        groupBox1.Controls.Add(RemapperUnseal);
        groupBox1.Controls.Add(RemapperPublicicize);
        groupBox1.Controls.Add(RemapperOutputDirectoryPath);
        groupBox1.Controls.Add(TargetAssemblyPath);
        groupBox1.Controls.Add(RenamePropertiesCheckbox);
        groupBox1.Controls.Add(OutputDirectoryButton);
        groupBox1.Controls.Add(PickAssemblyPathButton);
        groupBox1.Controls.Add(ConstuctorCountUpDown);
        groupBox1.Controls.Add(RenameFieldsCheckbox);
        groupBox1.Controls.Add(ConstructorCountEnabled);
        groupBox1.Controls.Add(LoadMappingFileButton);
        groupBox1.Controls.Add(RunRemapButton);
        groupBox1.Controls.Add(Inclusions);
        groupBox1.Controls.Add(NewTypeName);
        groupBox1.Controls.Add(PropertyCountEnabled);
        groupBox1.Controls.Add(IsInterfaceUpDown);
        groupBox1.Controls.Add(NestedTypeCountEnabled);
        groupBox1.Controls.Add(PropertyCountUpDown);
        groupBox1.Controls.Add(RemoveRemapButton);
        groupBox1.Controls.Add(FieldCountUpDown);
        groupBox1.Controls.Add(IsPublicUpDown);
        groupBox1.Controls.Add(FieldCountEnabled);
        groupBox1.Controls.Add(NestedTypeParentName);
        groupBox1.Controls.Add(MethodCountUpDown);
        groupBox1.Controls.Add(IsAbstractUpDown);
        groupBox1.Controls.Add(BaseClassIncludeTextFIeld);
        groupBox1.Controls.Add(OriginalTypeName);
        groupBox1.Controls.Add(HasGenericParametersUpDown);
        groupBox1.Controls.Add(IsEnumUpDown);
        groupBox1.Controls.Add(NestedTypeCountUpDown);
        groupBox1.Controls.Add(SaveRemapButton);
        groupBox1.Controls.Add(IsDerivedUpDown);
        groupBox1.Controls.Add(IsNestedUpDown);
        groupBox1.Controls.Add(HasAttributeUpDown);
        groupBox1.Controls.Add(BaseClassExcludeTextField);
        groupBox1.Controls.Add(MethodCountEnabled);
        groupBox1.Controls.Add(IsSealedUpDown);
        groupBox1.Location = new Point(6, 6);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(769, 904);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "Remap Editor";
        // 
        // ActiveProjectMappingsCheckbox
        // 
        ActiveProjectMappingsCheckbox.AutoSize = true;
        ActiveProjectMappingsCheckbox.Location = new Point(448, 479);
        ActiveProjectMappingsCheckbox.Name = "ActiveProjectMappingsCheckbox";
        ActiveProjectMappingsCheckbox.Size = new Size(264, 29);
        ActiveProjectMappingsCheckbox.TabIndex = 39;
        ActiveProjectMappingsCheckbox.Text = "Use Active Project Mappings";
        ActiveProjectMappingsCheckbox.UseVisualStyleBackColor = true;
        ActiveProjectMappingsCheckbox.CheckedChanged += UseProjectAutoMapping_Clicked;
        // 
        // LoadedMappingFilePath
        // 
        LoadedMappingFilePath.BackColor = SystemColors.ScrollBar;
        LoadedMappingFilePath.Location = new Point(448, 514);
        LoadedMappingFilePath.Name = "LoadedMappingFilePath";
        LoadedMappingFilePath.PlaceholderText = "Loaded Mapping File";
        LoadedMappingFilePath.ReadOnly = true;
        LoadedMappingFilePath.Size = new Size(297, 31);
        LoadedMappingFilePath.TabIndex = 38;
        // 
        // RemapperUseForceRename
        // 
        RemapperUseForceRename.AutoSize = true;
        RemapperUseForceRename.Checked = true;
        RemapperUseForceRename.CheckState = CheckState.Checked;
        RemapperUseForceRename.Location = new Point(577, 190);
        RemapperUseForceRename.Name = "RemapperUseForceRename";
        RemapperUseForceRename.Size = new Size(149, 29);
        RemapperUseForceRename.TabIndex = 37;
        RemapperUseForceRename.Text = "Force Rename";
        RemapperUseForceRename.UseVisualStyleBackColor = true;
        // 
        // RemapperUnseal
        // 
        RemapperUnseal.AutoSize = true;
        RemapperUnseal.Checked = true;
        RemapperUnseal.CheckState = CheckState.Checked;
        RemapperUnseal.Location = new Point(577, 330);
        RemapperUnseal.Name = "RemapperUnseal";
        RemapperUnseal.Size = new Size(90, 29);
        RemapperUnseal.TabIndex = 36;
        RemapperUnseal.Text = "Unseal";
        RemapperUnseal.UseVisualStyleBackColor = true;
        // 
        // RemapperPublicicize
        // 
        RemapperPublicicize.AutoSize = true;
        RemapperPublicicize.Checked = true;
        RemapperPublicicize.CheckState = CheckState.Checked;
        RemapperPublicicize.Location = new Point(577, 295);
        RemapperPublicicize.Name = "RemapperPublicicize";
        RemapperPublicicize.Size = new Size(106, 29);
        RemapperPublicicize.TabIndex = 35;
        RemapperPublicicize.Text = "Publicize";
        RemapperPublicicize.UseVisualStyleBackColor = true;
        // 
        // RemapperOutputDirectoryPath
        // 
        RemapperOutputDirectoryPath.BackColor = SystemColors.ScrollBar;
        RemapperOutputDirectoryPath.Location = new Point(10, 70);
        RemapperOutputDirectoryPath.Name = "RemapperOutputDirectoryPath";
        RemapperOutputDirectoryPath.PlaceholderText = "Output Directory";
        RemapperOutputDirectoryPath.ReadOnly = true;
        RemapperOutputDirectoryPath.Size = new Size(297, 31);
        RemapperOutputDirectoryPath.TabIndex = 34;
        // 
        // TargetAssemblyPath
        // 
        TargetAssemblyPath.BackColor = SystemColors.ScrollBar;
        TargetAssemblyPath.Location = new Point(10, 33);
        TargetAssemblyPath.Name = "TargetAssemblyPath";
        TargetAssemblyPath.PlaceholderText = "Target Assembly";
        TargetAssemblyPath.ReadOnly = true;
        TargetAssemblyPath.Size = new Size(297, 31);
        TargetAssemblyPath.TabIndex = 33;
        // 
        // RenamePropertiesCheckbox
        // 
        RenamePropertiesCheckbox.AutoSize = true;
        RenamePropertiesCheckbox.Checked = true;
        RenamePropertiesCheckbox.CheckState = CheckState.Checked;
        RenamePropertiesCheckbox.Location = new Point(577, 260);
        RenamePropertiesCheckbox.Name = "RenamePropertiesCheckbox";
        RenamePropertiesCheckbox.Size = new Size(186, 29);
        RenamePropertiesCheckbox.TabIndex = 28;
        RenamePropertiesCheckbox.Text = "Rename Properties";
        RenamePropertiesCheckbox.UseVisualStyleBackColor = true;
        // 
        // OutputDirectoryButton
        // 
        OutputDirectoryButton.Location = new Point(312, 68);
        OutputDirectoryButton.Name = "OutputDirectoryButton";
        OutputDirectoryButton.Size = new Size(112, 34);
        OutputDirectoryButton.TabIndex = 32;
        OutputDirectoryButton.Text = "Choose";
        OutputDirectoryButton.UseVisualStyleBackColor = true;
        OutputDirectoryButton.Click += OutputDirectoryButton_Click_1;
        // 
        // PickAssemblyPathButton
        // 
        PickAssemblyPathButton.Location = new Point(313, 30);
        PickAssemblyPathButton.Name = "PickAssemblyPathButton";
        PickAssemblyPathButton.Size = new Size(112, 34);
        PickAssemblyPathButton.TabIndex = 31;
        PickAssemblyPathButton.Text = "Choose";
        PickAssemblyPathButton.UseVisualStyleBackColor = true;
        PickAssemblyPathButton.Click += PickAssemblyPathButton_Click_1;
        // 
        // ConstuctorCountUpDown
        // 
        ConstuctorCountUpDown.BackColor = SystemColors.ScrollBar;
        ConstuctorCountUpDown.Location = new Point(224, 259);
        ConstuctorCountUpDown.Name = "ConstuctorCountUpDown";
        ConstuctorCountUpDown.Size = new Size(55, 31);
        ConstuctorCountUpDown.TabIndex = 19;
        // 
        // RenameFieldsCheckbox
        // 
        RenameFieldsCheckbox.AutoSize = true;
        RenameFieldsCheckbox.Checked = true;
        RenameFieldsCheckbox.CheckState = CheckState.Checked;
        RenameFieldsCheckbox.Location = new Point(577, 225);
        RenameFieldsCheckbox.Name = "RenameFieldsCheckbox";
        RenameFieldsCheckbox.Size = new Size(151, 29);
        RenameFieldsCheckbox.TabIndex = 26;
        RenameFieldsCheckbox.Text = "Rename Fields";
        RenameFieldsCheckbox.UseVisualStyleBackColor = true;
        // 
        // ConstructorCountEnabled
        // 
        ConstructorCountEnabled.AutoSize = true;
        ConstructorCountEnabled.Location = new Point(287, 265);
        ConstructorCountEnabled.Name = "ConstructorCountEnabled";
        ConstructorCountEnabled.Size = new Size(238, 29);
        ConstructorCountEnabled.TabIndex = 20;
        ConstructorCountEnabled.Text = "Constructor Param Count";
        ConstructorCountEnabled.UseVisualStyleBackColor = true;
        // 
        // LoadMappingFileButton
        // 
        LoadMappingFileButton.BackColor = SystemColors.ButtonShadow;
        LoadMappingFileButton.Location = new Point(577, 30);
        LoadMappingFileButton.Name = "LoadMappingFileButton";
        LoadMappingFileButton.Size = new Size(168, 34);
        LoadMappingFileButton.TabIndex = 18;
        LoadMappingFileButton.Text = "Load Mapping File";
        LoadMappingFileButton.UseVisualStyleBackColor = false;
        LoadMappingFileButton.Click += LoadMappingFileButton_Click;
        // 
        // RunRemapButton
        // 
        RunRemapButton.BackColor = SystemColors.ButtonShadow;
        RunRemapButton.Location = new Point(577, 150);
        RunRemapButton.Name = "RunRemapButton";
        RunRemapButton.Size = new Size(168, 34);
        RunRemapButton.TabIndex = 16;
        RunRemapButton.Text = "Run Remap";
        RunRemapButton.UseVisualStyleBackColor = false;
        RunRemapButton.Click += RunRemapButton_Click;
        // 
        // Inclusions
        // 
        Inclusions.Controls.Add(tabPage1);
        Inclusions.Controls.Add(tabPage2);
        Inclusions.Controls.Add(tabPage3);
        Inclusions.Controls.Add(tabPage4);
        Inclusions.Location = new Point(6, 529);
        Inclusions.Name = "Inclusions";
        Inclusions.SelectedIndex = 0;
        Inclusions.Size = new Size(751, 364);
        Inclusions.TabIndex = 14;
        // 
        // tabPage1
        // 
        tabPage1.BackColor = SystemColors.ControlDarkDark;
        tabPage1.BorderStyle = BorderStyle.FixedSingle;
        tabPage1.Controls.Add(ExcludeMethodTextBox);
        tabPage1.Controls.Add(IncludeMethodTextBox);
        tabPage1.Controls.Add(MethodExcludeRemoveButton);
        tabPage1.Controls.Add(MethodExcludeAddButton);
        tabPage1.Controls.Add(MethodIncludeRemoveButton);
        tabPage1.Controls.Add(MethodIncludeAddButton);
        tabPage1.Controls.Add(MethodExcludeBox);
        tabPage1.Controls.Add(MethodIncludeBox);
        tabPage1.Location = new Point(4, 34);
        tabPage1.Name = "tabPage1";
        tabPage1.Padding = new Padding(3);
        tabPage1.Size = new Size(743, 326);
        tabPage1.TabIndex = 0;
        tabPage1.Text = "Methods";
        // 
        // ExcludeMethodTextBox
        // 
        ExcludeMethodTextBox.BackColor = SystemColors.ScrollBar;
        ExcludeMethodTextBox.Location = new Point(381, 6);
        ExcludeMethodTextBox.Name = "ExcludeMethodTextBox";
        ExcludeMethodTextBox.PlaceholderText = "Exclude Methods";
        ExcludeMethodTextBox.Size = new Size(353, 31);
        ExcludeMethodTextBox.TabIndex = 21;
        // 
        // IncludeMethodTextBox
        // 
        IncludeMethodTextBox.BackColor = SystemColors.ScrollBar;
        IncludeMethodTextBox.Location = new Point(6, 6);
        IncludeMethodTextBox.Name = "IncludeMethodTextBox";
        IncludeMethodTextBox.PlaceholderText = "Include Methods";
        IncludeMethodTextBox.Size = new Size(353, 31);
        IncludeMethodTextBox.TabIndex = 20;
        // 
        // MethodExcludeRemoveButton
        // 
        MethodExcludeRemoveButton.Location = new Point(622, 278);
        MethodExcludeRemoveButton.Name = "MethodExcludeRemoveButton";
        MethodExcludeRemoveButton.Size = new Size(112, 34);
        MethodExcludeRemoveButton.TabIndex = 19;
        MethodExcludeRemoveButton.Text = "Remove";
        MethodExcludeRemoveButton.UseVisualStyleBackColor = true;
        MethodExcludeRemoveButton.Click += MethodExcludeRemoveButton_Click;
        // 
        // MethodExcludeAddButton
        // 
        MethodExcludeAddButton.Location = new Point(381, 278);
        MethodExcludeAddButton.Name = "MethodExcludeAddButton";
        MethodExcludeAddButton.Size = new Size(112, 34);
        MethodExcludeAddButton.TabIndex = 18;
        MethodExcludeAddButton.Text = "Add";
        MethodExcludeAddButton.UseVisualStyleBackColor = true;
        MethodExcludeAddButton.Click += MethodExcludeAddButton_Click;
        // 
        // MethodIncludeRemoveButton
        // 
        MethodIncludeRemoveButton.Location = new Point(247, 278);
        MethodIncludeRemoveButton.Name = "MethodIncludeRemoveButton";
        MethodIncludeRemoveButton.Size = new Size(112, 34);
        MethodIncludeRemoveButton.TabIndex = 17;
        MethodIncludeRemoveButton.Text = "Remove";
        MethodIncludeRemoveButton.UseVisualStyleBackColor = true;
        MethodIncludeRemoveButton.Click += MethodIncludeRemoveButton_Click;
        // 
        // MethodIncludeAddButton
        // 
        MethodIncludeAddButton.Location = new Point(6, 278);
        MethodIncludeAddButton.Name = "MethodIncludeAddButton";
        MethodIncludeAddButton.Size = new Size(112, 34);
        MethodIncludeAddButton.TabIndex = 16;
        MethodIncludeAddButton.Text = "Add";
        MethodIncludeAddButton.UseVisualStyleBackColor = true;
        MethodIncludeAddButton.Click += MethodIncludeAddButton_Click;
        // 
        // MethodExcludeBox
        // 
        MethodExcludeBox.BackColor = Color.Gray;
        MethodExcludeBox.FormattingEnabled = true;
        MethodExcludeBox.ItemHeight = 25;
        MethodExcludeBox.Location = new Point(381, 43);
        MethodExcludeBox.Name = "MethodExcludeBox";
        MethodExcludeBox.Size = new Size(353, 229);
        MethodExcludeBox.TabIndex = 15;
        // 
        // MethodIncludeBox
        // 
        MethodIncludeBox.BackColor = Color.Gray;
        MethodIncludeBox.FormattingEnabled = true;
        MethodIncludeBox.ItemHeight = 25;
        MethodIncludeBox.Location = new Point(6, 43);
        MethodIncludeBox.Name = "MethodIncludeBox";
        MethodIncludeBox.Size = new Size(353, 229);
        MethodIncludeBox.TabIndex = 14;
        // 
        // tabPage2
        // 
        tabPage2.BackColor = SystemColors.ControlDarkDark;
        tabPage2.Controls.Add(FieldsExcludeTextInput);
        tabPage2.Controls.Add(FieldsIncludeTextInput);
        tabPage2.Controls.Add(FieldExcludeRemoveButton);
        tabPage2.Controls.Add(FieldExcludeAddButton);
        tabPage2.Controls.Add(FieldIncludeRemoveButton);
        tabPage2.Controls.Add(FIeldIncludeAddButton);
        tabPage2.Controls.Add(FieldExcludeBox);
        tabPage2.Controls.Add(FieldIncludeBox);
        tabPage2.Location = new Point(4, 34);
        tabPage2.Name = "tabPage2";
        tabPage2.Padding = new Padding(3);
        tabPage2.Size = new Size(743, 326);
        tabPage2.TabIndex = 1;
        tabPage2.Text = "Fields";
        // 
        // FieldsExcludeTextInput
        // 
        FieldsExcludeTextInput.BackColor = SystemColors.ScrollBar;
        FieldsExcludeTextInput.Location = new Point(381, 6);
        FieldsExcludeTextInput.Name = "FieldsExcludeTextInput";
        FieldsExcludeTextInput.PlaceholderText = "Exclude Fields";
        FieldsExcludeTextInput.Size = new Size(353, 31);
        FieldsExcludeTextInput.TabIndex = 27;
        // 
        // FieldsIncludeTextInput
        // 
        FieldsIncludeTextInput.BackColor = SystemColors.ScrollBar;
        FieldsIncludeTextInput.Location = new Point(6, 6);
        FieldsIncludeTextInput.Name = "FieldsIncludeTextInput";
        FieldsIncludeTextInput.PlaceholderText = "Include Fields";
        FieldsIncludeTextInput.Size = new Size(353, 31);
        FieldsIncludeTextInput.TabIndex = 26;
        // 
        // FieldExcludeRemoveButton
        // 
        FieldExcludeRemoveButton.BackColor = SystemColors.ButtonShadow;
        FieldExcludeRemoveButton.Location = new Point(622, 278);
        FieldExcludeRemoveButton.Name = "FieldExcludeRemoveButton";
        FieldExcludeRemoveButton.Size = new Size(112, 34);
        FieldExcludeRemoveButton.TabIndex = 25;
        FieldExcludeRemoveButton.Text = "Remove";
        FieldExcludeRemoveButton.UseVisualStyleBackColor = false;
        FieldExcludeRemoveButton.Click += FieldExcludeRemoveButton_Click;
        // 
        // FieldExcludeAddButton
        // 
        FieldExcludeAddButton.BackColor = SystemColors.ButtonShadow;
        FieldExcludeAddButton.Location = new Point(381, 278);
        FieldExcludeAddButton.Name = "FieldExcludeAddButton";
        FieldExcludeAddButton.Size = new Size(112, 34);
        FieldExcludeAddButton.TabIndex = 24;
        FieldExcludeAddButton.Text = "Add";
        FieldExcludeAddButton.UseVisualStyleBackColor = false;
        FieldExcludeAddButton.Click += FieldExcludeAddButton_Click;
        // 
        // FieldIncludeRemoveButton
        // 
        FieldIncludeRemoveButton.BackColor = SystemColors.ButtonShadow;
        FieldIncludeRemoveButton.Location = new Point(247, 278);
        FieldIncludeRemoveButton.Name = "FieldIncludeRemoveButton";
        FieldIncludeRemoveButton.Size = new Size(112, 34);
        FieldIncludeRemoveButton.TabIndex = 23;
        FieldIncludeRemoveButton.Text = "Remove";
        FieldIncludeRemoveButton.UseVisualStyleBackColor = false;
        FieldIncludeRemoveButton.Click += FieldIncludeRemoveButton_Click;
        // 
        // FIeldIncludeAddButton
        // 
        FIeldIncludeAddButton.BackColor = SystemColors.ButtonShadow;
        FIeldIncludeAddButton.Location = new Point(6, 278);
        FIeldIncludeAddButton.Name = "FIeldIncludeAddButton";
        FIeldIncludeAddButton.Size = new Size(112, 34);
        FIeldIncludeAddButton.TabIndex = 22;
        FIeldIncludeAddButton.Text = "Add";
        FIeldIncludeAddButton.UseVisualStyleBackColor = false;
        FIeldIncludeAddButton.Click += FIeldIncludeAddButton_Click;
        // 
        // FieldExcludeBox
        // 
        FieldExcludeBox.BackColor = Color.Gray;
        FieldExcludeBox.FormattingEnabled = true;
        FieldExcludeBox.ItemHeight = 25;
        FieldExcludeBox.Location = new Point(382, 43);
        FieldExcludeBox.Name = "FieldExcludeBox";
        FieldExcludeBox.Size = new Size(353, 229);
        FieldExcludeBox.TabIndex = 17;
        // 
        // FieldIncludeBox
        // 
        FieldIncludeBox.BackColor = Color.Gray;
        FieldIncludeBox.FormattingEnabled = true;
        FieldIncludeBox.ItemHeight = 25;
        FieldIncludeBox.Location = new Point(6, 43);
        FieldIncludeBox.Name = "FieldIncludeBox";
        FieldIncludeBox.Size = new Size(353, 229);
        FieldIncludeBox.TabIndex = 16;
        // 
        // tabPage3
        // 
        tabPage3.BackColor = SystemColors.ControlDarkDark;
        tabPage3.Controls.Add(PropertiesExcludeTextField);
        tabPage3.Controls.Add(PropertiesIncludeTextField);
        tabPage3.Controls.Add(PropertiesExcludeRemoveButton);
        tabPage3.Controls.Add(PropertiesExcludeAddButton);
        tabPage3.Controls.Add(PropertiesIncludeRemoveButton);
        tabPage3.Controls.Add(PropertiesIncludeAddButton);
        tabPage3.Controls.Add(PropertiesExcludeBox);
        tabPage3.Controls.Add(PropertiesIncludeBox);
        tabPage3.Location = new Point(4, 34);
        tabPage3.Name = "tabPage3";
        tabPage3.Padding = new Padding(3);
        tabPage3.Size = new Size(743, 326);
        tabPage3.TabIndex = 2;
        tabPage3.Text = "Properties";
        // 
        // PropertiesExcludeTextField
        // 
        PropertiesExcludeTextField.BackColor = SystemColors.ScrollBar;
        PropertiesExcludeTextField.Location = new Point(381, 6);
        PropertiesExcludeTextField.Name = "PropertiesExcludeTextField";
        PropertiesExcludeTextField.PlaceholderText = "Exclude Properties";
        PropertiesExcludeTextField.Size = new Size(353, 31);
        PropertiesExcludeTextField.TabIndex = 27;
        // 
        // PropertiesIncludeTextField
        // 
        PropertiesIncludeTextField.BackColor = SystemColors.ScrollBar;
        PropertiesIncludeTextField.Location = new Point(6, 6);
        PropertiesIncludeTextField.Name = "PropertiesIncludeTextField";
        PropertiesIncludeTextField.PlaceholderText = "Include Properties";
        PropertiesIncludeTextField.Size = new Size(353, 31);
        PropertiesIncludeTextField.TabIndex = 26;
        // 
        // PropertiesExcludeRemoveButton
        // 
        PropertiesExcludeRemoveButton.BackColor = SystemColors.ButtonShadow;
        PropertiesExcludeRemoveButton.Location = new Point(622, 278);
        PropertiesExcludeRemoveButton.Name = "PropertiesExcludeRemoveButton";
        PropertiesExcludeRemoveButton.Size = new Size(112, 34);
        PropertiesExcludeRemoveButton.TabIndex = 25;
        PropertiesExcludeRemoveButton.Text = "Remove";
        PropertiesExcludeRemoveButton.UseVisualStyleBackColor = false;
        PropertiesExcludeRemoveButton.Click += PropertiesExcludeRemoveButton_Click;
        // 
        // PropertiesExcludeAddButton
        // 
        PropertiesExcludeAddButton.BackColor = SystemColors.ButtonShadow;
        PropertiesExcludeAddButton.Location = new Point(381, 278);
        PropertiesExcludeAddButton.Name = "PropertiesExcludeAddButton";
        PropertiesExcludeAddButton.Size = new Size(112, 34);
        PropertiesExcludeAddButton.TabIndex = 24;
        PropertiesExcludeAddButton.Text = "Add";
        PropertiesExcludeAddButton.UseVisualStyleBackColor = false;
        PropertiesExcludeAddButton.Click += PropertiesExcludeAddButton_Click;
        // 
        // PropertiesIncludeRemoveButton
        // 
        PropertiesIncludeRemoveButton.BackColor = SystemColors.ButtonShadow;
        PropertiesIncludeRemoveButton.Location = new Point(247, 278);
        PropertiesIncludeRemoveButton.Name = "PropertiesIncludeRemoveButton";
        PropertiesIncludeRemoveButton.Size = new Size(112, 34);
        PropertiesIncludeRemoveButton.TabIndex = 23;
        PropertiesIncludeRemoveButton.Text = "Remove";
        PropertiesIncludeRemoveButton.UseVisualStyleBackColor = false;
        PropertiesIncludeRemoveButton.Click += PropertiesIncludeRemoveButton_Click;
        // 
        // PropertiesIncludeAddButton
        // 
        PropertiesIncludeAddButton.BackColor = SystemColors.ButtonShadow;
        PropertiesIncludeAddButton.Location = new Point(6, 278);
        PropertiesIncludeAddButton.Name = "PropertiesIncludeAddButton";
        PropertiesIncludeAddButton.Size = new Size(112, 34);
        PropertiesIncludeAddButton.TabIndex = 22;
        PropertiesIncludeAddButton.Text = "Add";
        PropertiesIncludeAddButton.UseVisualStyleBackColor = false;
        PropertiesIncludeAddButton.Click += PropertiesIncludeAddButton_Click;
        // 
        // PropertiesExcludeBox
        // 
        PropertiesExcludeBox.BackColor = Color.Gray;
        PropertiesExcludeBox.FormattingEnabled = true;
        PropertiesExcludeBox.ItemHeight = 25;
        PropertiesExcludeBox.Location = new Point(381, 43);
        PropertiesExcludeBox.Name = "PropertiesExcludeBox";
        PropertiesExcludeBox.Size = new Size(353, 229);
        PropertiesExcludeBox.TabIndex = 19;
        // 
        // PropertiesIncludeBox
        // 
        PropertiesIncludeBox.BackColor = Color.Gray;
        PropertiesIncludeBox.FormattingEnabled = true;
        PropertiesIncludeBox.ItemHeight = 25;
        PropertiesIncludeBox.Location = new Point(6, 43);
        PropertiesIncludeBox.Name = "PropertiesIncludeBox";
        PropertiesIncludeBox.Size = new Size(353, 229);
        PropertiesIncludeBox.TabIndex = 18;
        // 
        // tabPage4
        // 
        tabPage4.BackColor = SystemColors.ControlDarkDark;
        tabPage4.Controls.Add(NestedTypesExcludeTextField);
        tabPage4.Controls.Add(NestedTypesIncludeTextField);
        tabPage4.Controls.Add(NestedTypesExcludeRemoveButton);
        tabPage4.Controls.Add(NestedTypesExlcudeAddButton);
        tabPage4.Controls.Add(NestedTypesRemoveButton);
        tabPage4.Controls.Add(NestedTypesAddButton);
        tabPage4.Controls.Add(NestedTypesExcludeBox);
        tabPage4.Controls.Add(NestedTypesIncludeBox);
        tabPage4.Location = new Point(4, 34);
        tabPage4.Name = "tabPage4";
        tabPage4.Padding = new Padding(3);
        tabPage4.Size = new Size(743, 326);
        tabPage4.TabIndex = 3;
        tabPage4.Text = "Other";
        // 
        // NestedTypesExcludeTextField
        // 
        NestedTypesExcludeTextField.BackColor = SystemColors.ScrollBar;
        NestedTypesExcludeTextField.Location = new Point(381, 6);
        NestedTypesExcludeTextField.Name = "NestedTypesExcludeTextField";
        NestedTypesExcludeTextField.PlaceholderText = "Exclude Nested Types";
        NestedTypesExcludeTextField.Size = new Size(353, 31);
        NestedTypesExcludeTextField.TabIndex = 27;
        // 
        // NestedTypesIncludeTextField
        // 
        NestedTypesIncludeTextField.BackColor = SystemColors.ScrollBar;
        NestedTypesIncludeTextField.Location = new Point(6, 6);
        NestedTypesIncludeTextField.Name = "NestedTypesIncludeTextField";
        NestedTypesIncludeTextField.PlaceholderText = "Include Nested Types";
        NestedTypesIncludeTextField.Size = new Size(353, 31);
        NestedTypesIncludeTextField.TabIndex = 26;
        // 
        // NestedTypesExcludeRemoveButton
        // 
        NestedTypesExcludeRemoveButton.BackColor = SystemColors.ButtonShadow;
        NestedTypesExcludeRemoveButton.Location = new Point(622, 278);
        NestedTypesExcludeRemoveButton.Name = "NestedTypesExcludeRemoveButton";
        NestedTypesExcludeRemoveButton.Size = new Size(112, 34);
        NestedTypesExcludeRemoveButton.TabIndex = 25;
        NestedTypesExcludeRemoveButton.Text = "Remove";
        NestedTypesExcludeRemoveButton.UseVisualStyleBackColor = false;
        NestedTypesExcludeRemoveButton.Click += NestedTypesExcludeRemoveButton_Click;
        // 
        // NestedTypesExlcudeAddButton
        // 
        NestedTypesExlcudeAddButton.BackColor = SystemColors.ButtonShadow;
        NestedTypesExlcudeAddButton.Location = new Point(381, 278);
        NestedTypesExlcudeAddButton.Name = "NestedTypesExlcudeAddButton";
        NestedTypesExlcudeAddButton.Size = new Size(112, 34);
        NestedTypesExlcudeAddButton.TabIndex = 24;
        NestedTypesExlcudeAddButton.Text = "Add";
        NestedTypesExlcudeAddButton.UseVisualStyleBackColor = false;
        NestedTypesExlcudeAddButton.Click += NestedTypesExlcudeAddButton_Click;
        // 
        // NestedTypesRemoveButton
        // 
        NestedTypesRemoveButton.BackColor = SystemColors.ButtonShadow;
        NestedTypesRemoveButton.Location = new Point(247, 278);
        NestedTypesRemoveButton.Name = "NestedTypesRemoveButton";
        NestedTypesRemoveButton.Size = new Size(112, 34);
        NestedTypesRemoveButton.TabIndex = 23;
        NestedTypesRemoveButton.Text = "Remove";
        NestedTypesRemoveButton.UseVisualStyleBackColor = false;
        NestedTypesRemoveButton.Click += NestedTypesRemoveButton_Click;
        // 
        // NestedTypesAddButton
        // 
        NestedTypesAddButton.BackColor = SystemColors.ButtonShadow;
        NestedTypesAddButton.Location = new Point(6, 278);
        NestedTypesAddButton.Name = "NestedTypesAddButton";
        NestedTypesAddButton.Size = new Size(112, 34);
        NestedTypesAddButton.TabIndex = 22;
        NestedTypesAddButton.Text = "Add";
        NestedTypesAddButton.UseVisualStyleBackColor = false;
        NestedTypesAddButton.Click += NestedTypesAddButton_Click;
        // 
        // NestedTypesExcludeBox
        // 
        NestedTypesExcludeBox.BackColor = Color.Gray;
        NestedTypesExcludeBox.FormattingEnabled = true;
        NestedTypesExcludeBox.ItemHeight = 25;
        NestedTypesExcludeBox.Location = new Point(381, 43);
        NestedTypesExcludeBox.Name = "NestedTypesExcludeBox";
        NestedTypesExcludeBox.Size = new Size(353, 229);
        NestedTypesExcludeBox.TabIndex = 21;
        // 
        // NestedTypesIncludeBox
        // 
        NestedTypesIncludeBox.BackColor = Color.Gray;
        NestedTypesIncludeBox.FormattingEnabled = true;
        NestedTypesIncludeBox.ItemHeight = 25;
        NestedTypesIncludeBox.Location = new Point(6, 43);
        NestedTypesIncludeBox.Name = "NestedTypesIncludeBox";
        NestedTypesIncludeBox.Size = new Size(353, 229);
        NestedTypesIncludeBox.TabIndex = 20;
        // 
        // NewTypeName
        // 
        NewTypeName.BackColor = SystemColors.ScrollBar;
        NewTypeName.Location = new Point(10, 111);
        NewTypeName.Name = "NewTypeName";
        NewTypeName.PlaceholderText = "New Name";
        NewTypeName.Size = new Size(208, 31);
        NewTypeName.TabIndex = 0;
        // 
        // PropertyCountEnabled
        // 
        PropertyCountEnabled.AutoSize = true;
        PropertyCountEnabled.Location = new Point(287, 372);
        PropertyCountEnabled.Name = "PropertyCountEnabled";
        PropertyCountEnabled.Size = new Size(159, 29);
        PropertyCountEnabled.TabIndex = 11;
        PropertyCountEnabled.Text = "Property Count";
        PropertyCountEnabled.UseVisualStyleBackColor = true;
        // 
        // IsInterfaceUpDown
        // 
        IsInterfaceUpDown.BackColor = SystemColors.ScrollBar;
        IsInterfaceUpDown.Location = new Point(10, 264);
        IsInterfaceUpDown.Name = "IsInterfaceUpDown";
        IsInterfaceUpDown.Size = new Size(208, 31);
        IsInterfaceUpDown.Sorted = true;
        IsInterfaceUpDown.TabIndex = 15;
        IsInterfaceUpDown.Text = "IsInterface";
        // 
        // NestedTypeCountEnabled
        // 
        NestedTypeCountEnabled.AutoSize = true;
        NestedTypeCountEnabled.Location = new Point(287, 407);
        NestedTypeCountEnabled.Name = "NestedTypeCountEnabled";
        NestedTypeCountEnabled.Size = new Size(276, 29);
        NestedTypeCountEnabled.TabIndex = 12;
        NestedTypeCountEnabled.Text = "Nested OriginalTypeRef Count";
        NestedTypeCountEnabled.UseVisualStyleBackColor = true;
        // 
        // PropertyCountUpDown
        // 
        PropertyCountUpDown.BackColor = SystemColors.ScrollBar;
        PropertyCountUpDown.Location = new Point(224, 372);
        PropertyCountUpDown.Name = "PropertyCountUpDown";
        PropertyCountUpDown.Size = new Size(55, 31);
        PropertyCountUpDown.TabIndex = 5;
        // 
        // RemoveRemapButton
        // 
        RemoveRemapButton.BackColor = SystemColors.ButtonShadow;
        RemoveRemapButton.Location = new Point(577, 111);
        RemoveRemapButton.Name = "RemoveRemapButton";
        RemoveRemapButton.Size = new Size(168, 34);
        RemoveRemapButton.TabIndex = 2;
        RemoveRemapButton.Text = "Remove Remap";
        RemoveRemapButton.UseVisualStyleBackColor = false;
        RemoveRemapButton.Click += RemoveRemapButton_Click;
        // 
        // FieldCountUpDown
        // 
        FieldCountUpDown.BackColor = SystemColors.ScrollBar;
        FieldCountUpDown.Location = new Point(224, 334);
        FieldCountUpDown.Name = "FieldCountUpDown";
        FieldCountUpDown.Size = new Size(55, 31);
        FieldCountUpDown.TabIndex = 3;
        // 
        // IsPublicUpDown
        // 
        IsPublicUpDown.BackColor = SystemColors.ScrollBar;
        IsPublicUpDown.Location = new Point(10, 188);
        IsPublicUpDown.Name = "IsPublicUpDown";
        IsPublicUpDown.Size = new Size(208, 31);
        IsPublicUpDown.Sorted = true;
        IsPublicUpDown.TabIndex = 0;
        IsPublicUpDown.Text = "IsPublic";
        // 
        // FieldCountEnabled
        // 
        FieldCountEnabled.AutoSize = true;
        FieldCountEnabled.Location = new Point(287, 337);
        FieldCountEnabled.Name = "FieldCountEnabled";
        FieldCountEnabled.Size = new Size(128, 29);
        FieldCountEnabled.TabIndex = 13;
        FieldCountEnabled.Text = "Field Count";
        FieldCountEnabled.UseVisualStyleBackColor = true;
        // 
        // NestedTypeParentName
        // 
        NestedTypeParentName.BackColor = SystemColors.ScrollBar;
        NestedTypeParentName.Location = new Point(224, 226);
        NestedTypeParentName.Name = "NestedTypeParentName";
        NestedTypeParentName.PlaceholderText = "Nested Type Parent Name";
        NestedTypeParentName.Size = new Size(208, 31);
        NestedTypeParentName.TabIndex = 0;
        // 
        // MethodCountUpDown
        // 
        MethodCountUpDown.BackColor = SystemColors.ScrollBar;
        MethodCountUpDown.Location = new Point(224, 296);
        MethodCountUpDown.Name = "MethodCountUpDown";
        MethodCountUpDown.Size = new Size(55, 31);
        MethodCountUpDown.TabIndex = 6;
        // 
        // IsAbstractUpDown
        // 
        IsAbstractUpDown.BackColor = SystemColors.ScrollBar;
        IsAbstractUpDown.Location = new Point(10, 225);
        IsAbstractUpDown.Name = "IsAbstractUpDown";
        IsAbstractUpDown.Size = new Size(208, 31);
        IsAbstractUpDown.Sorted = true;
        IsAbstractUpDown.TabIndex = 1;
        IsAbstractUpDown.Text = "IsAbstract";
        // 
        // BaseClassIncludeTextFIeld
        // 
        BaseClassIncludeTextFIeld.BackColor = SystemColors.ScrollBar;
        BaseClassIncludeTextFIeld.Location = new Point(224, 148);
        BaseClassIncludeTextFIeld.Name = "BaseClassIncludeTextFIeld";
        BaseClassIncludeTextFIeld.PlaceholderText = "Include Base Class";
        BaseClassIncludeTextFIeld.Size = new Size(208, 31);
        BaseClassIncludeTextFIeld.TabIndex = 2;
        // 
        // OriginalTypeName
        // 
        OriginalTypeName.BackColor = SystemColors.ScrollBar;
        OriginalTypeName.Location = new Point(224, 111);
        OriginalTypeName.Name = "OriginalTypeName";
        OriginalTypeName.PlaceholderText = "Original Name";
        OriginalTypeName.Size = new Size(208, 31);
        OriginalTypeName.TabIndex = 1;
        // 
        // HasGenericParametersUpDown
        // 
        HasGenericParametersUpDown.BackColor = SystemColors.ScrollBar;
        HasGenericParametersUpDown.Location = new Point(10, 447);
        HasGenericParametersUpDown.Name = "HasGenericParametersUpDown";
        HasGenericParametersUpDown.Size = new Size(208, 31);
        HasGenericParametersUpDown.Sorted = true;
        HasGenericParametersUpDown.TabIndex = 7;
        HasGenericParametersUpDown.Text = "HasGenericParameters";
        // 
        // IsEnumUpDown
        // 
        IsEnumUpDown.BackColor = SystemColors.ScrollBar;
        IsEnumUpDown.Location = new Point(10, 336);
        IsEnumUpDown.Name = "IsEnumUpDown";
        IsEnumUpDown.Size = new Size(208, 31);
        IsEnumUpDown.Sorted = true;
        IsEnumUpDown.TabIndex = 2;
        IsEnumUpDown.Text = "IsEnum";
        // 
        // NestedTypeCountUpDown
        // 
        NestedTypeCountUpDown.BackColor = SystemColors.ScrollBar;
        NestedTypeCountUpDown.Location = new Point(224, 410);
        NestedTypeCountUpDown.Name = "NestedTypeCountUpDown";
        NestedTypeCountUpDown.Size = new Size(55, 31);
        NestedTypeCountUpDown.TabIndex = 4;
        // 
        // SaveRemapButton
        // 
        SaveRemapButton.BackColor = SystemColors.ButtonShadow;
        SaveRemapButton.Location = new Point(577, 70);
        SaveRemapButton.Name = "SaveRemapButton";
        SaveRemapButton.Size = new Size(168, 34);
        SaveRemapButton.TabIndex = 4;
        SaveRemapButton.Text = "Add Remap";
        SaveRemapButton.UseVisualStyleBackColor = false;
        SaveRemapButton.Click += AddRemapButton_Click;
        // 
        // IsDerivedUpDown
        // 
        IsDerivedUpDown.BackColor = SystemColors.ScrollBar;
        IsDerivedUpDown.Location = new Point(10, 410);
        IsDerivedUpDown.Name = "IsDerivedUpDown";
        IsDerivedUpDown.Size = new Size(208, 31);
        IsDerivedUpDown.Sorted = true;
        IsDerivedUpDown.TabIndex = 6;
        IsDerivedUpDown.Text = "IsDerived";
        // 
        // IsNestedUpDown
        // 
        IsNestedUpDown.BackColor = SystemColors.ScrollBar;
        IsNestedUpDown.Location = new Point(10, 149);
        IsNestedUpDown.Name = "IsNestedUpDown";
        IsNestedUpDown.Size = new Size(208, 31);
        IsNestedUpDown.Sorted = true;
        IsNestedUpDown.TabIndex = 3;
        IsNestedUpDown.Text = "IsNested";
        // 
        // HasAttributeUpDown
        // 
        HasAttributeUpDown.BackColor = SystemColors.ScrollBar;
        HasAttributeUpDown.Location = new Point(10, 373);
        HasAttributeUpDown.Name = "HasAttributeUpDown";
        HasAttributeUpDown.Size = new Size(208, 31);
        HasAttributeUpDown.Sorted = true;
        HasAttributeUpDown.TabIndex = 5;
        HasAttributeUpDown.Text = "HasAttribute";
        // 
        // BaseClassExcludeTextField
        // 
        BaseClassExcludeTextField.BackColor = SystemColors.ScrollBar;
        BaseClassExcludeTextField.Location = new Point(224, 188);
        BaseClassExcludeTextField.Name = "BaseClassExcludeTextField";
        BaseClassExcludeTextField.PlaceholderText = "Exclude Base Class";
        BaseClassExcludeTextField.Size = new Size(208, 31);
        BaseClassExcludeTextField.TabIndex = 1;
        // 
        // MethodCountEnabled
        // 
        MethodCountEnabled.AutoSize = true;
        MethodCountEnabled.Location = new Point(287, 302);
        MethodCountEnabled.Name = "MethodCountEnabled";
        MethodCountEnabled.Size = new Size(154, 29);
        MethodCountEnabled.TabIndex = 14;
        MethodCountEnabled.Text = "Method Count";
        MethodCountEnabled.UseVisualStyleBackColor = true;
        // 
        // IsSealedUpDown
        // 
        IsSealedUpDown.BackColor = SystemColors.ScrollBar;
        IsSealedUpDown.Location = new Point(10, 299);
        IsSealedUpDown.Name = "IsSealedUpDown";
        IsSealedUpDown.Size = new Size(208, 31);
        IsSealedUpDown.Sorted = true;
        IsSealedUpDown.TabIndex = 4;
        IsSealedUpDown.Text = "IsSealed";
        // 
        // TabControlMain
        // 
        TabControlMain.Controls.Add(TabPageRemapper);
        TabControlMain.Controls.Add(AutoMapperTab);
        TabControlMain.Controls.Add(tabPage5);
        TabControlMain.Controls.Add(SettingsTab);
        TabControlMain.Location = new Point(-5, 1);
        TabControlMain.Name = "TabControlMain";
        TabControlMain.SelectedIndex = 0;
        TabControlMain.Size = new Size(1344, 991);
        TabControlMain.TabIndex = 6;
        // 
        // AutoMapperTab
        // 
        AutoMapperTab.BackColor = SystemColors.ControlDarkDark;
        AutoMapperTab.Controls.Add(AutoMapperUnseal);
        AutoMapperTab.Controls.Add(AutoMapperPublicize);
        AutoMapperTab.Controls.Add(AutoMapperRenameProps);
        AutoMapperTab.Controls.Add(AutoMapperRenameFields);
        AutoMapperTab.Controls.Add(AutoMapperOuputPath);
        AutoMapperTab.Controls.Add(AutoMapperTargetPath);
        AutoMapperTab.Controls.Add(AutoMapperChooseOutpathButton);
        AutoMapperTab.Controls.Add(AutoMapperChooseTargetPathButton);
        AutoMapperTab.Controls.Add(AutoMapperMethodTextBox);
        AutoMapperTab.Controls.Add(AutoMapperMethodRemoveButton);
        AutoMapperTab.Controls.Add(AutoMapperMethodAddButton);
        AutoMapperTab.Controls.Add(AutoMapperMethodBox);
        AutoMapperTab.Controls.Add(AutoMapperSearchMethodsCheckBox);
        AutoMapperTab.Controls.Add(AutoMapperFPTextField);
        AutoMapperTab.Controls.Add(AutoMapperFPRemoveButton);
        AutoMapperTab.Controls.Add(AutoMapperFPAddButton);
        AutoMapperTab.Controls.Add(AutoMapperFPBox);
        AutoMapperTab.Controls.Add(AutoMapperTokensTextField);
        AutoMapperTab.Controls.Add(AutoMapperTokensRemoveButton);
        AutoMapperTab.Controls.Add(AutoMapperTokensAddButton);
        AutoMapperTab.Controls.Add(AutoMapperTokensBox);
        AutoMapperTab.Controls.Add(label3);
        AutoMapperTab.Controls.Add(AutoMapperMinLengthUpDown);
        AutoMapperTab.Controls.Add(RunAutoRemapButton);
        AutoMapperTab.Controls.Add(label2);
        AutoMapperTab.Controls.Add(AutoMapperRequiredMatchesUpDown);
        AutoMapperTab.Controls.Add(AutoMapperTypesToIgnoreTextField);
        AutoMapperTab.Controls.Add(AutoMapperExcludeTypesRemoveButton);
        AutoMapperTab.Controls.Add(AutoMapperExcludeTypesAddButton);
        AutoMapperTab.Controls.Add(AutoMapperTypesExcludeBox);
        AutoMapperTab.Location = new Point(4, 34);
        AutoMapperTab.Name = "AutoMapperTab";
        AutoMapperTab.Padding = new Padding(3);
        AutoMapperTab.Size = new Size(1336, 953);
        AutoMapperTab.TabIndex = 3;
        AutoMapperTab.Text = "Auto Mapper";
        // 
        // AutoMapperUnseal
        // 
        AutoMapperUnseal.AutoSize = true;
        AutoMapperUnseal.Checked = true;
        AutoMapperUnseal.CheckState = CheckState.Checked;
        AutoMapperUnseal.Location = new Point(537, 136);
        AutoMapperUnseal.Name = "AutoMapperUnseal";
        AutoMapperUnseal.Size = new Size(90, 29);
        AutoMapperUnseal.TabIndex = 51;
        AutoMapperUnseal.Text = "Unseal";
        AutoMapperUnseal.UseVisualStyleBackColor = true;
        AutoMapperUnseal.CheckedChanged += AutoMapperUnseal_CheckedChanged;
        // 
        // AutoMapperPublicize
        // 
        AutoMapperPublicize.AutoSize = true;
        AutoMapperPublicize.Checked = true;
        AutoMapperPublicize.CheckState = CheckState.Checked;
        AutoMapperPublicize.Location = new Point(537, 101);
        AutoMapperPublicize.Name = "AutoMapperPublicize";
        AutoMapperPublicize.Size = new Size(106, 29);
        AutoMapperPublicize.TabIndex = 50;
        AutoMapperPublicize.Text = "Publicize";
        AutoMapperPublicize.UseVisualStyleBackColor = true;
        AutoMapperPublicize.CheckedChanged += AutoMapperPublicize_CheckedChanged;
        // 
        // AutoMapperRenameProps
        // 
        AutoMapperRenameProps.AutoSize = true;
        AutoMapperRenameProps.Checked = true;
        AutoMapperRenameProps.CheckState = CheckState.Checked;
        AutoMapperRenameProps.Location = new Point(537, 66);
        AutoMapperRenameProps.Name = "AutoMapperRenameProps";
        AutoMapperRenameProps.Size = new Size(186, 29);
        AutoMapperRenameProps.TabIndex = 49;
        AutoMapperRenameProps.Text = "Rename Properties";
        AutoMapperRenameProps.UseVisualStyleBackColor = true;
        AutoMapperRenameProps.CheckedChanged += AutoMapperRenameProps_CheckedChanged;
        // 
        // AutoMapperRenameFields
        // 
        AutoMapperRenameFields.AutoSize = true;
        AutoMapperRenameFields.Checked = true;
        AutoMapperRenameFields.CheckState = CheckState.Checked;
        AutoMapperRenameFields.Location = new Point(537, 31);
        AutoMapperRenameFields.Name = "AutoMapperRenameFields";
        AutoMapperRenameFields.Size = new Size(151, 29);
        AutoMapperRenameFields.TabIndex = 48;
        AutoMapperRenameFields.Text = "Rename Fields";
        AutoMapperRenameFields.UseVisualStyleBackColor = true;
        AutoMapperRenameFields.CheckedChanged += AutoMapperRenameFields_CheckedChanged;
        // 
        // AutoMapperOuputPath
        // 
        AutoMapperOuputPath.BackColor = SystemColors.ScrollBar;
        AutoMapperOuputPath.Location = new Point(6, 43);
        AutoMapperOuputPath.Name = "AutoMapperOuputPath";
        AutoMapperOuputPath.PlaceholderText = "Output Directory";
        AutoMapperOuputPath.ReadOnly = true;
        AutoMapperOuputPath.Size = new Size(297, 31);
        AutoMapperOuputPath.TabIndex = 47;
        // 
        // AutoMapperTargetPath
        // 
        AutoMapperTargetPath.BackColor = SystemColors.ScrollBar;
        AutoMapperTargetPath.Location = new Point(6, 6);
        AutoMapperTargetPath.Name = "AutoMapperTargetPath";
        AutoMapperTargetPath.PlaceholderText = "Target Assembly";
        AutoMapperTargetPath.ReadOnly = true;
        AutoMapperTargetPath.Size = new Size(297, 31);
        AutoMapperTargetPath.TabIndex = 46;
        // 
        // AutoMapperChooseOutpathButton
        // 
        AutoMapperChooseOutpathButton.Location = new Point(308, 41);
        AutoMapperChooseOutpathButton.Name = "AutoMapperChooseOutpathButton";
        AutoMapperChooseOutpathButton.Size = new Size(112, 34);
        AutoMapperChooseOutpathButton.TabIndex = 45;
        AutoMapperChooseOutpathButton.Text = "Choose";
        AutoMapperChooseOutpathButton.UseVisualStyleBackColor = true;
        AutoMapperChooseOutpathButton.Click += AutoMapperChooseOutpathButton_Click;
        // 
        // AutoMapperChooseTargetPathButton
        // 
        AutoMapperChooseTargetPathButton.Location = new Point(308, 3);
        AutoMapperChooseTargetPathButton.Name = "AutoMapperChooseTargetPathButton";
        AutoMapperChooseTargetPathButton.Size = new Size(112, 34);
        AutoMapperChooseTargetPathButton.TabIndex = 44;
        AutoMapperChooseTargetPathButton.Text = "Choose";
        AutoMapperChooseTargetPathButton.UseVisualStyleBackColor = true;
        AutoMapperChooseTargetPathButton.Click += AutoMapperChooseTargetPathButton_Click;
        // 
        // AutoMapperMethodTextBox
        // 
        AutoMapperMethodTextBox.BackColor = SystemColors.ScrollBar;
        AutoMapperMethodTextBox.Location = new Point(365, 496);
        AutoMapperMethodTextBox.Name = "AutoMapperMethodTextBox";
        AutoMapperMethodTextBox.PlaceholderText = "method parameter names to blacklist";
        AutoMapperMethodTextBox.Size = new Size(353, 31);
        AutoMapperMethodTextBox.TabIndex = 43;
        // 
        // AutoMapperMethodRemoveButton
        // 
        AutoMapperMethodRemoveButton.Location = new Point(606, 768);
        AutoMapperMethodRemoveButton.Name = "AutoMapperMethodRemoveButton";
        AutoMapperMethodRemoveButton.Size = new Size(112, 34);
        AutoMapperMethodRemoveButton.TabIndex = 42;
        AutoMapperMethodRemoveButton.Text = "Remove";
        AutoMapperMethodRemoveButton.UseVisualStyleBackColor = true;
        AutoMapperMethodRemoveButton.Click += AutoMapperMethodRemoveButton_Click;
        // 
        // AutoMapperMethodAddButton
        // 
        AutoMapperMethodAddButton.Location = new Point(365, 768);
        AutoMapperMethodAddButton.Name = "AutoMapperMethodAddButton";
        AutoMapperMethodAddButton.Size = new Size(112, 34);
        AutoMapperMethodAddButton.TabIndex = 41;
        AutoMapperMethodAddButton.Text = "Add";
        AutoMapperMethodAddButton.UseVisualStyleBackColor = true;
        AutoMapperMethodAddButton.Click += AutoMapperMethodAddButton_Click;
        // 
        // AutoMapperMethodBox
        // 
        AutoMapperMethodBox.BackColor = Color.Gray;
        AutoMapperMethodBox.FormattingEnabled = true;
        AutoMapperMethodBox.ItemHeight = 25;
        AutoMapperMethodBox.Location = new Point(365, 533);
        AutoMapperMethodBox.Name = "AutoMapperMethodBox";
        AutoMapperMethodBox.Size = new Size(353, 229);
        AutoMapperMethodBox.TabIndex = 40;
        // 
        // AutoMapperSearchMethodsCheckBox
        // 
        AutoMapperSearchMethodsCheckBox.AutoSize = true;
        AutoMapperSearchMethodsCheckBox.Location = new Point(536, 2);
        AutoMapperSearchMethodsCheckBox.Name = "AutoMapperSearchMethodsCheckBox";
        AutoMapperSearchMethodsCheckBox.Size = new Size(291, 29);
        AutoMapperSearchMethodsCheckBox.TabIndex = 39;
        AutoMapperSearchMethodsCheckBox.Text = "Search Methods (Experiemental)";
        AutoMapperSearchMethodsCheckBox.UseVisualStyleBackColor = true;
        AutoMapperSearchMethodsCheckBox.CheckedChanged += SearchMethodsCheckBox_CheckedChanged;
        // 
        // AutoMapperFPTextField
        // 
        AutoMapperFPTextField.BackColor = SystemColors.ScrollBar;
        AutoMapperFPTextField.Location = new Point(6, 496);
        AutoMapperFPTextField.Name = "AutoMapperFPTextField";
        AutoMapperFPTextField.PlaceholderText = "Field or property names to blacklist";
        AutoMapperFPTextField.Size = new Size(353, 31);
        AutoMapperFPTextField.TabIndex = 38;
        // 
        // AutoMapperFPRemoveButton
        // 
        AutoMapperFPRemoveButton.Location = new Point(247, 768);
        AutoMapperFPRemoveButton.Name = "AutoMapperFPRemoveButton";
        AutoMapperFPRemoveButton.Size = new Size(112, 34);
        AutoMapperFPRemoveButton.TabIndex = 37;
        AutoMapperFPRemoveButton.Text = "Remove";
        AutoMapperFPRemoveButton.UseVisualStyleBackColor = true;
        AutoMapperFPRemoveButton.Click += AutoMapperFPRemoveButton_Click;
        // 
        // AutoMapperFPAddButton
        // 
        AutoMapperFPAddButton.Location = new Point(6, 768);
        AutoMapperFPAddButton.Name = "AutoMapperFPAddButton";
        AutoMapperFPAddButton.Size = new Size(112, 34);
        AutoMapperFPAddButton.TabIndex = 36;
        AutoMapperFPAddButton.Text = "Add";
        AutoMapperFPAddButton.UseVisualStyleBackColor = true;
        AutoMapperFPAddButton.Click += AutoMapperFPAddButton_Click;
        // 
        // AutoMapperFPBox
        // 
        AutoMapperFPBox.BackColor = Color.Gray;
        AutoMapperFPBox.FormattingEnabled = true;
        AutoMapperFPBox.ItemHeight = 25;
        AutoMapperFPBox.Location = new Point(6, 533);
        AutoMapperFPBox.Name = "AutoMapperFPBox";
        AutoMapperFPBox.Size = new Size(353, 229);
        AutoMapperFPBox.TabIndex = 35;
        // 
        // AutoMapperTokensTextField
        // 
        AutoMapperTokensTextField.BackColor = SystemColors.ScrollBar;
        AutoMapperTokensTextField.Location = new Point(365, 173);
        AutoMapperTokensTextField.Name = "AutoMapperTokensTextField";
        AutoMapperTokensTextField.PlaceholderText = "Class Tokens To Match During Renaming";
        AutoMapperTokensTextField.Size = new Size(353, 31);
        AutoMapperTokensTextField.TabIndex = 34;
        // 
        // AutoMapperTokensRemoveButton
        // 
        AutoMapperTokensRemoveButton.Location = new Point(606, 445);
        AutoMapperTokensRemoveButton.Name = "AutoMapperTokensRemoveButton";
        AutoMapperTokensRemoveButton.Size = new Size(112, 34);
        AutoMapperTokensRemoveButton.TabIndex = 33;
        AutoMapperTokensRemoveButton.Text = "Remove";
        AutoMapperTokensRemoveButton.UseVisualStyleBackColor = true;
        AutoMapperTokensRemoveButton.Click += AutoMapperTokensRemoveButton_Click;
        // 
        // AutoMapperTokensAddButton
        // 
        AutoMapperTokensAddButton.Location = new Point(365, 445);
        AutoMapperTokensAddButton.Name = "AutoMapperTokensAddButton";
        AutoMapperTokensAddButton.Size = new Size(112, 34);
        AutoMapperTokensAddButton.TabIndex = 32;
        AutoMapperTokensAddButton.Text = "Add";
        AutoMapperTokensAddButton.UseVisualStyleBackColor = true;
        AutoMapperTokensAddButton.Click += AutoMapperTokensAddButton_Click;
        // 
        // AutoMapperTokensBox
        // 
        AutoMapperTokensBox.BackColor = Color.Gray;
        AutoMapperTokensBox.FormattingEnabled = true;
        AutoMapperTokensBox.ItemHeight = 25;
        AutoMapperTokensBox.Location = new Point(365, 210);
        AutoMapperTokensBox.Name = "AutoMapperTokensBox";
        AutoMapperTokensBox.Size = new Size(353, 229);
        AutoMapperTokensBox.TabIndex = 31;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(243, 119);
        label3.Name = "label3";
        label3.Size = new Size(277, 25);
        label3.TabIndex = 30;
        label3.Text = "Min Length of field or prop name";
        // 
        // AutoMapperMinLengthUpDown
        // 
        AutoMapperMinLengthUpDown.Location = new Point(180, 117);
        AutoMapperMinLengthUpDown.Name = "AutoMapperMinLengthUpDown";
        AutoMapperMinLengthUpDown.Size = new Size(57, 31);
        AutoMapperMinLengthUpDown.TabIndex = 29;
        AutoMapperMinLengthUpDown.ValueChanged += AutoMapperMinLengthUpDown_ValueChanged;
        // 
        // RunAutoRemapButton
        // 
        RunAutoRemapButton.Location = new Point(6, 80);
        RunAutoRemapButton.Name = "RunAutoRemapButton";
        RunAutoRemapButton.Size = new Size(168, 34);
        RunAutoRemapButton.TabIndex = 28;
        RunAutoRemapButton.Text = "Run Auto Remap";
        RunAutoRemapButton.UseVisualStyleBackColor = true;
        RunAutoRemapButton.Click += RunAutoRemapButton_Click;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(243, 86);
        label2.Name = "label2";
        label2.Size = new Size(153, 25);
        label2.TabIndex = 27;
        label2.Text = "Required Matches";
        // 
        // AutoMapperRequiredMatchesUpDown
        // 
        AutoMapperRequiredMatchesUpDown.Location = new Point(180, 80);
        AutoMapperRequiredMatchesUpDown.Name = "AutoMapperRequiredMatchesUpDown";
        AutoMapperRequiredMatchesUpDown.Size = new Size(57, 31);
        AutoMapperRequiredMatchesUpDown.TabIndex = 26;
        AutoMapperRequiredMatchesUpDown.ValueChanged += AutoMapperRequiredMatchesUpDown_ValueChanged_1;
        // 
        // AutoMapperTypesToIgnoreTextField
        // 
        AutoMapperTypesToIgnoreTextField.BackColor = SystemColors.ScrollBar;
        AutoMapperTypesToIgnoreTextField.Location = new Point(6, 173);
        AutoMapperTypesToIgnoreTextField.Name = "AutoMapperTypesToIgnoreTextField";
        AutoMapperTypesToIgnoreTextField.PlaceholderText = "Type Exclude";
        AutoMapperTypesToIgnoreTextField.Size = new Size(353, 31);
        AutoMapperTypesToIgnoreTextField.TabIndex = 24;
        // 
        // AutoMapperExcludeTypesRemoveButton
        // 
        AutoMapperExcludeTypesRemoveButton.Location = new Point(247, 445);
        AutoMapperExcludeTypesRemoveButton.Name = "AutoMapperExcludeTypesRemoveButton";
        AutoMapperExcludeTypesRemoveButton.Size = new Size(112, 34);
        AutoMapperExcludeTypesRemoveButton.TabIndex = 23;
        AutoMapperExcludeTypesRemoveButton.Text = "Remove";
        AutoMapperExcludeTypesRemoveButton.UseVisualStyleBackColor = true;
        AutoMapperExcludeTypesRemoveButton.Click += AutoMapperExcludeRemoveButton_Click;
        // 
        // AutoMapperExcludeTypesAddButton
        // 
        AutoMapperExcludeTypesAddButton.Location = new Point(6, 445);
        AutoMapperExcludeTypesAddButton.Name = "AutoMapperExcludeTypesAddButton";
        AutoMapperExcludeTypesAddButton.Size = new Size(112, 34);
        AutoMapperExcludeTypesAddButton.TabIndex = 22;
        AutoMapperExcludeTypesAddButton.Text = "Add";
        AutoMapperExcludeTypesAddButton.UseVisualStyleBackColor = true;
        AutoMapperExcludeTypesAddButton.Click += AutoMapperExcludeAddButton_Click;
        // 
        // AutoMapperTypesExcludeBox
        // 
        AutoMapperTypesExcludeBox.BackColor = Color.Gray;
        AutoMapperTypesExcludeBox.FormattingEnabled = true;
        AutoMapperTypesExcludeBox.ItemHeight = 25;
        AutoMapperTypesExcludeBox.Location = new Point(6, 210);
        AutoMapperTypesExcludeBox.Name = "AutoMapperTypesExcludeBox";
        AutoMapperTypesExcludeBox.Size = new Size(353, 229);
        AutoMapperTypesExcludeBox.TabIndex = 21;
        // 
        // tabPage5
        // 
        tabPage5.BackColor = SystemColors.ControlDarkDark;
        tabPage5.Controls.Add(groupBox4);
        tabPage5.Location = new Point(4, 34);
        tabPage5.Name = "tabPage5";
        tabPage5.Padding = new Padding(3);
        tabPage5.Size = new Size(1336, 953);
        tabPage5.TabIndex = 4;
        tabPage5.Text = "Cross Compiler";
        // 
        // groupBox4
        // 
        groupBox4.Controls.Add(groupBox5);
        groupBox4.Controls.Add(label4);
        groupBox4.Controls.Add(CCMappingTreeView);
        groupBox4.Controls.Add(groupBox3);
        groupBox4.Location = new Point(13, 18);
        groupBox4.Name = "groupBox4";
        groupBox4.Size = new Size(1273, 901);
        groupBox4.TabIndex = 22;
        groupBox4.TabStop = false;
        groupBox4.Text = "Cross Compiler";
        // 
        // groupBox5
        // 
        groupBox5.Controls.Add(CCBuildConfiguration);
        groupBox5.Controls.Add(CrossPatchRemapButton);
        groupBox5.Controls.Add(CrossPatchRunButton);
        groupBox5.Location = new Point(6, 304);
        groupBox5.Name = "groupBox5";
        groupBox5.Size = new Size(631, 215);
        groupBox5.TabIndex = 27;
        groupBox5.TabStop = false;
        groupBox5.Text = "Generation";
        // 
        // CCBuildConfiguration
        // 
        CCBuildConfiguration.Location = new Point(6, 30);
        CCBuildConfiguration.Name = "CCBuildConfiguration";
        CCBuildConfiguration.PlaceholderText = "Build Configuration";
        CCBuildConfiguration.Size = new Size(150, 31);
        CCBuildConfiguration.TabIndex = 25;
        CCBuildConfiguration.Text = "Debug";
        // 
        // CrossPatchRemapButton
        // 
        CrossPatchRemapButton.Location = new Point(6, 175);
        CrossPatchRemapButton.Name = "CrossPatchRemapButton";
        CrossPatchRemapButton.Size = new Size(316, 34);
        CrossPatchRemapButton.TabIndex = 21;
        CrossPatchRemapButton.Text = "Generate Remapped Reference";
        CrossPatchRemapButton.UseVisualStyleBackColor = true;
        CrossPatchRemapButton.Click += CrossPatchRemapButton_Click;
        // 
        // CrossPatchRunButton
        // 
        CrossPatchRunButton.Location = new Point(328, 175);
        CrossPatchRunButton.Name = "CrossPatchRunButton";
        CrossPatchRunButton.Size = new Size(150, 34);
        CrossPatchRunButton.TabIndex = 24;
        CrossPatchRunButton.Text = "Compile Project";
        CrossPatchRunButton.UseVisualStyleBackColor = true;
        CrossPatchRunButton.Click += CrossPatchRunButton_Click;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(786, 18);
        label4.Name = "label4";
        label4.Size = new Size(446, 25);
        label4.TabIndex = 26;
        label4.Text = "Project Mappings (Double click to edit in remap editor)";
        // 
        // CCMappingTreeView
        // 
        CCMappingTreeView.BackColor = Color.Gray;
        CCMappingTreeView.Location = new Point(786, 46);
        CCMappingTreeView.Name = "CCMappingTreeView";
        CCMappingTreeView.Size = new Size(487, 855);
        CCMappingTreeView.TabIndex = 25;
        // 
        // groupBox3
        // 
        groupBox3.Controls.Add(CCImportMappings);
        groupBox3.Controls.Add(CCAutoLoadLastProj);
        groupBox3.Controls.Add(CCLoadProjButton);
        groupBox3.Controls.Add(CCRemappedOutputButton);
        groupBox3.Controls.Add(CCRemappedOutputText);
        groupBox3.Controls.Add(CCOriginalAssemblyText);
        groupBox3.Controls.Add(CCVisualStudioProjDirText);
        groupBox3.Controls.Add(CrossCompilerNewProjectButton);
        groupBox3.Controls.Add(CCVisualStudioProjDirButton);
        groupBox3.Controls.Add(CCBuildDirButton);
        groupBox3.Controls.Add(CCOriginalAssemblyButton);
        groupBox3.Controls.Add(CCBuildDirText);
        groupBox3.Location = new Point(6, 30);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(631, 268);
        groupBox3.TabIndex = 21;
        groupBox3.TabStop = false;
        groupBox3.Text = "ReCodeIt Proj Settings";
        // 
        // CCImportMappings
        // 
        CCImportMappings.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
        CCImportMappings.Location = new Point(6, 219);
        CCImportMappings.Name = "CCImportMappings";
        CCImportMappings.Size = new Size(150, 34);
        CCImportMappings.TabIndex = 35;
        CCImportMappings.Text = "Import Mappings";
        CCImportMappings.UseVisualStyleBackColor = true;
        CCImportMappings.Click += CCImportMappings_Click;
        // 
        // CCAutoLoadLastProj
        // 
        CCAutoLoadLastProj.AutoSize = true;
        CCAutoLoadLastProj.Checked = true;
        CCAutoLoadLastProj.CheckState = CheckState.Checked;
        CCAutoLoadLastProj.Location = new Point(318, 183);
        CCAutoLoadLastProj.Name = "CCAutoLoadLastProj";
        CCAutoLoadLastProj.Size = new Size(259, 29);
        CCAutoLoadLastProj.TabIndex = 34;
        CCAutoLoadLastProj.Text = "Auto load last active project";
        CCAutoLoadLastProj.UseVisualStyleBackColor = true;
        CCAutoLoadLastProj.CheckedChanged += CCAutoLoadLastProj_CheckedChanged_1;
        // 
        // CCLoadProjButton
        // 
        CCLoadProjButton.Location = new Point(162, 179);
        CCLoadProjButton.Name = "CCLoadProjButton";
        CCLoadProjButton.Size = new Size(150, 34);
        CCLoadProjButton.TabIndex = 33;
        CCLoadProjButton.Text = "Load Project";
        CCLoadProjButton.UseVisualStyleBackColor = true;
        CCLoadProjButton.Click += CCLoadProjButton_Click;
        // 
        // CCRemappedOutputButton
        // 
        CCRemappedOutputButton.Location = new Point(513, 66);
        CCRemappedOutputButton.Name = "CCRemappedOutputButton";
        CCRemappedOutputButton.Size = new Size(112, 34);
        CCRemappedOutputButton.TabIndex = 32;
        CCRemappedOutputButton.Text = "Choose";
        CCRemappedOutputButton.UseVisualStyleBackColor = true;
        CCRemappedOutputButton.Click += CCRemappedOutputButton_Click;
        // 
        // CCRemappedOutputText
        // 
        CCRemappedOutputText.Location = new Point(6, 68);
        CCRemappedOutputText.Name = "CCRemappedOutputText";
        CCRemappedOutputText.PlaceholderText = "Remapped Assembly Output (Assembly-CSharp-Remapped)";
        CCRemappedOutputText.ReadOnly = true;
        CCRemappedOutputText.Size = new Size(501, 31);
        CCRemappedOutputText.TabIndex = 26;
        // 
        // CCOriginalAssemblyText
        // 
        CCOriginalAssemblyText.Location = new Point(6, 30);
        CCOriginalAssemblyText.Name = "CCOriginalAssemblyText";
        CCOriginalAssemblyText.PlaceholderText = "Original Input Target Assembly (Assembly-CSharp)";
        CCOriginalAssemblyText.ReadOnly = true;
        CCOriginalAssemblyText.Size = new Size(501, 31);
        CCOriginalAssemblyText.TabIndex = 25;
        // 
        // CCVisualStudioProjDirText
        // 
        CCVisualStudioProjDirText.Location = new Point(6, 105);
        CCVisualStudioProjDirText.Name = "CCVisualStudioProjDirText";
        CCVisualStudioProjDirText.PlaceholderText = "Visual studio solution";
        CCVisualStudioProjDirText.ReadOnly = true;
        CCVisualStudioProjDirText.Size = new Size(501, 31);
        CCVisualStudioProjDirText.TabIndex = 27;
        // 
        // CrossCompilerNewProjectButton
        // 
        CrossCompilerNewProjectButton.Location = new Point(6, 179);
        CrossCompilerNewProjectButton.Name = "CrossCompilerNewProjectButton";
        CrossCompilerNewProjectButton.Size = new Size(150, 34);
        CrossCompilerNewProjectButton.TabIndex = 25;
        CrossCompilerNewProjectButton.Text = "Create New";
        CrossCompilerNewProjectButton.UseVisualStyleBackColor = true;
        CrossCompilerNewProjectButton.Click += CrossCompilerNewProjectButton_Click;
        // 
        // CCVisualStudioProjDirButton
        // 
        CCVisualStudioProjDirButton.Location = new Point(513, 102);
        CCVisualStudioProjDirButton.Name = "CCVisualStudioProjDirButton";
        CCVisualStudioProjDirButton.Size = new Size(112, 34);
        CCVisualStudioProjDirButton.TabIndex = 31;
        CCVisualStudioProjDirButton.Text = "Choose";
        CCVisualStudioProjDirButton.UseVisualStyleBackColor = true;
        CCVisualStudioProjDirButton.Click += CCVisualStudioProjDirButton_Click;
        // 
        // CCBuildDirButton
        // 
        CCBuildDirButton.Location = new Point(513, 141);
        CCBuildDirButton.Name = "CCBuildDirButton";
        CCBuildDirButton.Size = new Size(112, 34);
        CCBuildDirButton.TabIndex = 29;
        CCBuildDirButton.Text = "Choose";
        CCBuildDirButton.UseVisualStyleBackColor = true;
        CCBuildDirButton.Click += CCBuildDirButton_Click;
        // 
        // CCOriginalAssemblyButton
        // 
        CCOriginalAssemblyButton.Location = new Point(513, 28);
        CCOriginalAssemblyButton.Name = "CCOriginalAssemblyButton";
        CCOriginalAssemblyButton.Size = new Size(112, 34);
        CCOriginalAssemblyButton.TabIndex = 28;
        CCOriginalAssemblyButton.Text = "Choose";
        CCOriginalAssemblyButton.UseVisualStyleBackColor = true;
        CCOriginalAssemblyButton.Click += CCOriginalAssemblyButton_Click;
        // 
        // CCBuildDirText
        // 
        CCBuildDirText.Location = new Point(6, 142);
        CCBuildDirText.Name = "CCBuildDirText";
        CCBuildDirText.PlaceholderText = "Build Directory";
        CCBuildDirText.ReadOnly = true;
        CCBuildDirText.Size = new Size(501, 31);
        CCBuildDirText.TabIndex = 30;
        // 
        // SettingsTab
        // 
        SettingsTab.BackColor = SystemColors.ControlDarkDark;
        SettingsTab.Controls.Add(groupBox2);
        SettingsTab.Location = new Point(4, 34);
        SettingsTab.Name = "SettingsTab";
        SettingsTab.Padding = new Padding(3);
        SettingsTab.Size = new Size(1336, 953);
        SettingsTab.TabIndex = 2;
        SettingsTab.Text = "Settings";
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(GithubLinkLabel);
        groupBox2.Controls.Add(SilentModeCheckbox);
        groupBox2.Controls.Add(DebugLoggingCheckbox);
        groupBox2.Location = new Point(13, 6);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(445, 350);
        groupBox2.TabIndex = 0;
        groupBox2.TabStop = false;
        groupBox2.Text = "App Settings";
        // 
        // GithubLinkLabel
        // 
        GithubLinkLabel.AutoSize = true;
        GithubLinkLabel.Location = new Point(6, 313);
        GithubLinkLabel.Name = "GithubLinkLabel";
        GithubLinkLabel.Size = new Size(301, 25);
        GithubLinkLabel.TabIndex = 3;
        GithubLinkLabel.TabStop = true;
        GithubLinkLabel.Text = "https://github.com/CJ-SPT/ReCodeIt";
        GithubLinkLabel.LinkClicked += GithubLinkLabel_LinkClicked;
        // 
        // SilentModeCheckbox
        // 
        SilentModeCheckbox.AutoSize = true;
        SilentModeCheckbox.Location = new Point(6, 65);
        SilentModeCheckbox.Name = "SilentModeCheckbox";
        SilentModeCheckbox.Size = new Size(133, 29);
        SilentModeCheckbox.TabIndex = 2;
        SilentModeCheckbox.Text = "Silent Mode";
        SilentModeCheckbox.UseVisualStyleBackColor = true;
        SilentModeCheckbox.CheckedChanged += SilentModeCheckbox_CheckedChanged;
        // 
        // DebugLoggingCheckbox
        // 
        DebugLoggingCheckbox.AutoSize = true;
        DebugLoggingCheckbox.Location = new Point(6, 30);
        DebugLoggingCheckbox.Name = "DebugLoggingCheckbox";
        DebugLoggingCheckbox.Size = new Size(159, 29);
        DebugLoggingCheckbox.TabIndex = 0;
        DebugLoggingCheckbox.Text = "Debug logging";
        DebugLoggingCheckbox.UseVisualStyleBackColor = true;
        DebugLoggingCheckbox.CheckedChanged += DebugLoggingCheckbox_CheckedChanged;
        // 
        // ReCodeItForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ControlDarkDark;
        ClientSize = new Size(1297, 966);
        Controls.Add(TabControlMain);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Name = "ReCodeItForm";
        Text = "ReCodeIt V0.1.0 - PRC0";
        TabPageRemapper.ResumeLayout(false);
        groupBox1.ResumeLayout(false);
        groupBox1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)ConstuctorCountUpDown).EndInit();
        Inclusions.ResumeLayout(false);
        tabPage1.ResumeLayout(false);
        tabPage1.PerformLayout();
        tabPage2.ResumeLayout(false);
        tabPage2.PerformLayout();
        tabPage3.ResumeLayout(false);
        tabPage3.PerformLayout();
        tabPage4.ResumeLayout(false);
        tabPage4.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)PropertyCountUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)FieldCountUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)MethodCountUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)NestedTypeCountUpDown).EndInit();
        TabControlMain.ResumeLayout(false);
        AutoMapperTab.ResumeLayout(false);
        AutoMapperTab.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)AutoMapperMinLengthUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)AutoMapperRequiredMatchesUpDown).EndInit();
        tabPage5.ResumeLayout(false);
        groupBox4.ResumeLayout(false);
        groupBox4.PerformLayout();
        groupBox5.ResumeLayout(false);
        groupBox5.PerformLayout();
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        SettingsTab.ResumeLayout(false);
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        ResumeLayout(false);
    }

    #endregion
    private TabPage TabPageRemapper;
    private GroupBox groupBox1;
    private TabControl Inclusions;
    private TabPage tabPage1;
    private TextBox ExcludeMethodTextBox;
    private TextBox IncludeMethodTextBox;
    private Button MethodExcludeRemoveButton;
    private Button MethodExcludeAddButton;
    private Button MethodIncludeRemoveButton;
    private Button MethodIncludeAddButton;
    private ListBox MethodExcludeBox;
    private ListBox MethodIncludeBox;
    private TabPage tabPage2;
    private NumericUpDown FieldCountUpDown;
    private TextBox FieldsExcludeTextInput;
    private TextBox FieldsIncludeTextInput;
    private Button FieldExcludeRemoveButton;
    private Button FieldExcludeAddButton;
    private Button FieldIncludeRemoveButton;
    private Button FIeldIncludeAddButton;
    private ListBox FieldExcludeBox;
    private ListBox FieldIncludeBox;
    private TabPage tabPage3;
    private NumericUpDown PropertyCountUpDown;
    private TextBox PropertiesExcludeTextField;
    private TextBox PropertiesIncludeTextField;
    private Button PropertiesExcludeRemoveButton;
    private Button PropertiesExcludeAddButton;
    private Button PropertiesIncludeRemoveButton;
    private Button PropertiesIncludeAddButton;
    private ListBox PropertiesExcludeBox;
    private ListBox PropertiesIncludeBox;
    private TabPage tabPage4;
    private NumericUpDown NestedTypeCountUpDown;
    private TextBox BaseClassExcludeTextField;
    private TextBox NestedTypeParentName;
    private TextBox BaseClassIncludeTextFIeld;
    private TextBox NestedTypesExcludeTextField;
    private TextBox NestedTypesIncludeTextField;
    private Button NestedTypesExcludeRemoveButton;
    private Button NestedTypesExlcudeAddButton;
    private Button NestedTypesRemoveButton;
    private Button NestedTypesAddButton;
    private ListBox NestedTypesExcludeBox;
    private ListBox NestedTypesIncludeBox;
    private CheckBox ForceRenameCheckbox;
    private TextBox OriginalTypeName;
    private TextBox NewTypeName;
    private Button RemoveRemapButton;
    private Button SaveRemapButton;
    private ListView RemapListView;
    private TabControl TabControlMain;
    private DomainUpDown IsPublicUpDown;
    private DomainUpDown HasGenericParametersUpDown;
    private DomainUpDown IsDerivedUpDown;
    private DomainUpDown HasAttributeUpDown;
    private DomainUpDown IsSealedUpDown;
    private DomainUpDown IsNestedUpDown;
    private DomainUpDown IsEnumUpDown;
    private DomainUpDown IsAbstractUpDown;
    private CheckBox FieldCountEnabled;
    private CheckBox PropertyCountEnabled;
    private CheckBox NestedTypeCountEnabled;
    private DomainUpDown IsInterfaceUpDown;
    private TreeView RemapTreeView;
    private Button RunRemapButton;
    private Button LoadMappingFileButton;
    private NumericUpDown ConstuctorCountUpDown;
    private CheckBox ConstructorCountEnabled;
    private NumericUpDown MethodCountUpDown;
    private CheckBox MethodCountEnabled;
    private TabPage SettingsTab;
    private GroupBox groupBox2;
    private CheckBox SilentModeCheckbox;
    private CheckBox DebugLoggingCheckbox;
    private TabPage AutoMapperTab;
    private TextBox AutoMapperTypesToIgnoreTextField;
    private Button AutoMapperExcludeTypesRemoveButton;
    private Button AutoMapperExcludeTypesAddButton;
    private ListBox AutoMapperTypesExcludeBox;
    private Label label2;
    private NumericUpDown AutoMapperRequiredMatchesUpDown;
    private Button RunAutoRemapButton;
    private Label label3;
    private NumericUpDown AutoMapperMinLengthUpDown;
    private TextBox AutoMapperTokensTextField;
    private Button AutoMapperTokensRemoveButton;
    private Button AutoMapperTokensAddButton;
    private ListBox AutoMapperTokensBox;
    private TextBox AutoMapperFPTextField;
    private Button AutoMapperFPRemoveButton;
    private Button AutoMapperFPAddButton;
    private ListBox AutoMapperFPBox;
    private CheckBox AutoMapperSearchMethodsCheckBox;
    private TextBox AutoMapperMethodTextBox;
    private Button AutoMapperMethodRemoveButton;
    private Button AutoMapperMethodAddButton;
    private ListBox AutoMapperMethodBox;
    private TabPage tabPage5;
    private Button PickNameMangledPathButton;
    private TextBox NameMangledAssemblyTextBox;
    private GroupBox groupBox3;
    private Button CrossPatchRemapButton;
    private CheckBox UnsealCheckbox;
    private CheckBox RenamePropertiesCheckbox;
    private CheckBox PublicizeCheckbox;
    private CheckBox RenameFieldsCheckbox;
    private TextBox AssemblyPathTextBox;
    private TextBox OutputPathTextBox;
    private Button OutputDirectoryButton;
    private TextBox MappingPathTextBox;
    private Button PickAssemblyPathButton;
    private CheckBox RemapperUseForceRename;
    private CheckBox RemapperUnseal;
    private CheckBox RemapperPublicicize;
    private TextBox RemapperOutputDirectoryPath;
    private TextBox TargetAssemblyPath;
    private TextBox AutoMapperOuputPath;
    private TextBox AutoMapperTargetPath;
    private Button AutoMapperChooseOutpathButton;
    private Button AutoMapperChooseTargetPathButton;
    private CheckBox AutoMapperUnseal;
    private CheckBox AutoMapperPublicize;
    private CheckBox AutoMapperRenameProps;
    private CheckBox AutoMapperRenameFields;
    private Button CrossPatchRunButton;
    private GroupBox groupBox4;
    private TextBox LoadedMappingFilePath;
    private Button CrossCompilerNewProjectButton;
    private Button CCRemappedOutputButton;
    private TextBox CCRemappedOutputText;
    private TextBox CCOriginalAssemblyText;
    private TextBox CCVisualStudioProjDirText;
    private Button CCVisualStudioProjDirButton;
    private Button CCOriginalAssemblyButton;
    private TextBox CCBuildDirText;
    private Button CCBuildDirButton;
    private CheckBox ActiveProjectMappingsCheckbox;
    private Label label4;
    private TreeView CCMappingTreeView;
    private Button CCLoadProjButton;
    private CheckBox CCAutoLoadLastProj;
    private GroupBox groupBox5;
    private TextBox CCBuildConfiguration;
    private LinkLabel GithubLinkLabel;
    private Button CCImportMappings;
}
