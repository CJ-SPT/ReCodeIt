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
        groupBox4 = new GroupBox();
        MethodCountUpDown = new NumericUpDown();
        label4 = new Label();
        ExcludeMethodTextBox = new TextBox();
        IncludeMethodTextBox = new TextBox();
        MethodExcludeRemoveButton = new Button();
        MethodExcludeAddButton = new Button();
        MethodIncludeRemoveButton = new Button();
        MethodIncludeAddButton = new Button();
        MethodExcludeBox = new ListBox();
        MethodIncludeBox = new ListBox();
        tabPage2 = new TabPage();
        groupBox5 = new GroupBox();
        FieldCountUpDown = new NumericUpDown();
        label1 = new Label();
        FieldsExcludeTextInput = new TextBox();
        FieldsIncludeTextInput = new TextBox();
        FieldExcludeRemoveButton = new Button();
        FieldExcludeAddButton = new Button();
        FieldIncludeRemoveButton = new Button();
        FIeldIncludeAddButton = new Button();
        FieldExcludeBox = new ListBox();
        FieldIncludeBox = new ListBox();
        tabPage3 = new TabPage();
        groupBox7 = new GroupBox();
        PropertiesCountUpDown = new NumericUpDown();
        label3 = new Label();
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
        NestedTypeCountUpDown = new NumericUpDown();
        BaseClassExcludeTextField = new TextBox();
        NestedTypeParentName = new TextBox();
        label2 = new Label();
        BaseClassIncludeTextFIeld = new TextBox();
        NestedTypesExcludeTextField = new TextBox();
        NestedTypesIncludeTextField = new TextBox();
        NestedTypesExcludeRemoveButton = new Button();
        NestedTypesExlcudeAddButton = new Button();
        NestedTypesRemoveButton = new Button();
        NestedTypesAddButton = new Button();
        NestedTypesExcludeBox = new ListBox();
        NestedTypesIncludeBox = new ListBox();
        groupBox3 = new GroupBox();
        IsSealed = new CheckBox();
        IsInterface = new CheckBox();
        IsAbstract = new CheckBox();
        HasGenericParameters = new CheckBox();
        IsPublic = new CheckBox();
        IsDerived = new CheckBox();
        IsEnum = new CheckBox();
        IsNested = new CheckBox();
        HasAttribute = new CheckBox();
        groupBox2 = new GroupBox();
        ForceRenameCheckbox = new CheckBox();
        OriginalTypeName = new TextBox();
        NewTypeName = new TextBox();
        ScoreButton = new Button();
        RemoveRemapButton = new Button();
        AddRemapButton = new Button();
        RemapListView = new ListView();
        TabControlMain = new TabControl();
        menuStrip1 = new MenuStrip();
        SettingsButton = new ToolStripMenuItem();
        ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
        TabPageRemapper.SuspendLayout();
        groupBox1.SuspendLayout();
        Inclusions.SuspendLayout();
        tabPage1.SuspendLayout();
        groupBox4.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)MethodCountUpDown).BeginInit();
        tabPage2.SuspendLayout();
        groupBox5.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)FieldCountUpDown).BeginInit();
        tabPage3.SuspendLayout();
        groupBox7.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)PropertiesCountUpDown).BeginInit();
        tabPage4.SuspendLayout();
        groupBox6.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)NestedTypeCountUpDown).BeginInit();
        groupBox3.SuspendLayout();
        groupBox2.SuspendLayout();
        TabControlMain.SuspendLayout();
        menuStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // TabPageRemapper
        // 
        TabPageRemapper.BackColor = SystemColors.ControlDarkDark;
        TabPageRemapper.Controls.Add(groupBox1);
        TabPageRemapper.Controls.Add(ScoreButton);
        TabPageRemapper.Controls.Add(RemoveRemapButton);
        TabPageRemapper.Controls.Add(AddRemapButton);
        TabPageRemapper.Controls.Add(RemapListView);
        TabPageRemapper.Location = new Point(4, 34);
        TabPageRemapper.Name = "TabPageRemapper";
        TabPageRemapper.Padding = new Padding(3);
        TabPageRemapper.Size = new Size(1695, 990);
        TabPageRemapper.TabIndex = 1;
        TabPageRemapper.Text = "Remapper";
        // 
        // groupBox1
        // 
        groupBox1.Controls.Add(Inclusions);
        groupBox1.Controls.Add(groupBox3);
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
        Inclusions.Location = new Point(12, 309);
        Inclusions.Name = "Inclusions";
        Inclusions.SelectedIndex = 0;
        Inclusions.Size = new Size(751, 784);
        Inclusions.TabIndex = 14;
        // 
        // tabPage1
        // 
        tabPage1.BackColor = SystemColors.ControlDarkDark;
        tabPage1.Controls.Add(groupBox4);
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
        tabPage1.Size = new Size(743, 746);
        tabPage1.TabIndex = 0;
        tabPage1.Text = "Methods";
        // 
        // groupBox4
        // 
        groupBox4.Controls.Add(MethodCountUpDown);
        groupBox4.Controls.Add(label4);
        groupBox4.Location = new Point(6, 329);
        groupBox4.Name = "groupBox4";
        groupBox4.Size = new Size(741, 421);
        groupBox4.TabIndex = 22;
        groupBox4.TabStop = false;
        groupBox4.Text = "Other";
        // 
        // MethodCountUpDown
        // 
        MethodCountUpDown.Location = new Point(6, 30);
        MethodCountUpDown.Name = "MethodCountUpDown";
        MethodCountUpDown.Size = new Size(55, 31);
        MethodCountUpDown.TabIndex = 6;
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(67, 32);
        label4.Name = "label4";
        label4.Size = new Size(128, 25);
        label4.TabIndex = 10;
        label4.Text = "Method Count";
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
        tabPage2.Controls.Add(groupBox5);
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
        tabPage2.Size = new Size(743, 746);
        tabPage2.TabIndex = 1;
        tabPage2.Text = "Fields";
        // 
        // groupBox5
        // 
        groupBox5.Controls.Add(FieldCountUpDown);
        groupBox5.Controls.Add(label1);
        groupBox5.Location = new Point(6, 329);
        groupBox5.Name = "groupBox5";
        groupBox5.Size = new Size(741, 417);
        groupBox5.TabIndex = 28;
        groupBox5.TabStop = false;
        groupBox5.Text = "Other";
        // 
        // FieldCountUpDown
        // 
        FieldCountUpDown.Location = new Point(6, 30);
        FieldCountUpDown.Name = "FieldCountUpDown";
        FieldCountUpDown.Size = new Size(55, 31);
        FieldCountUpDown.TabIndex = 3;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(67, 32);
        label1.Name = "label1";
        label1.Size = new Size(102, 25);
        label1.TabIndex = 7;
        label1.Text = "Field Count";
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
        tabPage3.Controls.Add(groupBox7);
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
        tabPage3.Size = new Size(743, 746);
        tabPage3.TabIndex = 2;
        tabPage3.Text = "Properties";
        // 
        // groupBox7
        // 
        groupBox7.Controls.Add(PropertiesCountUpDown);
        groupBox7.Controls.Add(label3);
        groupBox7.Location = new Point(6, 329);
        groupBox7.Name = "groupBox7";
        groupBox7.Size = new Size(734, 417);
        groupBox7.TabIndex = 28;
        groupBox7.TabStop = false;
        groupBox7.Text = "Other";
        // 
        // PropertiesCountUpDown
        // 
        PropertiesCountUpDown.Location = new Point(6, 30);
        PropertiesCountUpDown.Name = "PropertiesCountUpDown";
        PropertiesCountUpDown.Size = new Size(55, 31);
        PropertiesCountUpDown.TabIndex = 5;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(67, 32);
        label3.Name = "label3";
        label3.Size = new Size(133, 25);
        label3.TabIndex = 9;
        label3.Text = "Property Count";
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
        tabPage4.Size = new Size(743, 746);
        tabPage4.TabIndex = 3;
        tabPage4.Text = "Other";
        // 
        // groupBox6
        // 
        groupBox6.Controls.Add(NestedTypeCountUpDown);
        groupBox6.Controls.Add(BaseClassExcludeTextField);
        groupBox6.Controls.Add(NestedTypeParentName);
        groupBox6.Controls.Add(label2);
        groupBox6.Controls.Add(BaseClassIncludeTextFIeld);
        groupBox6.Location = new Point(6, 328);
        groupBox6.Name = "groupBox6";
        groupBox6.Size = new Size(737, 422);
        groupBox6.TabIndex = 28;
        groupBox6.TabStop = false;
        groupBox6.Text = "Other";
        // 
        // NestedTypeCountUpDown
        // 
        NestedTypeCountUpDown.Location = new Point(6, 30);
        NestedTypeCountUpDown.Name = "NestedTypeCountUpDown";
        NestedTypeCountUpDown.Size = new Size(55, 31);
        NestedTypeCountUpDown.TabIndex = 4;
        // 
        // BaseClassExcludeTextField
        // 
        BaseClassExcludeTextField.Location = new Point(6, 141);
        BaseClassExcludeTextField.Name = "BaseClassExcludeTextField";
        BaseClassExcludeTextField.PlaceholderText = "Exclude Base Class";
        BaseClassExcludeTextField.Size = new Size(239, 31);
        BaseClassExcludeTextField.TabIndex = 1;
        // 
        // NestedTypeParentName
        // 
        NestedTypeParentName.Location = new Point(6, 67);
        NestedTypeParentName.Name = "NestedTypeParentName";
        NestedTypeParentName.PlaceholderText = "Nested Type Parent Name";
        NestedTypeParentName.Size = new Size(239, 31);
        NestedTypeParentName.TabIndex = 0;
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(68, 30);
        label2.Name = "label2";
        label2.Size = new Size(163, 25);
        label2.TabIndex = 8;
        label2.Text = "Nested Type Count";
        // 
        // BaseClassIncludeTextFIeld
        // 
        BaseClassIncludeTextFIeld.Location = new Point(6, 104);
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
        // groupBox3
        // 
        groupBox3.Controls.Add(IsSealed);
        groupBox3.Controls.Add(IsInterface);
        groupBox3.Controls.Add(IsAbstract);
        groupBox3.Controls.Add(HasGenericParameters);
        groupBox3.Controls.Add(IsPublic);
        groupBox3.Controls.Add(IsDerived);
        groupBox3.Controls.Add(IsEnum);
        groupBox3.Controls.Add(IsNested);
        groupBox3.Controls.Add(HasAttribute);
        groupBox3.Location = new Point(6, 124);
        groupBox3.Name = "groupBox3";
        groupBox3.Size = new Size(751, 165);
        groupBox3.TabIndex = 4;
        groupBox3.TabStop = false;
        groupBox3.Text = "True or False Checks";
        // 
        // IsSealed
        // 
        IsSealed.AutoSize = true;
        IsSealed.Location = new Point(6, 121);
        IsSealed.Name = "IsSealed";
        IsSealed.Size = new Size(103, 29);
        IsSealed.TabIndex = 12;
        IsSealed.Text = "IsSealed";
        IsSealed.UseVisualStyleBackColor = true;
        // 
        // IsInterface
        // 
        IsInterface.AutoSize = true;
        IsInterface.Location = new Point(6, 75);
        IsInterface.Name = "IsInterface";
        IsInterface.Size = new Size(119, 29);
        IsInterface.TabIndex = 6;
        IsInterface.Text = "IsInterface";
        IsInterface.UseVisualStyleBackColor = true;
        // 
        // IsAbstract
        // 
        IsAbstract.AutoSize = true;
        IsAbstract.Location = new Point(125, 30);
        IsAbstract.Name = "IsAbstract";
        IsAbstract.Size = new Size(117, 29);
        IsAbstract.TabIndex = 5;
        IsAbstract.Text = "IsAbstract";
        IsAbstract.UseVisualStyleBackColor = true;
        // 
        // HasGenericParameters
        // 
        HasGenericParameters.AutoSize = true;
        HasGenericParameters.Location = new Point(269, 76);
        HasGenericParameters.Name = "HasGenericParameters";
        HasGenericParameters.Size = new Size(213, 29);
        HasGenericParameters.TabIndex = 11;
        HasGenericParameters.Text = "HasGenericParameters";
        HasGenericParameters.UseVisualStyleBackColor = true;
        // 
        // IsPublic
        // 
        IsPublic.AutoSize = true;
        IsPublic.Location = new Point(6, 30);
        IsPublic.Name = "IsPublic";
        IsPublic.Size = new Size(98, 29);
        IsPublic.TabIndex = 4;
        IsPublic.Text = "IsPublic";
        IsPublic.UseVisualStyleBackColor = true;
        // 
        // IsDerived
        // 
        IsDerived.AutoSize = true;
        IsDerived.Location = new Point(269, 31);
        IsDerived.Name = "IsDerived";
        IsDerived.Size = new Size(112, 29);
        IsDerived.TabIndex = 10;
        IsDerived.Text = "IsDerived";
        IsDerived.UseVisualStyleBackColor = true;
        // 
        // IsEnum
        // 
        IsEnum.AutoSize = true;
        IsEnum.Location = new Point(269, 121);
        IsEnum.Name = "IsEnum";
        IsEnum.Size = new Size(96, 29);
        IsEnum.TabIndex = 7;
        IsEnum.Text = "IsEnum";
        IsEnum.UseVisualStyleBackColor = true;
        // 
        // IsNested
        // 
        IsNested.AutoSize = true;
        IsNested.Location = new Point(125, 76);
        IsNested.Name = "IsNested";
        IsNested.Size = new Size(107, 29);
        IsNested.TabIndex = 8;
        IsNested.Text = "IsNested";
        IsNested.UseVisualStyleBackColor = true;
        // 
        // HasAttribute
        // 
        HasAttribute.AutoSize = true;
        HasAttribute.Location = new Point(125, 121);
        HasAttribute.Name = "HasAttribute";
        HasAttribute.Size = new Size(138, 29);
        HasAttribute.TabIndex = 9;
        HasAttribute.Text = "HasAttribute";
        HasAttribute.UseVisualStyleBackColor = true;
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
        // ScoreButton
        // 
        ScoreButton.Location = new Point(1129, 22);
        ScoreButton.Name = "ScoreButton";
        ScoreButton.Size = new Size(168, 34);
        ScoreButton.TabIndex = 5;
        ScoreButton.Text = "Score Remap";
        ScoreButton.UseVisualStyleBackColor = true;
        ScoreButton.Click += ScoreButton_Click;
        // 
        // RemoveRemapButton
        // 
        RemoveRemapButton.Location = new Point(955, 22);
        RemoveRemapButton.Name = "RemoveRemapButton";
        RemoveRemapButton.Size = new Size(168, 34);
        RemoveRemapButton.TabIndex = 2;
        RemoveRemapButton.Text = "Remove Remap";
        RemoveRemapButton.UseVisualStyleBackColor = true;
        RemoveRemapButton.Click += RemoveRemapButton_Click;
        // 
        // AddRemapButton
        // 
        AddRemapButton.Location = new Point(781, 22);
        AddRemapButton.Name = "AddRemapButton";
        AddRemapButton.Size = new Size(168, 34);
        AddRemapButton.TabIndex = 4;
        AddRemapButton.Text = "Add Remap";
        AddRemapButton.UseVisualStyleBackColor = true;
        AddRemapButton.Click += AddRemapButton_Click;
        // 
        // RemapListView
        // 
        RemapListView.BackColor = SystemColors.AppWorkspace;
        RemapListView.Location = new Point(781, 62);
        RemapListView.Name = "RemapListView";
        RemapListView.Size = new Size(899, 631);
        RemapListView.TabIndex = 0;
        RemapListView.UseCompatibleStateImageBehavior = false;
        // 
        // TabControlMain
        // 
        TabControlMain.Controls.Add(TabPageRemapper);
        TabControlMain.Location = new Point(12, 40);
        TabControlMain.Name = "TabControlMain";
        TabControlMain.SelectedIndex = 0;
        TabControlMain.Size = new Size(1703, 1028);
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
        // AssemblyToolGUI
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = SystemColors.ControlDarkDark;
        ClientSize = new Size(1708, 1133);
        Controls.Add(TabControlMain);
        Controls.Add(menuStrip1);
        Name = "AssemblyToolGUI";
        Text = "Cj's Assembly Tool V0.1.0";
        ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
        TabPageRemapper.ResumeLayout(false);
        groupBox1.ResumeLayout(false);
        Inclusions.ResumeLayout(false);
        tabPage1.ResumeLayout(false);
        tabPage1.PerformLayout();
        groupBox4.ResumeLayout(false);
        groupBox4.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)MethodCountUpDown).EndInit();
        tabPage2.ResumeLayout(false);
        tabPage2.PerformLayout();
        groupBox5.ResumeLayout(false);
        groupBox5.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)FieldCountUpDown).EndInit();
        tabPage3.ResumeLayout(false);
        tabPage3.PerformLayout();
        groupBox7.ResumeLayout(false);
        groupBox7.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)PropertiesCountUpDown).EndInit();
        tabPage4.ResumeLayout(false);
        tabPage4.PerformLayout();
        groupBox6.ResumeLayout(false);
        groupBox6.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)NestedTypeCountUpDown).EndInit();
        groupBox3.ResumeLayout(false);
        groupBox3.PerformLayout();
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
    private GroupBox groupBox4;
    private NumericUpDown MethodCountUpDown;
    private Label label4;
    private TextBox ExcludeMethodTextBox;
    private TextBox IncludeMethodTextBox;
    private Button MethodExcludeRemoveButton;
    private Button MethodExcludeAddButton;
    private Button MethodIncludeRemoveButton;
    private Button MethodIncludeAddButton;
    private ListBox MethodExcludeBox;
    private ListBox MethodIncludeBox;
    private TabPage tabPage2;
    private GroupBox groupBox5;
    private NumericUpDown FieldCountUpDown;
    private Label label1;
    private TextBox FieldsExcludeTextInput;
    private TextBox FieldsIncludeTextInput;
    private Button FieldExcludeRemoveButton;
    private Button FieldExcludeAddButton;
    private Button FieldIncludeRemoveButton;
    private Button FIeldIncludeAddButton;
    private ListBox FieldExcludeBox;
    private ListBox FieldIncludeBox;
    private TabPage tabPage3;
    private GroupBox groupBox7;
    private NumericUpDown PropertiesCountUpDown;
    private Label label3;
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
    private Label label2;
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
    private CheckBox IsInterface;
    private CheckBox IsAbstract;
    private CheckBox HasGenericParameters;
    private CheckBox IsPublic;
    private CheckBox IsDerived;
    private CheckBox IsEnum;
    private CheckBox IsNested;
    private CheckBox HasAttribute;
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
    private CheckBox IsSealed;
}
