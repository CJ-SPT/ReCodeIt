using Microsoft.Win32;
using ReCodeIt.Models;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace ReCodeItGUI_WPF.Helpers
{
    internal static class GUIExtentions
    {
        #region EXT_METHODS

        public static void AddToListView(this ListView listView, TextBox textBox)
        {
            if (!string.IsNullOrWhiteSpace(textBox.Text) && !listView.Items.Contains(textBox.Text))
            {
                listView.Items.Add(textBox.Text);
                textBox.Clear();
            }
        }

        public static void RemoveSelectedItem(this ListView listView)
        {
            if (listView.SelectedItem != null)
            {
                listView.Items.RemoveAt(listView.SelectedIndex);
            }
        }

        #endregion EXT_METHODS

        /// <summary>
        /// Returns the string result of the dialog
        /// </summary>
        /// <returns>Path if valid, or empty string</returns>
        public static string OpenFileDialog(bool dll = false)
        {
            var title = dll ? "Select a DLL File" : "Select a Json File";

            var defaultExt = dll ? ".dll" : ".jsonc";

            var dllFilter = "DLL files (*.dll)|*.dll|All files (*.*)|*.*";
            var jsonFilter = "JSON/JSONC files (*.json;*.jsonc)|*.json;*.jsonc|All files (*.*)|*.*";

            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = title;
            openFileDialog.Filter = dll ? dllFilter : jsonFilter;
            openFileDialog.DefaultExt = defaultExt;

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                return openFileDialog.FileName;
            }

            return string.Empty;
        }

        public static RemapModel CreateRemapFromGui(this MainWindow window)
        {
            var model = new RemapModel
            {
                NewTypeName = window.NewTypeNameTextBox.Text,
                OriginalTypeName = window.OriginalTypeNameTextBox.Text ?? string.Empty,
                UseForceRename = (bool)window.UseForceRenameCheckbox.IsChecked,
                SearchParams = new SearchParams
                {
                    IsPublic = window.IsPublicComboBox.IsComboEnabled(),
                    IsAbstract = window.IsAbstractComboBox.IsComboEnabled(),
                    IsInterface = window.IsInterfaceComboBox.IsComboEnabled(),
                    IsEnum = window.IsEnumComboBox.IsComboEnabled(),
                    IsNested = window.IsNestedComboBox.IsComboEnabled(),
                    IsSealed = window.IsSealedComboBox.IsComboEnabled(),
                    HasAttribute = window.HasAttributeComboBox.IsComboEnabled(),
                    IsDerived = window.IsDerivedComboBox.IsComboEnabled(),
                    HasGenericParameters = window.HasAttributeComboBox.IsComboEnabled(),
                    ParentName = window.ParentNameTextBox.GetText(),
                    MatchBaseClass = window.IncludeBaseClassTextBox.GetText(),
                    IgnoreBaseClass = window.IgnoreBaseClassTextBox.GetText(),
                    ConstructorParameterCount = window.CtorParamCountUpDown.GetValIfEnabled(window.CtorCheckbox),
                    MethodCount = window.MethodCountUpDown.GetValIfEnabled(window.MethodCheckbox),
                    FieldCount = window.FieldCountUpDown.GetValIfEnabled(window.FieldCountCheckbox),
                    PropertyCount = window.PropertyCountUpDown.GetValIfEnabled(window.PropertyCountCheckBox),
                    NestedTypeCount = window.NestedTypeCountUpDown.GetValIfEnabled(window.NestedTypeCountCheckBox),
                    IncludeMethods = window.MethodIncludeListView.GetItems(),
                    ExcludeMethods = window.MethodExcludeListView.GetItems(),
                    IncludeFields = window.FieldIncludeListView.GetItems(),
                    ExcludeFields = window.FieldExcludeListView.GetItems(),
                    IncludeProperties = window.PropertyIncludeListBox.GetItems(),
                    ExcludeProperties = window.PropertyExcludeListBox.GetItems(),
                    IncludeNestedTypes = window.NestedTypeIncludeListView.GetItems(),
                    ExcludeNestedTypes = window.NestedTypeExcludeListView.GetItems(),
                }
            };

            return model;
        }

        /// <summary>
        /// True or false if selected, otherwise null
        /// </summary>
        /// <param name="comboBox"></param>
        /// <returns></returns>
        public static bool? IsComboEnabled(this ComboBox comboBox)
        {
            if (bool.TryParse(comboBox.Text, out var result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// returns the text in the box if exists, or null
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns></returns>
        public static string? GetText(this TextBox textBox)
        {
            if (textBox.Text != string.Empty)
            {
                return textBox.Text;
            }

            return null;
        }

        public static int? GetValIfEnabled(this IntegerUpDown intUpDown, CheckBox cBox)
        {
            if ((bool)cBox.IsChecked)
            {
                return intUpDown.Value;
            }

            return null;
        }

        /// <summary>
        /// Converts list view objects to string list
        /// </summary>
        /// <param name="listView"></param>
        /// <returns></returns>
        public static List<string> GetItems(this ListView listView)
        {
            var tmp = new List<string>();

            foreach (var item in listView.Items)
            {
                tmp.Add(item.ToString());
            }

            return tmp;
        }
    }
}