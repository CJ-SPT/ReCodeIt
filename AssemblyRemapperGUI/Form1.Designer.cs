namespace AssemblyRemapperGUI;

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
        groupBox1 = new GroupBox();
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
        groupBox6 = new GroupBox();
        BaseClassExcludeTextField = new TextBox();
        NestedTypeParentName = new TextBox();
        BaseClassIncludeTextFIeld = new TextBox();
        NestedTypesExcludeTextField = new TextBox();
        NestedTypesIncludeTextField = new TextBox();
        NestedTypesExcludeRemoveButton = new Button();
        NestedTypesExlcudeAddButton = new Button();
        NestedTypesRemoveButton = new Button();
        NestedTypesAddButton = new Button();
        NestedTypesExcludeBox = new ListBox();
        NestedTypesIncludeBox = new ListBox();
        ScoreButton = new Button();
        groupBox3 = new GroupBox();
        NestedTypeCountUpDown = new NumericUpDown();
        PropertiesCountUpDown = new NumericUpDown();
        FieldCountUpDown = new NumericUpDown();
        MethodCountUpDown = new NumericUpDown();
        HasGenericParametersUpDown = new DomainUpDown();
        IsDerivedUpDown = new DomainUpDown();
        HasAttributeUpDown = new DomainUpDown();
        IsSealedUpDown = new DomainUpDown();
        domainUpDown4 = new DomainUpDown();
        IsEnumUpDown = new DomainUpDown();
        IsAbstractUpDown = new DomainUpDown();
        IsPublicUpDown = new DomainUpDown();
        RemoveRemapButton = new Button();
        AddRemapButton = new Button();
        groupBox2 = new GroupBox();
        ForceRenameCheckbox = new CheckBox();
        OriginalTypeName = new TextBox();
        NewTypeName = new TextBox();
        RemapListView = new ListView();
        TabControlMain = new TabControl();
        menuStrip1 = new MenuStrip();
        SettingsButton = new ToolStripMenuItem();
        colorDialog1 = new ColorDialog();
        PropertyCountEnabled = new CheckBox();
        NestedTypeCountEnabled = new CheckBox();
        FieldCountEnabled = new CheckBox();
        MethodCountEnabled = new CheckBox();
        ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
        TabPageRemapper.SuspendLayout();
        groupBox1.SuspendLayout();
        Inclusions.SuspendLayout();
        tabPage1.SuspendLayout();
        tabPage2.SuspendLayout();
        tabPage3.SuspendLayout();
        tabPage4.SuspendLayout();
        groupBox6.SuspendLayout();
        groupBox3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)NestedTypeCountUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)PropertiesCountUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)FieldCountUpDown).BeginInit();
        ((System.ComponentModel.ISupportInitialize)MethodCountUpDown).BeginInit();
        groupBox2.SuspendLayout();
        TabControlMain.SuspendLayout();
        menuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // TabPageRemapper
        // 
        TabPageRemapper.BackColor = SystemColors.ControlDarkDark;
        TabPageRemapper.Controls.Add(groupBox1);
        TabPageRemapper.Controls.Add(RemapListView);
        TabPageRemapper.Location = new Point(4, 34);
        TabPageRemapper.Name = "TabPageRemapper";
        TabPageRemapper.Padding = new Padding(3);
        TabPageRemapper.Size = new Size(1695, 1055);
        TabPageRemapper.TabIndex = 1;
        TabPageRemapper.Text = "Remapper";
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(Inclusions);
        groupBox1.Controls.Add(ScoreButton);
        groupBox1.Controls.Add(groupBox3);
        groupBox1.Controls.Add(RemoveRemapButton);
        groupBox1.Controls.Add(AddRemapButton);
        groupBox1.Controls.Add(groupBox2);
        groupBox1.Location = new Point(6, 11);
        groupBox1.Name = "groupBox1";
        groupBox1.Size = new Size(769, 1058);
        groupBox1.TabIndex = 0;
        groupBox1.TabStop = false;
        groupBox1.Text = "Remap Editor";
        // 
        // Inclusions
        // 
        Inclusions.Controls.Add(tabPage1);
        Inclusions.Controls.Add(tabPage2);
        Inclusions.Controls.Add(tabPage3);
        Inclusions.Controls.Add(tabPage4);
        Inclusions.Location = new Point(6, 315);
        Inclusions.Name = "Inclusions";
        Inclusions.SelectedIndex = 0;
        Inclusions.Size = new Size(751, 478);
        Inclusions.TabIndex = 14;
        // 
        // tabPage1
        // 
        tabPage1.BackColor = SystemColors.ControlDarkDark;
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
        tabPage1.Size = new Size(743, 520);
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
        tabPage2.Size = new Size(743, 520);
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
        tabPage3.Size = new Size(743, 520);
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
        tabPage4.Controls.Add(groupBox6);
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
        tabPage4.Size = new Size(743, 440);
        tabPage4.TabIndex = 3;
        tabPage4.Text = "Other";
        // 
        // groupBox6
        // 
        groupBox6.Controls.Add(BaseClassExcludeTextField);
        groupBox6.Controls.Add(NestedTypeParentName);
        groupBox6.Controls.Add(BaseClassIncludeTextFIeld);
        groupBox6.Location = new Point(6, 328);
        groupBox6.Name = "groupBox6";
        groupBox6.Size = new Size(737, 422);
        groupBox6.TabIndex = 28;
        groupBox6.TabStop = false;
        groupBox6.Text = "Other";
        // 
        // BaseClassExcludeTextField
        // 
        BaseClassExcludeTextField.Location = new Point(6, 67);
        BaseClassExcludeTextField.Name = "BaseClassExcludeTextField";
        BaseClassExcludeTextField.PlaceholderText = "Exclude Base Class";
        BaseClassExcludeTextField.Size = new Size(239, 31);
        BaseClassExcludeTextField.TabIndex = 1;
        // 
        // NestedTypeParentName
        // 
        NestedTypeParentName.Location = new Point(252, 30);
        NestedTypeParentName.Name = "NestedTypeParentName";
        NestedTypeParentName.PlaceholderText = "Nested Type Parent Name";
        NestedTypeParentName.Size = new Size(239, 31);
        NestedTypeParentName.TabIndex = 0;
        // 
        // BaseClassIncludeTextFIeld
        // 
        BaseClassIncludeTextFIeld.Location = new Point(7, 30);
        BaseClassIncludeTextFIeld.Name = "BaseClassIncludeTextFIeld";
        BaseClassIncludeTextFIeld.PlaceholderText = "Include Base Class";
        BaseClassIncludeTextFIeld.Size = new Size(239, 31);
        BaseClassIncludeTextFIeld.TabIndex = 2;
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
        // ScoreButton
        // 
        ScoreButton.Location = new Point(357, 799);
        ScoreButton.Name = "ScoreButton";
        ScoreButton.Size = new Size(168, 34);
        ScoreButton.TabIndex = 5;
        ScoreButton.Text = "Score Remap";
        ScoreButton.UseVisualStyleBackColor = true;
        ScoreButton.Click += ScoreButton_Click;
        // 
        // groupBox3
        // 
        groupBox3.Controls.Add(MethodCountEnabled);
        groupBox3.Controls.Add(NestedTypeCountUpDown);
        groupBox3.Controls.Add(FieldCountEnabled);
        groupBox3.Controls.Add(PropertiesCountUpDown);
        groupBox3.Controls.Add(PropertyCountEnabled);
        groupBox3.Controls.Add(NestedTypeCountEnabled);
        groupBox3.Controls.Add(FieldCountUpDown);
        groupBox3.Controls.Add(MethodCountUpDown);
        groupBox3.Controls.Add(HasGenericParametersUpDown);
        groupBox3.Controls.Add(IsDerivedUpDown);
        groupBox3.Controls.Add(HasAttributeUpDown);
        groupBox3.Controls.Add(IsSealedUpDown);
        groupBox3.Controls.Add(domainUpDown4);
        groupBox3.Controls.Add(IsEnumUpDown);
        groupBox3.Controls.Add(IsAbstractUpDown);
        groupBox3.Controls.Add(IsPublicUpDown);
        groupBox3.Location = new Point(6, 124);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(751, 185);
        groupBox3.TabIndex = 4;
        groupBox3.TabStop = false;
        groupBox3.Text = "General";
        // 
        // NestedTypeCountUpDown
        // 
        NestedTypeCountUpDown.Location = new Point(474, 141);
        NestedTypeCountUpDown.Name = "NestedTypeCountUpDown";
        NestedTypeCountUpDown.Size = new Size(55, 31);
        NestedTypeCountUpDown.TabIndex = 4;
        // 
        // PropertiesCountUpDown
        // 
        PropertiesCountUpDown.Location = new Point(474, 104);
        PropertiesCountUpDown.Name = "PropertiesCountUpDown";
        PropertiesCountUpDown.Size = new Size(55, 31);
        PropertiesCountUpDown.TabIndex = 5;
        // 
        // FieldCountUpDown
        // 
        FieldCountUpDown.Location = new Point(474, 67);
        FieldCountUpDown.Name = "FieldCountUpDown";
        FieldCountUpDown.Size = new Size(55, 31);
        FieldCountUpDown.TabIndex = 3;
        // 
        // MethodCountUpDown
        // 
        MethodCountUpDown.Location = new Point(474, 30);
        MethodCountUpDown.Name = "MethodCountUpDown";
        MethodCountUpDown.Size = new Size(55, 31);
        MethodCountUpDown.TabIndex = 6;
        // 
        // HasGenericParametersUpDown
        // 
        HasGenericParametersUpDown.Items.Add("Disabled");
        HasGenericParametersUpDown.Items.Add("False");
        HasGenericParametersUpDown.Items.Add("True");
        HasGenericParametersUpDown.Location = new Point(224, 141);
        HasGenericParametersUpDown.Name = "HasGenericParametersUpDown";
        HasGenericParametersUpDown.Size = new Size(208, 31);
        HasGenericParametersUpDown.Sorted = true;
        HasGenericParametersUpDown.TabIndex = 7;
        HasGenericParametersUpDown.Text = "HasGenericParameters";
        // 
        // IsDerivedUpDown
        // 
        IsDerivedUpDown.Items.Add("Disabled");
        IsDerivedUpDown.Items.Add("False");
        IsDerivedUpDown.Items.Add("True");
        IsDerivedUpDown.Location = new Point(224, 104);
        IsDerivedUpDown.Name = "IsDerivedUpDown";
        IsDerivedUpDown.Size = new Size(208, 31);
        IsDerivedUpDown.Sorted = true;
        IsDerivedUpDown.TabIndex = 6;
        IsDerivedUpDown.Text = "IsDerived";
        // 
        // HasAttributeUpDown
        // 
        HasAttributeUpDown.Items.Add("Disabled");
        HasAttributeUpDown.Items.Add("False");
        HasAttributeUpDown.Items.Add("True");
        HasAttributeUpDown.Location = new Point(224, 67);
        HasAttributeUpDown.Name = "HasAttributeUpDown";
        HasAttributeUpDown.Size = new Size(208, 31);
        HasAttributeUpDown.Sorted = true;
        HasAttributeUpDown.TabIndex = 5;
        HasAttributeUpDown.Text = "HasAttribute";
        // 
        // IsSealedUpDown
        // 
        IsSealedUpDown.Items.Add("Disabled");
        IsSealedUpDown.Items.Add("False");
        IsSealedUpDown.Items.Add("True");
        IsSealedUpDown.Location = new Point(224, 30);
        IsSealedUpDown.Name = "IsSealedUpDown";
        IsSealedUpDown.Size = new Size(208, 31);
        IsSealedUpDown.Sorted = true;
        IsSealedUpDown.TabIndex = 4;
        IsSealedUpDown.Text = "IsSealed";
        // 
        // domainUpDown4
        // 
        domainUpDown4.Items.Add("Disabled");
        domainUpDown4.Items.Add("False");
        domainUpDown4.Items.Add("True");
        domainUpDown4.Location = new Point(10, 141);
        domainUpDown4.Name = "domainUpDown4";
        domainUpDown4.Size = new Size(208, 31);
        domainUpDown4.Sorted = true;
        domainUpDown4.TabIndex = 3;
        domainUpDown4.Text = "IsNested";
        // 
        // IsEnumUpDown
        // 
        IsEnumUpDown.Items.Add("Disabled");
        IsEnumUpDown.Items.Add("False");
        IsEnumUpDown.Items.Add("True");
        IsEnumUpDown.Location = new Point(10, 104);
        IsEnumUpDown.Name = "IsEnumUpDown";
        IsEnumUpDown.Size = new Size(208, 31);
        IsEnumUpDown.Sorted = true;
        IsEnumUpDown.TabIndex = 2;
        IsEnumUpDown.Text = "IsEnum";
        // 
        // IsAbstractUpDown
        // 
        IsAbstractUpDown.Items.Add("Disabled");
        IsAbstractUpDown.Items.Add("False");
        IsAbstractUpDown.Items.Add("True");
        IsAbstractUpDown.Location = new Point(10, 67);
        IsAbstractUpDown.Name = "IsAbstractUpDown";
        IsAbstractUpDown.Size = new Size(208, 31);
        IsAbstractUpDown.Sorted = true;
        IsAbstractUpDown.TabIndex = 1;
        IsAbstractUpDown.Text = "IsAbstract";
        // 
        // IsPublicUpDown
        // 
        IsPublicUpDown.Items.Add("Disabled");
        IsPublicUpDown.Items.Add("False");
        IsPublicUpDown.Items.Add("True");
        IsPublicUpDown.Location = new Point(10, 30);
        IsPublicUpDown.Name = "IsPublicUpDown";
        IsPublicUpDown.Size = new Size(208, 31);
        IsPublicUpDown.Sorted = true;
        IsPublicUpDown.TabIndex = 0;
        IsPublicUpDown.Text = "IsPublic";
        // 
        // RemoveRemapButton
        // 
        RemoveRemapButton.Location = new Point(183, 799);
        RemoveRemapButton.Name = "RemoveRemapButton";
        RemoveRemapButton.Size = new Size(168, 34);
        RemoveRemapButton.TabIndex = 2;
        RemoveRemapButton.Text = "Remove Remap";
        RemoveRemapButton.UseVisualStyleBackColor = true;
        RemoveRemapButton.Click += RemoveRemapButton_Click;
        // 
        // AddRemapButton
        // 
        AddRemapButton.Location = new Point(9, 799);
        AddRemapButton.Name = "AddRemapButton";
        AddRemapButton.Size = new Size(168, 34);
        AddRemapButton.TabIndex = 4;
        AddRemapButton.Text = "Add Remap";
        AddRemapButton.UseVisualStyleBackColor = true;
        AddRemapButton.Click += AddRemapButton_Click;
        // 
        // groupBox2
        // 
        groupBox2.Controls.Add(ForceRenameCheckbox);
        groupBox2.Controls.Add(OriginalTypeName);
        groupBox2.Controls.Add(NewTypeName);
        groupBox2.Location = new Point(6, 30);
        groupBox2.Name = "groupBox2";
        groupBox2.Size = new Size(757, 79);
        groupBox2.TabIndex = 3;
        groupBox2.TabStop = false;
        groupBox2.Text = "Main settings";
        // 
        // ForceRenameCheckbox
        // 
        ForceRenameCheckbox.AutoSize = true;
        ForceRenameCheckbox.Location = new Point(568, 32);
        ForceRenameCheckbox.Name = "ForceRenameCheckbox";
        ForceRenameCheckbox.Size = new Size(183, 29);
        ForceRenameCheckbox.TabIndex = 2;
        ForceRenameCheckbox.Text = "Use Force Rename";
        ForceRenameCheckbox.UseVisualStyleBackColor = true;
        // 
        // OriginalTypeName
        // 
        OriginalTypeName.Location = new Point(288, 30);
        OriginalTypeName.Name = "OriginalTypeName";
        OriginalTypeName.PlaceholderText = "Original Type Name";
        OriginalTypeName.Size = new Size(250, 31);
        OriginalTypeName.TabIndex = 1;
        // 
        // NewTypeName
        // 
        NewTypeName.Location = new Point(6, 30);
        NewTypeName.Name = "NewTypeName";
        NewTypeName.PlaceholderText = "New Type Name";
        NewTypeName.Size = new Size(250, 31);
        NewTypeName.TabIndex = 0;
        // 
        // RemapListView
        // 
        RemapListView.BackColor = SystemColors.AppWorkspace;
        RemapListView.Location = new Point(781, 21);
        RemapListView.Name = "RemapListView";
        RemapListView.Size = new Size(899, 446);
        RemapListView.TabIndex = 0;
        RemapListView.UseCompatibleStateImageBehavior = false;
        // 
        // TabControlMain
        // 
        TabControlMain.Controls.Add(TabPageRemapper);
        TabControlMain.Location = new Point(12, 40);
        TabControlMain.Name = "TabControlMain";
        TabControlMain.SelectedIndex = 0;
        TabControlMain.Size = new Size(1703, 1093);
        TabControlMain.TabIndex = 6;
        // 
        // menuStrip1
        // 
        menuStrip1.ImageScalingSize = new Size(24, 24);
        menuStrip1.Items.AddRange(new ToolStripItem[] { SettingsButton });
        menuStrip1.Location = new Point(0, 0);
        menuStrip1.Name = "menuStrip1";
        menuStrip1.Size = new Size(1708, 33);
        menuStrip1.TabIndex = 7;
        menuStrip1.Text = "menuStrip1";
        // 
        // SettingsButton
        // 
        SettingsButton.Name = "SettingsButton";
        SettingsButton.Size = new Size(92, 29);
        SettingsButton.Text = "Settings";
        // 
        // PropertyCountEnabled
        // 
        PropertyCountEnabled.AutoSize = true;
        PropertyCountEnabled.Location = new Point(537, 106);
        PropertyCountEnabled.Name = "PropertyCountEnabled";
        PropertyCountEnabled.Size = new Size(159, 29);
        PropertyCountEnabled.TabIndex = 11;
        PropertyCountEnabled.Text = "Property Count";
        PropertyCountEnabled.UseVisualStyleBackColor = true;
        // 
        // NestedTypeCountEnabled
        // 
        NestedTypeCountEnabled.AutoSize = true;
        NestedTypeCountEnabled.Location = new Point(537, 141);
        NestedTypeCountEnabled.Name = "NestedTypeCountEnabled";
        NestedTypeCountEnabled.Size = new Size(189, 29);
        NestedTypeCountEnabled.TabIndex = 12;
        NestedTypeCountEnabled.Text = "Nested Type Count";
        NestedTypeCountEnabled.UseVisualStyleBackColor = true;
        // 
        // FieldCountEnabled
        // 
        FieldCountEnabled.AutoSize = true;
        FieldCountEnabled.Location = new Point(537, 71);
        FieldCountEnabled.Name = "FieldCountEnabled";
        FieldCountEnabled.Size = new Size(128, 29);
        FieldCountEnabled.TabIndex = 13;
        FieldCountEnabled.Text = "Field Count";
        FieldCountEnabled.UseVisualStyleBackColor = true;
        // 
        // MethodCountEnabled
        // 
        MethodCountEnabled.AutoSize = true;
        MethodCountEnabled.Location = new Point(537, 30);
        MethodCountEnabled.Name = "MethodCountEnabled";
        MethodCountEnabled.Size = new Size(154, 29);
        MethodCountEnabled.TabIndex = 14;
        MethodCountEnabled.Text = "Method Count";
        MethodCountEnabled.UseVisualStyleBackColor = true;
        // 
        // AssemblyToolGUI
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ControlDarkDark;
        ClientSize = new Size(1708, 1133);
        Controls.Add(TabControlMain);
        Controls.Add(menuStrip1);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Name = "AssemblyToolGUI";
        Text = "Cj's Assembly Tool V0.1.0";
        ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
        TabPageRemapper.ResumeLayout(false);
        groupBox1.ResumeLayout(false);
        Inclusions.ResumeLayout(false);
        tabPage1.ResumeLayout(false);
        tabPage1.PerformLayout();
        tabPage2.ResumeLayout(false);
        tabPage2.PerformLayout();
        tabPage3.ResumeLayout(false);
        tabPage3.PerformLayout();
        tabPage4.ResumeLayout(false);
        tabPage4.PerformLayout();
        groupBox6.ResumeLayout(false);
        groupBox6.PerformLayout();
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)NestedTypeCountUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)PropertiesCountUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)FieldCountUpDown).EndInit();
        ((System.ComponentModel.ISupportInitialize)MethodCountUpDown).EndInit();
        groupBox2.ResumeLayout(false);
        groupBox2.PerformLayout();
        TabControlMain.ResumeLayout(false);
        menuStrip1.ResumeLayout(false);
        menuStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
    private BindingSource bindingSource1;
    private TabPage TabPageRemapper;
    private GroupBox groupBox1;
    private TabControl Inclusions;
    private TabPage tabPage1;
    private NumericUpDown MethodCountUpDown;
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
    private NumericUpDown PropertiesCountUpDown;
    private TextBox PropertiesExcludeTextField;
    private TextBox PropertiesIncludeTextField;
    private Button PropertiesExcludeRemoveButton;
    private Button PropertiesExcludeAddButton;
    private Button PropertiesIncludeRemoveButton;
    private Button PropertiesIncludeAddButton;
    private ListBox PropertiesExcludeBox;
    private ListBox PropertiesIncludeBox;
    private TabPage tabPage4;
    private GroupBox groupBox6;
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
    private GroupBox groupBox3;
    private GroupBox groupBox2;
    private CheckBox ForceRenameCheckbox;
    private TextBox OriginalTypeName;
    private TextBox NewTypeName;
    private Button ScoreButton;
    private Button RemoveRemapButton;
    private Button AddRemapButton;
    private ListView RemapListView;
    private TabControl TabControlMain;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem SettingsButton;
    private DomainUpDown IsPublicUpDown;
    private ColorDialog colorDialog1;
    private DomainUpDown HasGenericParametersUpDown;
    private DomainUpDown IsDerivedUpDown;
    private DomainUpDown HasAttributeUpDown;
    private DomainUpDown IsSealedUpDown;
    private DomainUpDown domainUpDown4;
    private DomainUpDown IsEnumUpDown;
    private DomainUpDown IsAbstractUpDown;
    private CheckBox MethodCountEnabled;
    private CheckBox FieldCountEnabled;
    private CheckBox PropertyCountEnabled;
    private CheckBox NestedTypeCountEnabled;
}
