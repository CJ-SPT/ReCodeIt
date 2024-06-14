using AssemblyRemapper.Utils;

namespace AssemblyRemapperGUI;

public partial class AssemblyToolGUI
{
    #region SETTINGS_BUTTONS

    private void PickAssemblyPathButton_Click(object sender, EventArgs e)
    {
        OpenFileDialog fDialog = new()
        {
            Title = "Select a DLL file",
            Filter = "DLL Files (*.dll)|*.dll|All Files (*.*)|*.*",
            Multiselect = false
        };

        if (fDialog.ShowDialog() == DialogResult.OK)
        {
            DataProvider.Settings.AppSettings.AssemblyPath = fDialog.FileName;
            DataProvider.LoadAssemblyDefinition();
            AssemblyPathTextBox.Text = fDialog.FileName;
        }
    }

    private void OutputDirectoryButton_Click(object sender, EventArgs e)
    {
        using FolderBrowserDialog fDialog = new();

        fDialog.Description = "Select a directory";
        fDialog.ShowNewFolderButton = true;

        if (fDialog.ShowDialog() == DialogResult.OK)
        {
            DataProvider.Settings.AppSettings.OutputPath = fDialog.SelectedPath;
            OutputPathTextBox.Text = fDialog.SelectedPath;
        }
    }

    private void MappingChooseButton_Click(object sender, EventArgs e)
    {
        var fDialog = new OpenFileDialog
        {
            Title = "Select a mapping file",
            Filter = "JSON Files (*.json)|*.json|JSONC Files (*.jsonc)|*.jsonc|All Files (*.*)|*.*",
            Multiselect = false
        };

        if (fDialog.ShowDialog() == DialogResult.OK)
        {
            DataProvider.LoadMappingFile(fDialog.FileName);
            MappingPathTextBox.Text = fDialog.FileName;
        }
    }

    #endregion SETTINGS_BUTTONS

    #region CHECKBOXES

    private void DebugLoggingCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        DataProvider.Settings.AppSettings.Debug = DebugLoggingCheckbox.Checked;
        DataProvider.SaveAppSettings();
    }

    private void SilentModeCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        DataProvider.Settings.AppSettings.SilentMode = SilentModeCheckbox.Checked;
        DataProvider.SaveAppSettings();
    }

    private void RenameFieldsCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        DataProvider.Settings.AppSettings.RenameFields = RenameFieldsCheckbox.Checked;
        DataProvider.SaveAppSettings();
    }

    private void RenamePropertiesCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        DataProvider.Settings.AppSettings.RenameProperties = RenamePropertiesCheckbox.Checked;
        DataProvider.SaveAppSettings();
    }

    private void PublicizeCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        DataProvider.Settings.AppSettings.Publicize = PublicizeCheckbox.Checked;
        DataProvider.SaveAppSettings();
    }

    private void UnsealCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        DataProvider.Settings.AppSettings.Unseal = UnsealCheckbox.Checked;
        DataProvider.SaveAppSettings();
    }

    #endregion CHECKBOXES

    #region UPDOWNS

    private void MaxMatchCountUpDown_ValueChanged(object sender, EventArgs e)
    {
        DataProvider.Settings.Remapper.MaxMatchCount = (int)MaxMatchCountUpDown.Value;
        DataProvider.SaveAppSettings();
    }

    private void AutoMapperRequiredMatchesUpDown_ValueChanged(object sender, EventArgs e)
    {
        DataProvider.Settings.AutoMapper.RequiredMatches = (int)AutoMapperRequiredMatchesUpDown.Value;
        DataProvider.SaveAppSettings();
    }

    #endregion UPDOWNS
}