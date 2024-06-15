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
        EditRemapButton = new Button();
        ConstuctorCountUpDown = new NumericUpDown();
        ConstructorCountEnabled = new CheckBox();
        LoadMappingFileButton = new Button();
        SaveMappingFileButton = new Button();
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
        ForceRenameCheckbox = new CheckBox();
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
        label2 = new Label();
        AutoMapperRequiredMatchesUpDown = new NumericUpDown();
        treeView1 = new TreeView();
        AutoMapperExcludeTextField = new TextBox();
        AutoMapperExcludeRemoveButton = new Button();
        AutoMapperExcludeAddButton = new Button();
        AutoMapperExcludeBox = new ListBox();
        SettingsTab = new TabPage();
        groupBox3 = new GroupBox();
        label1 = new Label();
        MaxMatchCountUpDown = new NumericUpDown();
        groupBox4 = new GroupBox();
        groupBox2 = new GroupBox();
        MappingChooseButton = new Button();
        UnsealCheckbox = new CheckBox();
        RenamePropertiesCheckbox = new CheckBox();
        PublicizeCheckbox = new CheckBox();
        OutputDirectoryButton = new Button();
        RenameFieldsCheckbox = new CheckBox();
        PickAssemblyPathButton = new Button();
        MappingPathTextBox = new TextBox();
        OutputPathTextBox = new TextBox();
        AssemblyPathTextBox = new TextBox();
        SilentModeCheckbox = new CheckBox();
        DebugLoggingCheckbox = new CheckBox();
        RunAutoRemapButton = new Button();
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
        ((System.ComponentModel.ISupportInitialize)AutoMapperRequiredMatchesUpDown).BeginInit();
        SettingsTab.SuspendLayout();
        groupBox3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)MaxMatchCountUpDown).BeginInit();
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
        groupBox1.Controls.Add(EditRemapButton);
        groupBox1.Controls.Add(ConstuctorCountUpDown);
        groupBox1.Controls.Add(ConstructorCountEnabled);
        groupBox1.Controls.Add(LoadMappingFileButton);
        groupBox1.Controls.Add(SaveMappingFileButton);
        groupBox1.Controls.Add(RunRemapButton);
        groupBox1.Controls.Add(Inclusions);
        groupBox1.Controls.Add(NewTypeName);
        groupBox1.Controls.Add(ForceRenameCheckbox);
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
        // EditRemapButton
        // 
        EditRemapButton.BackColor = SystemColors.ButtonShadow;
        EditRemapButton.Location = new Point(580, 145);
        EditRemapButton.Name = "EditRemapButton";
        EditRemapButton.Size = new Size(168, 34);
        EditRemapButton.TabIndex = 21;
        EditRemapButton.Text = "Edit Remap";
        EditRemapButton.UseVisualStyleBackColor = false;
        EditRemapButton.Click += EditRemapButton_Click;
        // 
        // ConstuctorCountUpDown
        // 
        ConstuctorCountUpDown.BackColor = SystemColors.ScrollBar;
        ConstuctorCountUpDown.Location = new Point(224, 178);
        ConstuctorCountUpDown.Name = "ConstuctorCountUpDown";
        ConstuctorCountUpDown.Size = new Size(55, 31);
        ConstuctorCountUpDown.TabIndex = 19;
        // 
        // ConstructorCountEnabled
        // 
        ConstructorCountEnabled.AutoSize = true;
        ConstructorCountEnabled.Location = new Point(287, 184);
        ConstructorCountEnabled.Name = "ConstructorCountEnabled";
        ConstructorCountEnabled.Size = new Size(238, 29);
        ConstructorCountEnabled.TabIndex = 20;
        ConstructorCountEnabled.Text = "Constructor Param Count";
        ConstructorCountEnabled.UseVisualStyleBackColor = true;
        // 
        // LoadMappingFileButton
        // 
        LoadMappingFileButton.BackColor = SystemColors.ButtonShadow;
        LoadMappingFileButton.Location = new Point(601, 489);
        LoadMappingFileButton.Name = "LoadMappingFileButton";
        LoadMappingFileButton.Size = new Size(168, 34);
        LoadMappingFileButton.TabIndex = 18;
        LoadMappingFileButton.Text = "Load Mapping File";
        LoadMappingFileButton.UseVisualStyleBackColor = false;
        LoadMappingFileButton.Click += LoadMappingFileButton_Click;
        // 
        // SaveMappingFileButton
        // 
        SaveMappingFileButton.BackColor = SystemColors.ButtonShadow;
        SaveMappingFileButton.Location = new Point(427, 489);
        SaveMappingFileButton.Name = "SaveMappingFileButton";
        SaveMappingFileButton.Size = new Size(168, 34);
        SaveMappingFileButton.TabIndex = 17;
        SaveMappingFileButton.Text = "Save Mapping File";
        SaveMappingFileButton.UseVisualStyleBackColor = false;
        SaveMappingFileButton.Click += SaveMappingFileButton_Click;
        // 
        // RunRemapButton
        // 
        RunRemapButton.BackColor = SystemColors.ButtonShadow;
        RunRemapButton.Location = new Point(580, 185);
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
        NewTypeName.Location = new Point(10, 30);
        NewTypeName.Name = "NewTypeName";
        NewTypeName.PlaceholderText = "New Type Name";
        NewTypeName.Size = new Size(208, 31);
        NewTypeName.TabIndex = 0;
        // 
        // ForceRenameCheckbox
        // 
        ForceRenameCheckbox.AutoSize = true;
        ForceRenameCheckbox.Location = new Point(580, 30);
        ForceRenameCheckbox.Name = "ForceRenameCheckbox";
        ForceRenameCheckbox.Size = new Size(183, 29);
        ForceRenameCheckbox.TabIndex = 2;
        ForceRenameCheckbox.Text = "Use Force Rename";
        ForceRenameCheckbox.UseVisualStyleBackColor = true;
        // 
        // PropertyCountEnabled
        // 
        PropertyCountEnabled.AutoSize = true;
        PropertyCountEnabled.Location = new Point(287, 291);
        PropertyCountEnabled.Name = "PropertyCountEnabled";
        PropertyCountEnabled.Size = new Size(159, 29);
        PropertyCountEnabled.TabIndex = 11;
        PropertyCountEnabled.Text = "Property Count";
        PropertyCountEnabled.UseVisualStyleBackColor = true;
        // 
        // IsInterfaceUpDown
        // 
        IsInterfaceUpDown.BackColor = SystemColors.ScrollBar;
        IsInterfaceUpDown.Location = new Point(10, 183);
        IsInterfaceUpDown.Name = "IsInterfaceUpDown";
        IsInterfaceUpDown.Size = new Size(208, 31);
        IsInterfaceUpDown.Sorted = true;
        IsInterfaceUpDown.TabIndex = 15;
        IsInterfaceUpDown.Text = "IsInterface";
        // 
        // NestedTypeCountEnabled
        // 
        NestedTypeCountEnabled.AutoSize = true;
        NestedTypeCountEnabled.Location = new Point(287, 326);
        NestedTypeCountEnabled.Name = "NestedTypeCountEnabled";
        NestedTypeCountEnabled.Size = new Size(189, 29);
        NestedTypeCountEnabled.TabIndex = 12;
        NestedTypeCountEnabled.Text = "Nested Type Count";
        NestedTypeCountEnabled.UseVisualStyleBackColor = true;
        // 
        // PropertyCountUpDown
        // 
        PropertyCountUpDown.BackColor = SystemColors.ScrollBar;
        PropertyCountUpDown.Location = new Point(224, 291);
        PropertyCountUpDown.Name = "PropertyCountUpDown";
        PropertyCountUpDown.Size = new Size(55, 31);
        PropertyCountUpDown.TabIndex = 5;
        // 
        // RemoveRemapButton
        // 
        RemoveRemapButton.BackColor = SystemColors.ButtonShadow;
        RemoveRemapButton.Location = new Point(580, 105);
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
        FieldCountUpDown.Location = new Point(224, 253);
        FieldCountUpDown.Name = "FieldCountUpDown";
        FieldCountUpDown.Size = new Size(55, 31);
        FieldCountUpDown.TabIndex = 3;
        // 
        // IsPublicUpDown
        // 
        IsPublicUpDown.BackColor = SystemColors.ScrollBar;
        IsPublicUpDown.Location = new Point(10, 107);
        IsPublicUpDown.Name = "IsPublicUpDown";
        IsPublicUpDown.Size = new Size(208, 31);
        IsPublicUpDown.Sorted = true;
        IsPublicUpDown.TabIndex = 0;
        IsPublicUpDown.Text = "IsPublic";
        // 
        // FieldCountEnabled
        // 
        FieldCountEnabled.AutoSize = true;
        FieldCountEnabled.Location = new Point(287, 256);
        FieldCountEnabled.Name = "FieldCountEnabled";
        FieldCountEnabled.Size = new Size(128, 29);
        FieldCountEnabled.TabIndex = 13;
        FieldCountEnabled.Text = "Field Count";
        FieldCountEnabled.UseVisualStyleBackColor = true;
        // 
        // NestedTypeParentName
        // 
        NestedTypeParentName.BackColor = SystemColors.ScrollBar;
        NestedTypeParentName.Location = new Point(224, 106);
        NestedTypeParentName.Name = "NestedTypeParentName";
        NestedTypeParentName.PlaceholderText = "Nested Type Parent Name";
        NestedTypeParentName.Size = new Size(208, 31);
        NestedTypeParentName.TabIndex = 0;
        // 
        // MethodCountUpDown
        // 
        MethodCountUpDown.BackColor = SystemColors.ScrollBar;
        MethodCountUpDown.Location = new Point(224, 215);
        MethodCountUpDown.Name = "MethodCountUpDown";
        MethodCountUpDown.Size = new Size(55, 31);
        MethodCountUpDown.TabIndex = 6;
        // 
        // IsAbstractUpDown
        // 
        IsAbstractUpDown.BackColor = SystemColors.ScrollBar;
        IsAbstractUpDown.Location = new Point(10, 144);
        IsAbstractUpDown.Name = "IsAbstractUpDown";
        IsAbstractUpDown.Size = new Size(208, 31);
        IsAbstractUpDown.Sorted = true;
        IsAbstractUpDown.TabIndex = 1;
        IsAbstractUpDown.Text = "IsAbstract";
        // 
        // BaseClassIncludeTextFIeld
        // 
        BaseClassIncludeTextFIeld.BackColor = SystemColors.ScrollBar;
        BaseClassIncludeTextFIeld.Location = new Point(224, 67);
        BaseClassIncludeTextFIeld.Name = "BaseClassIncludeTextFIeld";
        BaseClassIncludeTextFIeld.PlaceholderText = "Include Base Class";
        BaseClassIncludeTextFIeld.Size = new Size(208, 31);
        BaseClassIncludeTextFIeld.TabIndex = 2;
        // 
        // OriginalTypeName
        // 
        OriginalTypeName.BackColor = SystemColors.ScrollBar;
        OriginalTypeName.Location = new Point(224, 30);
        OriginalTypeName.Name = "OriginalTypeName";
        OriginalTypeName.PlaceholderText = "Original Type Name";
        OriginalTypeName.Size = new Size(208, 31);
        OriginalTypeName.TabIndex = 1;
        // 
        // HasGenericParametersUpDown
        // 
        HasGenericParametersUpDown.BackColor = SystemColors.ScrollBar;
        HasGenericParametersUpDown.Location = new Point(10, 366);
        HasGenericParametersUpDown.Name = "HasGenericParametersUpDown";
        HasGenericParametersUpDown.Size = new Size(208, 31);
        HasGenericParametersUpDown.Sorted = true;
        HasGenericParametersUpDown.TabIndex = 7;
        HasGenericParametersUpDown.Text = "HasGenericParameters";
        // 
        // IsEnumUpDown
        // 
        IsEnumUpDown.BackColor = SystemColors.ScrollBar;
        IsEnumUpDown.Location = new Point(10, 255);
        IsEnumUpDown.Name = "IsEnumUpDown";
        IsEnumUpDown.Size = new Size(208, 31);
        IsEnumUpDown.Sorted = true;
        IsEnumUpDown.TabIndex = 2;
        IsEnumUpDown.Text = "IsEnum";
        // 
        // NestedTypeCountUpDown
        // 
        NestedTypeCountUpDown.BackColor = SystemColors.ScrollBar;
        NestedTypeCountUpDown.Location = new Point(224, 329);
        NestedTypeCountUpDown.Name = "NestedTypeCountUpDown";
        NestedTypeCountUpDown.Size = new Size(55, 31);
        NestedTypeCountUpDown.TabIndex = 4;
        // 
        // SaveRemapButton
        // 
        SaveRemapButton.BackColor = SystemColors.ButtonShadow;
        SaveRemapButton.Location = new Point(580, 64);
        SaveRemapButton.Name = "SaveRemapButton";
        SaveRemapButton.Size = new Size(168, 34);
        SaveRemapButton.TabIndex = 4;
        SaveRemapButton.Text = "Save Remap";
        SaveRemapButton.UseVisualStyleBackColor = false;
        SaveRemapButton.Click += AddRemapButton_Click;
        // 
        // IsDerivedUpDown
        // 
        IsDerivedUpDown.BackColor = SystemColors.ScrollBar;
        IsDerivedUpDown.Location = new Point(10, 329);
        IsDerivedUpDown.Name = "IsDerivedUpDown";
        IsDerivedUpDown.Size = new Size(208, 31);
        IsDerivedUpDown.Sorted = true;
        IsDerivedUpDown.TabIndex = 6;
        IsDerivedUpDown.Text = "IsDerived";
        // 
        // IsNestedUpDown
        // 
        IsNestedUpDown.BackColor = SystemColors.ScrollBar;
        IsNestedUpDown.Location = new Point(10, 68);
        IsNestedUpDown.Name = "IsNestedUpDown";
        IsNestedUpDown.Size = new Size(208, 31);
        IsNestedUpDown.Sorted = true;
        IsNestedUpDown.TabIndex = 3;
        IsNestedUpDown.Text = "IsNested";
        // 
        // HasAttributeUpDown
        // 
        HasAttributeUpDown.BackColor = SystemColors.ScrollBar;
        HasAttributeUpDown.Location = new Point(10, 292);
        HasAttributeUpDown.Name = "HasAttributeUpDown";
        HasAttributeUpDown.Size = new Size(208, 31);
        HasAttributeUpDown.Sorted = true;
        HasAttributeUpDown.TabIndex = 5;
        HasAttributeUpDown.Text = "HasAttribute";
        // 
        // BaseClassExcludeTextField
        // 
        BaseClassExcludeTextField.BackColor = SystemColors.ScrollBar;
        BaseClassExcludeTextField.Location = new Point(224, 145);
        BaseClassExcludeTextField.Name = "BaseClassExcludeTextField";
        BaseClassExcludeTextField.PlaceholderText = "Exclude Base Class";
        BaseClassExcludeTextField.Size = new Size(208, 31);
        BaseClassExcludeTextField.TabIndex = 1;
        // 
        // MethodCountEnabled
        // 
        MethodCountEnabled.AutoSize = true;
        MethodCountEnabled.Location = new Point(287, 221);
        MethodCountEnabled.Name = "MethodCountEnabled";
        MethodCountEnabled.Size = new Size(154, 29);
        MethodCountEnabled.TabIndex = 14;
        MethodCountEnabled.Text = "Method Count";
        MethodCountEnabled.UseVisualStyleBackColor = true;
        // 
        // IsSealedUpDown
        // 
        IsSealedUpDown.BackColor = SystemColors.ScrollBar;
        IsSealedUpDown.Location = new Point(10, 218);
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
        AutoMapperTab.Controls.Add(RunAutoRemapButton);
        AutoMapperTab.Controls.Add(label2);
        AutoMapperTab.Controls.Add(AutoMapperRequiredMatchesUpDown);
        AutoMapperTab.Controls.Add(treeView1);
        AutoMapperTab.Controls.Add(AutoMapperExcludeTextField);
        AutoMapperTab.Controls.Add(AutoMapperExcludeRemoveButton);
        AutoMapperTab.Controls.Add(AutoMapperExcludeAddButton);
        AutoMapperTab.Controls.Add(AutoMapperExcludeBox);
        AutoMapperTab.Location = new Point(4, 34);
        AutoMapperTab.Name = "AutoMapperTab";
        AutoMapperTab.Padding = new Padding(3);
        AutoMapperTab.Size = new Size(1336, 953);
        AutoMapperTab.TabIndex = 3;
        AutoMapperTab.Text = "Auto Mapper";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(76, 60);
        label2.Name = "label2";
        label2.Size = new Size(153, 25);
        label2.TabIndex = 27;
        label2.Text = "Required Matches";
        // 
        // AutoMapperRequiredMatchesUpDown
        // 
        AutoMapperRequiredMatchesUpDown.Location = new Point(13, 58);
        AutoMapperRequiredMatchesUpDown.Name = "AutoMapperRequiredMatchesUpDown";
        AutoMapperRequiredMatchesUpDown.Size = new Size(57, 31);
        AutoMapperRequiredMatchesUpDown.TabIndex = 26;
        // 
        // treeView1
        // 
        treeView1.Location = new Point(918, 18);
        treeView1.Name = "treeView1";
        treeView1.Size = new Size(368, 901);
        treeView1.TabIndex = 25;
        // 
        // AutoMapperExcludeTextField
        // 
        AutoMapperExcludeTextField.BackColor = SystemColors.ScrollBar;
        AutoMapperExcludeTextField.Location = new Point(6, 173);
        AutoMapperExcludeTextField.Name = "AutoMapperExcludeTextField";
        AutoMapperExcludeTextField.PlaceholderText = "Exclude Names";
        AutoMapperExcludeTextField.Size = new Size(353, 31);
        AutoMapperExcludeTextField.TabIndex = 24;
        // 
        // AutoMapperExcludeRemoveButton
        // 
        AutoMapperExcludeRemoveButton.Location = new Point(247, 445);
        AutoMapperExcludeRemoveButton.Name = "AutoMapperExcludeRemoveButton";
        AutoMapperExcludeRemoveButton.Size = new Size(112, 34);
        AutoMapperExcludeRemoveButton.TabIndex = 23;
        AutoMapperExcludeRemoveButton.Text = "Remove";
        AutoMapperExcludeRemoveButton.UseVisualStyleBackColor = true;
        AutoMapperExcludeRemoveButton.Click += AutoMapperExcludeRemoveButton_Click;
        // 
        // AutoMapperExcludeAddButton
        // 
        AutoMapperExcludeAddButton.Location = new Point(6, 445);
        AutoMapperExcludeAddButton.Name = "AutoMapperExcludeAddButton";
        AutoMapperExcludeAddButton.Size = new Size(112, 34);
        AutoMapperExcludeAddButton.TabIndex = 22;
        AutoMapperExcludeAddButton.Text = "Add";
        AutoMapperExcludeAddButton.UseVisualStyleBackColor = true;
        AutoMapperExcludeAddButton.Click += AutoMapperExcludeAddButton_Click;
        // 
        // AutoMapperExcludeBox
        // 
        AutoMapperExcludeBox.BackColor = Color.Gray;
        AutoMapperExcludeBox.FormattingEnabled = true;
        AutoMapperExcludeBox.ItemHeight = 25;
        AutoMapperExcludeBox.Location = new Point(6, 210);
        AutoMapperExcludeBox.Name = "AutoMapperExcludeBox";
        AutoMapperExcludeBox.Size = new Size(353, 229);
        AutoMapperExcludeBox.TabIndex = 21;
        // 
        // SettingsTab
        // 
        SettingsTab.BackColor = SystemColors.ControlDarkDark;
        SettingsTab.Controls.Add(groupBox3);
        SettingsTab.Controls.Add(groupBox4);
        SettingsTab.Controls.Add(groupBox2);
        SettingsTab.Location = new Point(4, 34);
        SettingsTab.Name = "SettingsTab";
        SettingsTab.Padding = new Padding(3);
        SettingsTab.Size = new Size(1336, 953);
        SettingsTab.TabIndex = 2;
        SettingsTab.Text = "Settings";
        // 
        // groupBox3
        // 
        groupBox3.Controls.Add(label1);
        groupBox3.Controls.Add(MaxMatchCountUpDown);
        groupBox3.Location = new Point(464, 6);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(259, 285);
        groupBox3.TabIndex = 1;
        groupBox3.TabStop = false;
        groupBox3.Text = "ReCodeItRemapper Settings";
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(69, 37);
        label1.Name = "label1";
        label1.Size = new Size(152, 25);
        label1.TabIndex = 5;
        label1.Text = "Max Match Count";
        // 
        // MaxMatchCountUpDown
        // 
        MaxMatchCountUpDown.Location = new Point(6, 35);
        MaxMatchCountUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        MaxMatchCountUpDown.Name = "MaxMatchCountUpDown";
        MaxMatchCountUpDown.Size = new Size(57, 31);
        MaxMatchCountUpDown.TabIndex = 0;
        MaxMatchCountUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
        MaxMatchCountUpDown.ValueChanged += MaxMatchCountUpDown_ValueChanged;
        // 
        // groupBox4
        // 
        groupBox4.Location = new Point(729, 6);
        groupBox4.Name = "groupBox4";
        groupBox4.Size = new Size(557, 285);
        groupBox4.TabIndex = 1;
        groupBox4.TabStop = false;
        groupBox4.Text = "Auto Mapper Settings";
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(MappingChooseButton);
        groupBox2.Controls.Add(UnsealCheckbox);
        groupBox2.Controls.Add(RenamePropertiesCheckbox);
        groupBox2.Controls.Add(PublicizeCheckbox);
        groupBox2.Controls.Add(OutputDirectoryButton);
        groupBox2.Controls.Add(RenameFieldsCheckbox);
        groupBox2.Controls.Add(PickAssemblyPathButton);
        groupBox2.Controls.Add(MappingPathTextBox);
        groupBox2.Controls.Add(OutputPathTextBox);
        groupBox2.Controls.Add(AssemblyPathTextBox);
        groupBox2.Controls.Add(SilentModeCheckbox);
        groupBox2.Controls.Add(DebugLoggingCheckbox);
        groupBox2.Location = new Point(13, 6);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(445, 285);
        groupBox2.TabIndex = 0;
        groupBox2.TabStop = false;
        groupBox2.Text = "App Settings";
        // 
        // MappingChooseButton
        // 
        MappingChooseButton.Location = new Point(308, 171);
        MappingChooseButton.Name = "MappingChooseButton";
        MappingChooseButton.Size = new Size(112, 34);
        MappingChooseButton.TabIndex = 8;
        MappingChooseButton.Text = "Choose";
        MappingChooseButton.UseVisualStyleBackColor = true;
        MappingChooseButton.Click += MappingChooseButton_Click;
        // 
        // UnsealCheckbox
        // 
        UnsealCheckbox.AutoSize = true;
        UnsealCheckbox.Checked = true;
        UnsealCheckbox.CheckState = CheckState.Checked;
        UnsealCheckbox.Location = new Point(196, 246);
        UnsealCheckbox.Name = "UnsealCheckbox";
        UnsealCheckbox.Size = new Size(90, 29);
        UnsealCheckbox.TabIndex = 2;
        UnsealCheckbox.Text = "Unseal";
        UnsealCheckbox.UseVisualStyleBackColor = true;
        UnsealCheckbox.CheckedChanged += UnsealCheckbox_CheckedChanged;
        // 
        // RenamePropertiesCheckbox
        // 
        RenamePropertiesCheckbox.AutoSize = true;
        RenamePropertiesCheckbox.Checked = true;
        RenamePropertiesCheckbox.CheckState = CheckState.Checked;
        RenamePropertiesCheckbox.Location = new Point(6, 246);
        RenamePropertiesCheckbox.Name = "RenamePropertiesCheckbox";
        RenamePropertiesCheckbox.Size = new Size(186, 29);
        RenamePropertiesCheckbox.TabIndex = 4;
        RenamePropertiesCheckbox.Text = "Rename Properties";
        RenamePropertiesCheckbox.UseVisualStyleBackColor = true;
        RenamePropertiesCheckbox.CheckedChanged += RenamePropertiesCheckbox_CheckedChanged;
        // 
        // PublicizeCheckbox
        // 
        PublicizeCheckbox.AutoSize = true;
        PublicizeCheckbox.Checked = true;
        PublicizeCheckbox.CheckState = CheckState.Checked;
        PublicizeCheckbox.Location = new Point(196, 211);
        PublicizeCheckbox.Name = "PublicizeCheckbox";
        PublicizeCheckbox.Size = new Size(106, 29);
        PublicizeCheckbox.TabIndex = 1;
        PublicizeCheckbox.Text = "Publicize";
        PublicizeCheckbox.UseVisualStyleBackColor = true;
        PublicizeCheckbox.CheckedChanged += PublicizeCheckbox_CheckedChanged;
        // 
        // OutputDirectoryButton
        // 
        OutputDirectoryButton.Location = new Point(308, 134);
        OutputDirectoryButton.Name = "OutputDirectoryButton";
        OutputDirectoryButton.Size = new Size(112, 34);
        OutputDirectoryButton.TabIndex = 7;
        OutputDirectoryButton.Text = "Choose";
        OutputDirectoryButton.UseVisualStyleBackColor = true;
        OutputDirectoryButton.Click += OutputDirectoryButton_Click;
        // 
        // RenameFieldsCheckbox
        // 
        RenameFieldsCheckbox.AutoSize = true;
        RenameFieldsCheckbox.Checked = true;
        RenameFieldsCheckbox.CheckState = CheckState.Checked;
        RenameFieldsCheckbox.Location = new Point(6, 211);
        RenameFieldsCheckbox.Name = "RenameFieldsCheckbox";
        RenameFieldsCheckbox.Size = new Size(151, 29);
        RenameFieldsCheckbox.TabIndex = 3;
        RenameFieldsCheckbox.Text = "Rename Fields";
        RenameFieldsCheckbox.UseVisualStyleBackColor = true;
        RenameFieldsCheckbox.CheckedChanged += RenameFieldsCheckbox_CheckedChanged;
        // 
        // PickAssemblyPathButton
        // 
        PickAssemblyPathButton.Location = new Point(308, 100);
        PickAssemblyPathButton.Name = "PickAssemblyPathButton";
        PickAssemblyPathButton.Size = new Size(112, 34);
        PickAssemblyPathButton.TabIndex = 6;
        PickAssemblyPathButton.Text = "Choose";
        PickAssemblyPathButton.UseVisualStyleBackColor = true;
        PickAssemblyPathButton.Click += PickAssemblyPathButton_Click;
        // 
        // MappingPathTextBox
        // 
        MappingPathTextBox.Location = new Point(6, 174);
        MappingPathTextBox.Name = "MappingPathTextBox";
        MappingPathTextBox.PlaceholderText = "Mapping.json path";
        MappingPathTextBox.ReadOnly = true;
        MappingPathTextBox.Size = new Size(296, 31);
        MappingPathTextBox.TabIndex = 5;
        // 
        // OutputPathTextBox
        // 
        OutputPathTextBox.Location = new Point(6, 137);
        OutputPathTextBox.Name = "OutputPathTextBox";
        OutputPathTextBox.PlaceholderText = "Output Directory";
        OutputPathTextBox.ReadOnly = true;
        OutputPathTextBox.Size = new Size(296, 31);
        OutputPathTextBox.TabIndex = 4;
        // 
        // AssemblyPathTextBox
        // 
        AssemblyPathTextBox.Location = new Point(6, 100);
        AssemblyPathTextBox.Name = "AssemblyPathTextBox";
        AssemblyPathTextBox.PlaceholderText = "Assembly Path (Including file name)";
        AssemblyPathTextBox.ReadOnly = true;
        AssemblyPathTextBox.Size = new Size(296, 31);
        AssemblyPathTextBox.TabIndex = 3;
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
        // RunAutoRemapButton
        // 
        RunAutoRemapButton.Location = new Point(6, 3);
        RunAutoRemapButton.Name = "RunAutoRemapButton";
        RunAutoRemapButton.Size = new Size(168, 34);
        RunAutoRemapButton.TabIndex = 28;
        RunAutoRemapButton.Text = "Run Auto Remap";
        RunAutoRemapButton.UseVisualStyleBackColor = true;
        RunAutoRemapButton.Click += RunAutoRemapButton_Click;
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
        Text = "ReCodeIt V0.1.0";
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
        ((System.ComponentModel.ISupportInitialize)AutoMapperRequiredMatchesUpDown).EndInit();
        SettingsTab.ResumeLayout(false);
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)MaxMatchCountUpDown).EndInit();
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
    private Button SaveMappingFileButton;
    private Button RunRemapButton;
    private Button LoadMappingFileButton;
    private NumericUpDown ConstuctorCountUpDown;
    private CheckBox ConstructorCountEnabled;
    private NumericUpDown MethodCountUpDown;
    private CheckBox MethodCountEnabled;
    private TabPage SettingsTab;
    private GroupBox groupBox3;
    private GroupBox groupBox4;
    private GroupBox groupBox2;
    private CheckBox SilentModeCheckbox;
    private CheckBox DebugLoggingCheckbox;
    private Button MappingChooseButton;
    private Button OutputDirectoryButton;
    private Button PickAssemblyPathButton;
    private TextBox MappingPathTextBox;
    private TextBox OutputPathTextBox;
    private TextBox AssemblyPathTextBox;
    private CheckBox RenamePropertiesCheckbox;
    private CheckBox RenameFieldsCheckbox;
    private CheckBox UnsealCheckbox;
    private CheckBox PublicizeCheckbox;
    private NumericUpDown MaxMatchCountUpDown;
    private Label label1;
    private Button EditRemapButton;
    private TabPage AutoMapperTab;
    private TreeView treeView1;
    private TextBox AutoMapperExcludeTextField;
    private Button AutoMapperExcludeRemoveButton;
    private Button AutoMapperExcludeAddButton;
    private ListBox AutoMapperExcludeBox;
    private Label label2;
    private NumericUpDown AutoMapperRequiredMatchesUpDown;
    private Button RunAutoRemapButton;
}
