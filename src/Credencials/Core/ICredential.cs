using Credencials.Common;

namespace Credencials.Core;

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

    /// <summary>
    /// Fiel whe credential.certificate is a FIEL certificate otherwise csd
    /// </summary>
    CredentialType CredentialType { get; }

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

    /// <summary>
    /// True if Certificate.ValidTo date is less than the current date
    /// </summary>
    bool IsValid();

    /// <summary>
    /// True when is a FIEL certificate
    /// </summary>
    bool IsFiel();

    /// <summary>
    /// True when certificate.ValidTo date is less than the current date and is a FIEL certificate
    /// </summary>
    /// <returns></returns>
    bool IsValidFiel();

    /// <summary>
    ///  Convert the input string to a byte array and compute the hash.
    /// </summary>
    /// <param name="input">data to hashing</param>
    /// <returns>encoded b64 hash</returns>
    string CreateHash(string input);

    /// <summary>
    /// Verify a hash against a string.
    /// </summary>
    /// <param name="input">data to hashing</param>
    /// <param name="hash">encoded b64 hash</param>
    /// <returns> true when computed hash is same of input hash otherwise false</returns>
    bool VerifyHash(string input, string hash);
}