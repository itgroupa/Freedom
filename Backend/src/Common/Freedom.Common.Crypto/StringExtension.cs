using System.Security.Cryptography;

namespace Freedom.Common.Crypto;

public static class StringExtension
{
    public static string GetHashMd5(this string str)
    {
        using var md5 = MD5.Create();
        var inputBytes = System.Text.Encoding.ASCII.GetBytes(str);
        var hashBytes = md5.ComputeHash(inputBytes);

        return Convert.ToHexString(hashBytes);
    }
}
