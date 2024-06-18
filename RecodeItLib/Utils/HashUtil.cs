using System.Security.Cryptography;

namespace ReCodeIt.Utils;

internal static class HashUtil
{
    /// <summary>
    /// Create a file hash from an inputed file
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns>A file hash</returns>
    public static string GetFileHash(string filePath)
    {
        using var sha256 = SHA256.Create();
        using var stream = File.OpenRead(filePath);

        byte[] hashBytes = sha256.ComputeHash(stream);

        var hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        return hash;
    }
}