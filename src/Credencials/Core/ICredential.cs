namespace Credencials.Core;

/// <summary>
/// Represents a wrapper for certificate and private key. Something like 'FIEL and CSD'
/// </summary>
public interface ICredential
{
    /// <summary>
    /// Private key password
    /// </summary>
    string PasswordPhrase { get; }

    /// <summary>
    ///  Dotnetcfdi certificate wrapper
    /// </summary>
    ICertificate Certificate { get; }

    /// <summary>
    ///  Dotnetcfdi PrivateKey wrapper
    /// </summary>
    IPrivateKey PrivateKey { get; }

    string PemCertificate { get; }
    string PemPrivateKey { get; }
    byte[] CreatePFX();

    /// <summary>
    /// Sign some data
    /// </summary>
    /// <param name="toSign">string to be signed</param>
    /// <returns>signed bytes</returns>
    /// see CredentialSettings class to see signature parameters
    byte[] SignData(string toSign);

    /// <summary>
    /// Verify the signature of some data
    /// </summary>
    /// <param name="dataToVerify">original data in bytes</param>
    /// <param name="signedData">signed data in bytes</param>
    /// <returns>True when the signature is valid, otherwise false</returns>
    bool VerifyData(byte[] dataToVerify, byte[] signedData);
}