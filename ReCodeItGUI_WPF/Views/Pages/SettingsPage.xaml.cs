using ReCodeItGUI_WPF.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace ReCodeItGUI_WPF.Views.Pages
{
    public partial class SettingsPage : INavigableView<SettingsViewModel>
    {
        public SettingsViewModel ViewModel { get; }

        public SettingsPage(SettingsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
