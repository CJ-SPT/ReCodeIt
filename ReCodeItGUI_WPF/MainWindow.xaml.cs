using ReCodeIt.Utils;
using ReCodeItGUI_WPF.Helpers;
using System.Windows;
using System.Windows.Input;

namespace ReCodeItGUI_WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private ICommand deleteCommand;

    public MainWindow()
    {
        InitializeComponent();
        DataProvider.LoadAppSettings();
        DataProvider.LoadMappingFile();
    }

    #region MANUAL_REMAPPER

    private void SaveRemapButton_Click(object sender, RoutedEventArgs e)
    {
        this.CreateRemapFromGui();
    }

    private void MethodIncludeButton_Click(object sender, RoutedEventArgs e)
    {
        MethodIncludeListView.AddToListView(MethodTextBox);
    }

    private void MethodExcludeButton_Click(object sender, RoutedEventArgs e)
    {
        MethodExcludeListView.AddToListView(MethodTextBox);
    }

    private void MethodRemoveButton_Click(object sender, RoutedEventArgs e)
    {
        MethodIncludeListView.RemoveSelectedItem();
        MethodExcludeListView.RemoveSelectedItem();
    }

    private void FieldIncludeButton_Click(object sender, RoutedEventArgs e)
    {
        FieldIncludeListView.AddToListView(FieldTextBox);
    }

    private void FieldExcludeButton_Click(object sender, RoutedEventArgs e)
    {
        FieldExcludeListView.AddToListView(FieldTextBox);
    }

    private void FieldRemoveButton_Click(object sender, RoutedEventArgs e)
    {
        FieldIncludeListView.RemoveSelectedItem();
        FieldExcludeListView.RemoveSelectedItem();
    }

    private void PropertyIncludeButton_Click(object sender, RoutedEventArgs e)
    {
        PropertyIncludeListBox.AddToListView(PropertyTextBox);
    }

    private void PropertyExcludeButton_Click(object sender, RoutedEventArgs e)
    {
        PropertyExcludeListBox.AddToListView(PropertyTextBox);
    }

    private void PropertyRemoveButton_Click(object sender, RoutedEventArgs e)
    {
        PropertyIncludeListBox.RemoveSelectedItem();
        PropertyExcludeListBox.RemoveSelectedItem();
    }

    private void NestedTypeIncludeAddButton_Click(object sender, RoutedEventArgs e)
    {
        NestedTypeIncludeListView.AddToListView(NestTypeTextBox);
    }

    private void NestedTypeExcludeButton_Click(object sender, RoutedEventArgs e)
    {
        NestedTypeExcludeListView.AddToListView(NestTypeTextBox);
    }

    private void NestedTypeRemoveButton_Click(object sender, RoutedEventArgs e)
    {
        NestedTypeIncludeListView.RemoveSelectedItem();
        NestedTypeExcludeListView.RemoveSelectedItem();
    }

    #endregion MANUAL_REMAPPER
}