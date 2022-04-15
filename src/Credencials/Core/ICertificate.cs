namespace Credencials.Core;

public interface ICertificate
{
    string PlainBase64 { get; }
    byte[] CertificatePlainBytes { get; }

    /// <summary>
    /// Tax identification number. In México is RFC.
    /// </summary>
    string? Subject { get; }

    /// <summary>
    /// Issuer
    /// </summary>
    string? Issuer { get; }

    /// <summary>
    /// Version
    /// </summary>
    int Version { get; }

    /// <summary>
    /// Valid Date
    /// </summary>
    DateTime NotBefore { get; }

    /// <summary>
    /// Expiry Date
    /// </summary>
    DateTime NotAfter { get; }

    /// <summary>
    /// Thumbprint
    /// </summary>
    string Thumbprint { get; }

    /// <summary>
    /// Serial Number
    /// </summary>
    string SerialNumber { get; }

    /// <summary>
    /// Friendly Name
    /// </summary>
    string? FriendlyName { get; }

    /// <summary>
    /// Public Key Format
    /// </summary>
    string PublicKeyFormat { get; }

    /// <summary>
    /// Raw Data Length
    /// </summary>
    int RawDataLength { get; }

    /// <summary>
    /// Certificate Raw Data 
    /// </summary>
    byte[] RawDataBytes { get; }

    string GetPemRepresentation();
}