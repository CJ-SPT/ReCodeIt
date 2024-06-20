using Microsoft.Win32;

namespace ReCodeItLib.Utils;

public static class RegistryHelper
{
    /// <summary>
    /// Sets a key in the registry, given its key, value, and kind
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="kind"></param>
    public static void SetRegistryValue(string key, string value, RegistryValueKind kind)
    {
        var regKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\ReCodeIt");

        regKey.SetValue(key, value, kind);
    }

    /// <summary>
    /// Gets a key from the registry, given its key and type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public static T? GetRegistryValue<T>(string key)
    {
        var regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\ReCodeIt");

        return (T)regKey.GetValue(key);
    }
}