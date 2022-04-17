namespace Credencials.Core;


/// <summary>
/// Represents a wrapper for FIEL and CSD certificate.
/// </summary>
public interface ICertificate
{
    /// <summary>
    /// The result of reading the bytes from the .cer file and converting them to base64
    /// </summary>
    string PlainBase64 { get; }

    /// <summary>
    /// The equivalent of reading the bytes from the .cer file and converting them to base64
    /// </summary>
    byte[] CertificatePlainBytes { get; }

    /// <summary>
    /// RFC as parsed from subject/x500UniqueIdentifier 
    /// see https://oidref.com/2.5.4.45
    /// </summary>
    string Rfc { get; }

    /// <summary>
    /// Legal name as parsed from subject/x500UniqueIdentifier (razón social)
    /// see https://oidref.com/2.5.4.45
    /// </summary>
    string LegalName { get; }

    /// <summary>
    /// All serial number
    /// </summary>
    string SerialNumber { get; }

    /// <summary>
    /// Certificate number required by Mexican tax authority (SAT) 
    /// </summary>
    string CertificateNumber { get; }

    /// <summary>
    /// Issuer data parsed into KeyValuePair collection
    /// </summary>
    List<KeyValuePair<string, string>> Issuer { get; }

    /// <summary>
    /// Subject data parsed into KeyValuePair collection
    /// see https://oidref.com/2.5.4.45
    /// </summary>
    List<KeyValuePair<string, string>> Subject { get; }

    /// <summary>
    /// Certificate version
    /// </summary>
    int Version { get; }

    /// <summary>
    /// Valid start date
    /// </summary>
    DateTime ValidFrom { get; }

    /// <summary>
    /// Valid end date
    /// </summary>
    DateTime ValidTo { get; }

    /// <summary>
    /// Raw Data Length
    /// </summary>
    int RawDataLength { get; }

    /// <summary>
    /// RawDataBytes
    /// </summary>
    byte[] RawDataBytes { get; }

    /// <summary>
    /// Convert X.509 DER base64 or X.509 DER to X.509 PEM
    /// </summary>
    /// <returns></returns>
    string GetPemRepresentation();
}