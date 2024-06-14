using AssemblyRemapper.Utils;

namespace AssemblyRemapperGUI;

using static DataProvider;

public partial class AssemblyToolGUI
{
    private void RefreshSettingsPage()
    {
        AssemblyPathTextBox.Text = Settings.AppSettings.AssemblyPath;
        OutputPathTextBox.Text = Settings.AppSettings.OutputPath;
        MappingPathTextBox.Text = Settings.AppSettings.MappingPath;

        DebugLoggingCheckbox.Checked = Settings.AppSettings.Debug;
        SilentModeCheckbox.Checked = Settings.AppSettings.SilentMode;
        RenameFieldsCheckbox.Checked = Settings.AppSettings.RenameFields;
        RenamePropertiesCheckbox.Checked = Settings.AppSettings.RenameProperties;
        PublicizeCheckbox.Checked = Settings.AppSettings.Publicize;
        UnsealCheckbox.Checked = Settings.AppSettings.Unseal;

        MaxMatchCountUpDown.Value = Settings.Remapper.MaxMatchCount;
        AutoMapperRequiredMatchesUpDown.Value = Settings.AutoMapper.RequiredMatches;
    }

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
            Settings.AppSettings.AssemblyPath = fDialog.FileName;
            LoadAssemblyDefinition();
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
            Settings.AppSettings.OutputPath = fDialog.SelectedPath;
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
            LoadMappingFile(fDialog.FileName);
            MappingPathTextBox.Text = fDialog.FileName;
        }
    }

    #endregion SETTINGS_BUTTONS

    #region CHECKBOXES

    private void DebugLoggingCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        Settings.AppSettings.Debug = DebugLoggingCheckbox.Checked;
        SaveAppSettings();
    }

    private void SilentModeCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        Settings.AppSettings.SilentMode = SilentModeCheckbox.Checked;
        SaveAppSettings();
    }

    private void RenameFieldsCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        Settings.AppSettings.RenameFields = RenameFieldsCheckbox.Checked;
        SaveAppSettings();
    }

    private void RenamePropertiesCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        Settings.AppSettings.RenameProperties = RenamePropertiesCheckbox.Checked;
        SaveAppSettings();
    }

    private void PublicizeCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        Settings.AppSettings.Publicize = PublicizeCheckbox.Checked;
        SaveAppSettings();
    }

    private void UnsealCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        Settings.AppSettings.Unseal = UnsealCheckbox.Checked;
        SaveAppSettings();
    }

    #endregion CHECKBOXES

    #region UPDOWNS

    private void MaxMatchCountUpDown_ValueChanged(object sender, EventArgs e)
    {
        Settings.Remapper.MaxMatchCount = (int)MaxMatchCountUpDown.Value;
        SaveAppSettings();
    }

    private void AutoMapperRequiredMatchesUpDown_ValueChanged(object sender, EventArgs e)
    {
        Settings.AutoMapper.RequiredMatches = (int)AutoMapperRequiredMatchesUpDown.Value;
        SaveAppSettings();
    }

    #endregion UPDOWNS
}