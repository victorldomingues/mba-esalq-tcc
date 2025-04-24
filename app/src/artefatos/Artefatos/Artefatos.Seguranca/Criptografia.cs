using System.Security.Cryptography;
using System.Text;

namespace Artefatos.Seguranca;

public abstract class Criptografia
{
    
    public static string CriptografarSHA256(string entrada)
    {
        using SHA256 sha256Hash = SHA256.Create();
        return ObterHash(sha256Hash, entrada);
    }
    
    /// <summary>
    /// TODO: referenciar algoritimo de crioptografia
    /// https://learn.microsoft.com/pt-br/dotnet/api/system.security.cryptography.hashalgorithm.computehash?view=net-8.0
    /// </summary>
    /// <param name="hashAlgorithm"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    private static string ObterHash(HashAlgorithm hashAlgorithm, string input)
    {

        // Convert the input string to a byte array and compute the hash.
        byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        var sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }

}