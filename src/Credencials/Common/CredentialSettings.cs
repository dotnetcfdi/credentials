using System.Security.Cryptography;

namespace Credencials.Common;

public static class CredentialSettings
{
    /// <summary>
    /// Default algorithm to mexican invoicing
    /// </summary>
    public static HashAlgorithmName Algorithm { get; set; } = HashAlgorithmName.SHA256;

    /// <summary>
    /// Default signature padding
    /// </summary>
    public static RSASignaturePadding SignaturePadding { get; set; } = RSASignaturePadding.Pss;
}