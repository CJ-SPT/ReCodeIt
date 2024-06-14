namespace RemapperGUI.Utils;

internal static class GUI
{
    /// <summary>
    /// Returns the value of the count or null if disabled
    /// </summary>
    /// <param name="box"></param>
    /// <returns></returns>
    public static int? GetCount(this CheckBox box, NumericUpDown upDown)
    {
        if (box.Checked)
        {
            return (int?)upDown.Value;
        }

        return null;
    }

    public static bool? GetEnabled(this DomainUpDown domainUpDown)
    {
        if (domainUpDown.Text == "True")
        {
            return true;
        }
        else if (domainUpDown.Text == "False")
        {
            return false;
        }

        return null;
    }
}