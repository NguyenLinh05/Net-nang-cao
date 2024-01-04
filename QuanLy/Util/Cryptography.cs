using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace QuanLy.Util
{
    public class Cryptography
    {
        // Reference: https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hashalgorithm.computehash?source=recommendations&view=net-7.0

        public static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public static bool VeryfyHash(HashAlgorithm hashAlgorithm, string input, string hash)
        {
            var hashOfInput = GetHash(hashAlgorithm, input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(hashOfInput, hash) == 0;
        }
    }
}
