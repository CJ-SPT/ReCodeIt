﻿namespace AssemblyRemapperGUI;

partial class AssemblyToolGUI
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
        components = new System.ComponentModel.Container();
        bindingSource1 = new BindingSource(components);
        TabPageRemapper = new TabPage();
        RemapTreeView = new TreeView();
        groupBox1 = new GroupBox();
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
        AddRemapButton = new Button();
        IsDerivedUpDown = new DomainUpDown();
        IsNestedUpDown = new DomainUpDown();
        HasAttributeUpDown = new DomainUpDown();
        BaseClassExcludeTextField = new TextBox();
        MethodCountEnabled = new CheckBox();
        IsSealedUpDown = new DomainUpDown();
        TabControlMain = new TabControl();
        colorDialog1 = new ColorDialog();
        ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
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
        RemapTreeView.Location = new Point(781, 20);
        RemapTreeView.Name = "RemapTreeView";
        RemapTreeView.Size = new Size(487, 890);
        RemapTreeView.TabIndex = 1;
        // 
        // groupBox1
        // 
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
        groupBox1.Controls.Add(AddRemapButton);
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
        // ConstuctorCountUpDown
        // 
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
        LoadMappingFileButton.Location = new Point(601, 489);
        LoadMappingFileButton.Name = "LoadMappingFileButton";
        LoadMappingFileButton.Size = new Size(168, 34);
        LoadMappingFileButton.TabIndex = 18;
        LoadMappingFileButton.Text = "Load Mapping File";
        LoadMappingFileButton.UseVisualStyleBackColor = true;
        LoadMappingFileButton.Click += LoadMappingFileButton_Click;
        // 
        // SaveMappingFileButton
        // 
        SaveMappingFileButton.Location = new Point(427, 489);
        SaveMappingFileButton.Name = "SaveMappingFileButton";
        SaveMappingFileButton.Size = new Size(168, 34);
        SaveMappingFileButton.TabIndex = 17;
        SaveMappingFileButton.Text = "Save Mapping File";
        SaveMappingFileButton.UseVisualStyleBackColor = true;
        SaveMappingFileButton.Click += SaveMappingFileButton_Click;
        // 
        // RunRemapButton
        // 
        RunRemapButton.Location = new Point(580, 145);
        RunRemapButton.Name = "RunRemapButton";
        RunRemapButton.Size = new Size(168, 34);
        RunRemapButton.TabIndex = 16;
        RunRemapButton.Text = "Run Remap";
        RunRemapButton.UseVisualStyleBackColor = true;
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
        ExcludeMethodTextBox.Location = new Point(381, 6);
        ExcludeMethodTextBox.Name = "ExcludeMethodTextBox";
        ExcludeMethodTextBox.PlaceholderText = "Exclude Methods";
        ExcludeMethodTextBox.Size = new Size(353, 31);
        ExcludeMethodTextBox.TabIndex = 21;
        // 
        // IncludeMethodTextBox
        // 
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
        MethodExcludeBox.FormattingEnabled = true;
        MethodExcludeBox.ItemHeight = 25;
        MethodExcludeBox.Location = new Point(381, 43);
        MethodExcludeBox.Name = "MethodExcludeBox";
        MethodExcludeBox.Size = new Size(353, 229);
        MethodExcludeBox.TabIndex = 15;
        // 
        // MethodIncludeBox
        // 
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
        FieldsExcludeTextInput.Location = new Point(381, 6);
        FieldsExcludeTextInput.Name = "FieldsExcludeTextInput";
        FieldsExcludeTextInput.PlaceholderText = "Exclude Fields";
        FieldsExcludeTextInput.Size = new Size(353, 31);
        FieldsExcludeTextInput.TabIndex = 27;
        // 
        // FieldsIncludeTextInput
        // 
        FieldsIncludeTextInput.Location = new Point(6, 6);
        FieldsIncludeTextInput.Name = "FieldsIncludeTextInput";
        FieldsIncludeTextInput.PlaceholderText = "Include Fields";
        FieldsIncludeTextInput.Size = new Size(353, 31);
        FieldsIncludeTextInput.TabIndex = 26;
        // 
        // FieldExcludeRemoveButton
        // 
        FieldExcludeRemoveButton.Location = new Point(622, 278);
        FieldExcludeRemoveButton.Name = "FieldExcludeRemoveButton";
        FieldExcludeRemoveButton.Size = new Size(112, 34);
        FieldExcludeRemoveButton.TabIndex = 25;
        FieldExcludeRemoveButton.Text = "Remove";
        FieldExcludeRemoveButton.UseVisualStyleBackColor = true;
        FieldExcludeRemoveButton.Click += FieldExcludeRemoveButton_Click;
        // 
        // FieldExcludeAddButton
        // 
        FieldExcludeAddButton.Location = new Point(381, 278);
        FieldExcludeAddButton.Name = "FieldExcludeAddButton";
        FieldExcludeAddButton.Size = new Size(112, 34);
        FieldExcludeAddButton.TabIndex = 24;
        FieldExcludeAddButton.Text = "Add";
        FieldExcludeAddButton.UseVisualStyleBackColor = true;
        FieldExcludeAddButton.Click += FieldExcludeAddButton_Click;
        // 
        // FieldIncludeRemoveButton
        // 
        FieldIncludeRemoveButton.Location = new Point(247, 278);
        FieldIncludeRemoveButton.Name = "FieldIncludeRemoveButton";
        FieldIncludeRemoveButton.Size = new Size(112, 34);
        FieldIncludeRemoveButton.TabIndex = 23;
        FieldIncludeRemoveButton.Text = "Remove";
        FieldIncludeRemoveButton.UseVisualStyleBackColor = true;
        FieldIncludeRemoveButton.Click += FieldIncludeRemoveButton_Click;
        // 
        // FIeldIncludeAddButton
        // 
        FIeldIncludeAddButton.Location = new Point(6, 278);
        FIeldIncludeAddButton.Name = "FIeldIncludeAddButton";
        FIeldIncludeAddButton.Size = new Size(112, 34);
        FIeldIncludeAddButton.TabIndex = 22;
        FIeldIncludeAddButton.Text = "Add";
        FIeldIncludeAddButton.UseVisualStyleBackColor = true;
        FIeldIncludeAddButton.Click += FIeldIncludeAddButton_Click;
        // 
        // FieldExcludeBox
        // 
        FieldExcludeBox.FormattingEnabled = true;
        FieldExcludeBox.ItemHeight = 25;
        FieldExcludeBox.Location = new Point(382, 43);
        FieldExcludeBox.Name = "FieldExcludeBox";
        FieldExcludeBox.Size = new Size(353, 229);
        FieldExcludeBox.TabIndex = 17;
        // 
        // FieldIncludeBox
        // 
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
        PropertiesExcludeTextField.Location = new Point(381, 6);
        PropertiesExcludeTextField.Name = "PropertiesExcludeTextField";
        PropertiesExcludeTextField.PlaceholderText = "Exclude Properties";
        PropertiesExcludeTextField.Size = new Size(353, 31);
        PropertiesExcludeTextField.TabIndex = 27;
        // 
        // PropertiesIncludeTextField
        // 
        PropertiesIncludeTextField.Location = new Point(6, 6);
        PropertiesIncludeTextField.Name = "PropertiesIncludeTextField";
        PropertiesIncludeTextField.PlaceholderText = "Include Properties";
        PropertiesIncludeTextField.Size = new Size(353, 31);
        PropertiesIncludeTextField.TabIndex = 26;
        // 
        // PropertiesExcludeRemoveButton
        // 
        PropertiesExcludeRemoveButton.Location = new Point(622, 278);
        PropertiesExcludeRemoveButton.Name = "PropertiesExcludeRemoveButton";
        PropertiesExcludeRemoveButton.Size = new Size(112, 34);
        PropertiesExcludeRemoveButton.TabIndex = 25;
        PropertiesExcludeRemoveButton.Text = "Remove";
        PropertiesExcludeRemoveButton.UseVisualStyleBackColor = true;
        PropertiesExcludeRemoveButton.Click += PropertiesExcludeRemoveButton_Click;
        // 
        // PropertiesExcludeAddButton
        // 
        PropertiesExcludeAddButton.Location = new Point(381, 278);
        PropertiesExcludeAddButton.Name = "PropertiesExcludeAddButton";
        PropertiesExcludeAddButton.Size = new Size(112, 34);
        PropertiesExcludeAddButton.TabIndex = 24;
        PropertiesExcludeAddButton.Text = "Add";
        PropertiesExcludeAddButton.UseVisualStyleBackColor = true;
        PropertiesExcludeAddButton.Click += PropertiesExcludeAddButton_Click;
        // 
        // PropertiesIncludeRemoveButton
        // 
        PropertiesIncludeRemoveButton.Location = new Point(247, 278);
        PropertiesIncludeRemoveButton.Name = "PropertiesIncludeRemoveButton";
        PropertiesIncludeRemoveButton.Size = new Size(112, 34);
        PropertiesIncludeRemoveButton.TabIndex = 23;
        PropertiesIncludeRemoveButton.Text = "Remove";
        PropertiesIncludeRemoveButton.UseVisualStyleBackColor = true;
        PropertiesIncludeRemoveButton.Click += PropertiesIncludeRemoveButton_Click;
        // 
        // PropertiesIncludeAddButton
        // 
        PropertiesIncludeAddButton.Location = new Point(6, 278);
        PropertiesIncludeAddButton.Name = "PropertiesIncludeAddButton";
        PropertiesIncludeAddButton.Size = new Size(112, 34);
        PropertiesIncludeAddButton.TabIndex = 22;
        PropertiesIncludeAddButton.Text = "Add";
        PropertiesIncludeAddButton.UseVisualStyleBackColor = true;
        PropertiesIncludeAddButton.Click += PropertiesIncludeAddButton_Click;
        // 
        // PropertiesExcludeBox
        // 
        PropertiesExcludeBox.FormattingEnabled = true;
        PropertiesExcludeBox.ItemHeight = 25;
        PropertiesExcludeBox.Location = new Point(381, 43);
        PropertiesExcludeBox.Name = "PropertiesExcludeBox";
        PropertiesExcludeBox.Size = new Size(353, 229);
        PropertiesExcludeBox.TabIndex = 19;
        // 
        // PropertiesIncludeBox
        // 
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
        NestedTypesExcludeTextField.Location = new Point(381, 6);
        NestedTypesExcludeTextField.Name = "NestedTypesExcludeTextField";
        NestedTypesExcludeTextField.PlaceholderText = "Exclude Nested Types";
        NestedTypesExcludeTextField.Size = new Size(353, 31);
        NestedTypesExcludeTextField.TabIndex = 27;
        // 
        // NestedTypesIncludeTextField
        // 
        NestedTypesIncludeTextField.Location = new Point(6, 6);
        NestedTypesIncludeTextField.Name = "NestedTypesIncludeTextField";
        NestedTypesIncludeTextField.PlaceholderText = "Include Nested Types";
        NestedTypesIncludeTextField.Size = new Size(353, 31);
        NestedTypesIncludeTextField.TabIndex = 26;
        // 
        // NestedTypesExcludeRemoveButton
        // 
        NestedTypesExcludeRemoveButton.Location = new Point(622, 278);
        NestedTypesExcludeRemoveButton.Name = "NestedTypesExcludeRemoveButton";
        NestedTypesExcludeRemoveButton.Size = new Size(112, 34);
        NestedTypesExcludeRemoveButton.TabIndex = 25;
        NestedTypesExcludeRemoveButton.Text = "Remove";
        NestedTypesExcludeRemoveButton.UseVisualStyleBackColor = true;
        NestedTypesExcludeRemoveButton.Click += NestedTypesExcludeRemoveButton_Click;
        // 
        // NestedTypesExlcudeAddButton
        // 
        NestedTypesExlcudeAddButton.Location = new Point(381, 278);
        NestedTypesExlcudeAddButton.Name = "NestedTypesExlcudeAddButton";
        NestedTypesExlcudeAddButton.Size = new Size(112, 34);
        NestedTypesExlcudeAddButton.TabIndex = 24;
        NestedTypesExlcudeAddButton.Text = "Add";
        NestedTypesExlcudeAddButton.UseVisualStyleBackColor = true;
        NestedTypesExlcudeAddButton.Click += NestedTypesExlcudeAddButton_Click;
        // 
        // NestedTypesRemoveButton
        // 
        NestedTypesRemoveButton.Location = new Point(247, 278);
        NestedTypesRemoveButton.Name = "NestedTypesRemoveButton";
        NestedTypesRemoveButton.Size = new Size(112, 34);
        NestedTypesRemoveButton.TabIndex = 23;
        NestedTypesRemoveButton.Text = "Remove";
        NestedTypesRemoveButton.UseVisualStyleBackColor = true;
        NestedTypesRemoveButton.Click += NestedTypesRemoveButton_Click;
        // 
        // NestedTypesAddButton
        // 
        NestedTypesAddButton.Location = new Point(6, 278);
        NestedTypesAddButton.Name = "NestedTypesAddButton";
        NestedTypesAddButton.Size = new Size(112, 34);
        NestedTypesAddButton.TabIndex = 22;
        NestedTypesAddButton.Text = "Add";
        NestedTypesAddButton.UseVisualStyleBackColor = true;
        NestedTypesAddButton.Click += NestedTypesAddButton_Click;
        // 
        // NestedTypesExcludeBox
        // 
        NestedTypesExcludeBox.FormattingEnabled = true;
        NestedTypesExcludeBox.ItemHeight = 25;
        NestedTypesExcludeBox.Location = new Point(381, 43);
        NestedTypesExcludeBox.Name = "NestedTypesExcludeBox";
        NestedTypesExcludeBox.Size = new Size(353, 229);
        NestedTypesExcludeBox.TabIndex = 21;
        // 
        // NestedTypesIncludeBox
        // 
        NestedTypesIncludeBox.FormattingEnabled = true;
        NestedTypesIncludeBox.ItemHeight = 25;
        NestedTypesIncludeBox.Location = new Point(6, 43);
        NestedTypesIncludeBox.Name = "NestedTypesIncludeBox";
        NestedTypesIncludeBox.Size = new Size(353, 229);
        NestedTypesIncludeBox.TabIndex = 20;
        // 
        // NewTypeName
        // 
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
        PropertyCountUpDown.Location = new Point(224, 291);
        PropertyCountUpDown.Name = "PropertyCountUpDown";
        PropertyCountUpDown.Size = new Size(55, 31);
        PropertyCountUpDown.TabIndex = 5;
        // 
        // RemoveRemapButton
        // 
        RemoveRemapButton.Location = new Point(580, 105);
        RemoveRemapButton.Name = "RemoveRemapButton";
        RemoveRemapButton.Size = new Size(168, 34);
        RemoveRemapButton.TabIndex = 2;
        RemoveRemapButton.Text = "Remove Remap";
        RemoveRemapButton.UseVisualStyleBackColor = true;
        RemoveRemapButton.Click += RemoveRemapButton_Click;
        // 
        // FieldCountUpDown
        // 
        FieldCountUpDown.Location = new Point(224, 253);
        FieldCountUpDown.Name = "FieldCountUpDown";
        FieldCountUpDown.Size = new Size(55, 31);
        FieldCountUpDown.TabIndex = 3;
        // 
        // IsPublicUpDown
        // 
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
        NestedTypeParentName.Location = new Point(224, 106);
        NestedTypeParentName.Name = "NestedTypeParentName";
        NestedTypeParentName.PlaceholderText = "Nested Type Parent Name";
        NestedTypeParentName.Size = new Size(208, 31);
        NestedTypeParentName.TabIndex = 0;
        // 
        // MethodCountUpDown
        // 
        MethodCountUpDown.Location = new Point(224, 215);
        MethodCountUpDown.Name = "MethodCountUpDown";
        MethodCountUpDown.Size = new Size(55, 31);
        MethodCountUpDown.TabIndex = 6;
        // 
        // IsAbstractUpDown
        // 
        IsAbstractUpDown.Location = new Point(10, 144);
        IsAbstractUpDown.Name = "IsAbstractUpDown";
        IsAbstractUpDown.Size = new Size(208, 31);
        IsAbstractUpDown.Sorted = true;
        IsAbstractUpDown.TabIndex = 1;
        IsAbstractUpDown.Text = "IsAbstract";
        // 
        // BaseClassIncludeTextFIeld
        // 
        BaseClassIncludeTextFIeld.Location = new Point(224, 67);
        BaseClassIncludeTextFIeld.Name = "BaseClassIncludeTextFIeld";
        BaseClassIncludeTextFIeld.PlaceholderText = "Include Base Class";
        BaseClassIncludeTextFIeld.Size = new Size(208, 31);
        BaseClassIncludeTextFIeld.TabIndex = 2;
        // 
        // OriginalTypeName
        // 
        OriginalTypeName.Location = new Point(224, 30);
        OriginalTypeName.Name = "OriginalTypeName";
        OriginalTypeName.PlaceholderText = "Original Type Name";
        OriginalTypeName.Size = new Size(208, 31);
        OriginalTypeName.TabIndex = 1;
        // 
        // HasGenericParametersUpDown
        // 
        HasGenericParametersUpDown.Location = new Point(10, 366);
        HasGenericParametersUpDown.Name = "HasGenericParametersUpDown";
        HasGenericParametersUpDown.Size = new Size(208, 31);
        HasGenericParametersUpDown.Sorted = true;
        HasGenericParametersUpDown.TabIndex = 7;
        HasGenericParametersUpDown.Text = "HasGenericParameters";
        // 
        // IsEnumUpDown
        // 
        IsEnumUpDown.Location = new Point(10, 255);
        IsEnumUpDown.Name = "IsEnumUpDown";
        IsEnumUpDown.Size = new Size(208, 31);
        IsEnumUpDown.Sorted = true;
        IsEnumUpDown.TabIndex = 2;
        IsEnumUpDown.Text = "IsEnum";
        // 
        // NestedTypeCountUpDown
        // 
        NestedTypeCountUpDown.Location = new Point(224, 329);
        NestedTypeCountUpDown.Name = "NestedTypeCountUpDown";
        NestedTypeCountUpDown.Size = new Size(55, 31);
        NestedTypeCountUpDown.TabIndex = 4;
        // 
        // AddRemapButton
        // 
        AddRemapButton.Location = new Point(580, 64);
        AddRemapButton.Name = "AddRemapButton";
        AddRemapButton.Size = new Size(168, 34);
        AddRemapButton.TabIndex = 4;
        AddRemapButton.Text = "Add Remap";
        AddRemapButton.UseVisualStyleBackColor = true;
        AddRemapButton.Click += AddRemapButton_Click;
        // 
        // IsDerivedUpDown
        // 
        IsDerivedUpDown.Location = new Point(10, 329);
        IsDerivedUpDown.Name = "IsDerivedUpDown";
        IsDerivedUpDown.Size = new Size(208, 31);
        IsDerivedUpDown.Sorted = true;
        IsDerivedUpDown.TabIndex = 6;
        IsDerivedUpDown.Text = "IsDerived";
        // 
        // IsNestedUpDown
        // 
        IsNestedUpDown.Location = new Point(10, 68);
        IsNestedUpDown.Name = "IsNestedUpDown";
        IsNestedUpDown.Size = new Size(208, 31);
        IsNestedUpDown.Sorted = true;
        IsNestedUpDown.TabIndex = 3;
        IsNestedUpDown.Text = "IsNested";
        // 
        // HasAttributeUpDown
        // 
        HasAttributeUpDown.Location = new Point(10, 292);
        HasAttributeUpDown.Name = "HasAttributeUpDown";
        HasAttributeUpDown.Size = new Size(208, 31);
        HasAttributeUpDown.Sorted = true;
        HasAttributeUpDown.TabIndex = 5;
        HasAttributeUpDown.Text = "HasAttribute";
        // 
        // BaseClassExcludeTextField
        // 
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
        TabControlMain.Location = new Point(-5, 1);
        TabControlMain.Name = "TabControlMain";
        TabControlMain.SelectedIndex = 0;
        TabControlMain.Size = new Size(1344, 991);
        TabControlMain.TabIndex = 6;
        // 
        // AssemblyToolGUI
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ControlDarkDark;
        ClientSize = new Size(1297, 966);
        Controls.Add(TabControlMain);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Name = "AssemblyToolGUI";
        Text = "Cj's Assembly Tool V0.1.0";
        ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
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
        ResumeLayout(false);
    }

    #endregion
    private BindingSource bindingSource1;
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
    private Button AddRemapButton;
    private ListView RemapListView;
    private TabControl TabControlMain;
    private DomainUpDown IsPublicUpDown;
    private ColorDialog colorDialog1;
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
}
