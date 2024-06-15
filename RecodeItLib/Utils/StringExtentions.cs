using System.Text;

namespace ReCodeItLib.Utils;

internal static class StringExtentions
{
    /// <summary>
    /// Returns a string trimmed after any non letter character
    /// </summary>
    /// <param name="str"></param>
    /// <returns>Trimmed string if special character found, or the original string</returns>
    public static string TrimAfterSpecialChar(this string str)
    {
        var sb = new StringBuilder();

        var trimChars = new char[] { '`', '[', ']' };

        foreach (char c in str)
        {
            if (trimChars.Contains(c))
            {
            }

            if (char.IsLetter(c) || char.IsDigit(c))
            {
                sb.Append(c);
            }
            else
            {
                return sb.ToString();
            }
        }

        if (sb.Length > 0)
        {
            return sb.ToString();
        }

        return str;
    }
}