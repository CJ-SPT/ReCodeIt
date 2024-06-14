using AssemblyRemapper.Enums;
using AssemblyRemapper.Models;
using AssemblyRemapper.Remapper;
using AssemblyRemapper.Utils;
using RemapperGUI.Utils;

namespace AssemblyRemapperGUI
{
    public partial class AssemblyToolGUI : Form
    {
        public static Remapper Remapper { get; private set; } = new();

        public AssemblyToolGUI()
        {
            InitializeComponent();
            PopulateDomainUpDowns();

            foreach (var remap in DataProvider.Remaps)
            {
                RemapTreeView.Nodes.Add(GUI.GenerateTreeNode(remap));
            }
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

            var remap = new RemapModel
            {
                Succeeded = false,
                FailureReason = EFailureReason.None,
                NewTypeName = NewTypeName.Text,
                OriginalTypeName = OriginalTypeName.Text == string.Empty ? null : OriginalTypeName.Text,
                UseForceRename = ForceRenameCheckbox.Checked,
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
                    IncludeMethods = GUI.GetAllEntriesFromListBox(MethodIncludeBox),
                    ExcludeMethods = GUI.GetAllEntriesFromListBox(MethodExcludeBox),
                    IncludeFields = GUI.GetAllEntriesFromListBox(FieldIncludeBox),
                    ExcludeFields = GUI.GetAllEntriesFromListBox(FieldExcludeBox),
                    IncludeProperties = GUI.GetAllEntriesFromListBox(PropertiesIncludeBox),
                    ExcludeProperties = GUI.GetAllEntriesFromListBox(PropertiesExcludeBox),
                    IncludeNestedTypes = GUI.GetAllEntriesFromListBox(NestedTypesIncludeBox),
                    ExcludeNestedTypes = GUI.GetAllEntriesFromListBox(NestedTypesExcludeBox),
                }
            };

            RemapTreeView.Nodes.Add(GUI.GenerateTreeNode(remap));
            DataProvider.Remaps.Add(remap);
            ResetAll();
        }

        private void RemoveRemapButton_Click(object sender, EventArgs e)
        {
            DataProvider.Remaps?.RemoveAt(RemapTreeView.SelectedNode.Index);
            RemapTreeView.SelectedNode?.Remove();
        }

        private void RunRemapButton_Click(object sender, EventArgs e)
        {
            if (Remapper.IsRunning) { return; }

            Task.Run(() => Remapper.InitializeRemap());
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
            var fDialog = new OpenFileDialog
            {
                Title = "Select a mapping file",
                Filter = "JSON Files (*.json)|*.json|JSONC Files (*.jsonc)|*.jsonc|All Files (*.*)|*.*",
                Multiselect = false
            };

            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                var fileName = fDialog.FileName;

                DataProvider.LoadMappingFile(fileName);
            }

            RemapTreeView.Nodes.Clear();

            foreach (var remap in DataProvider.Remaps)
            {
                RemapTreeView.Nodes.Add(GUI.GenerateTreeNode(remap));
            }
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

        #endregion LISTBOX_BUTTONS

        #endregion BUTTONS

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

            MethodCountUpDown.Value = 0;
            FieldCountUpDown.Value = 0;
            PropertyCountUpDown.Value = 0;
            NestedTypeCountUpDown.Value = 0;

            // Check boxes

            ForceRenameCheckbox.Checked = false;
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
    }
}