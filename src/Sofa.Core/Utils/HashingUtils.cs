using System.Security.Cryptography;

namespace Sofa.Core.Utils;

public static class HashingUtils
{
    public static string GetFileHash(string filePath)
    {
        using var sha256 = SHA256.Create();
        using FileStream fileStream = File.OpenRead(filePath);
        return BitConverter.ToString(sha256.ComputeHash(fileStream)).Replace("-", "");
    }
}
