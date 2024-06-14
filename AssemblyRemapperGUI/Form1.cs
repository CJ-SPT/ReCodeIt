using AssemblyRemapper.Enums;
using AssemblyRemapper.Models;
using AssemblyRemapper.Remapper;
using RemapperGUI.Utils;

namespace AssemblyRemapperGUI
{
    public partial class AssemblyToolGUI : Form
    {
        public static Remapper Remapper { get; private set; }

        public AssemblyToolGUI()
        {
            InitializeComponent();
        }

        #region BUTTONS

        /// <summary>
        /// Construct a new remap when the button is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddRemapButton_Click(object sender, EventArgs e)
        {
            MethodIncludeBox.Items.Add("TEST");

            var remap = new RemapModel
            {
                Succeeded = false,
                FailureReason = EFailureReason.None,
                NewTypeName = NewTypeName.Text,
                OriginalTypeName = OriginalTypeName.Text,
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

                    // Constructor
                    MethodCount = MethodCountEnabled.GetCount(MethodCountUpDown),
                    FieldCount = FieldCountEnabled.GetCount(FieldCountUpDown),
                    PropertyCount = PropertyCountEnabled.GetCount(PropertyCountUpDown),
                    NestedTypeCount = NestedTypeCountEnabled.GetCount(NestedTypeCountUpDown),
                    IncludeMethods = [],
                    ExcludeMethods = [],
                    IncludeFields = [],
                    ExcludeFields = [],
                    IncludeProperties = [],
                    ExcludeProperties = [],
                    IncludeNestedTypes = [],
                    ExcludeNestedTypes = [],
                }
            };
        }

        private void RemoveRemapButton_Click(object sender, EventArgs e)
        {
        }

        private void ScoreButton_Click(object sender, EventArgs e)
        {
        }

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

        #endregion BUTTONS
    }
}