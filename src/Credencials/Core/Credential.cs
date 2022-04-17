using System.Security.Cryptography.X509Certificates;

namespace Credencials.Core;

/// <summary>
/// Represents a wrapper for certificate and private key. Something like 'FIEL and CSD'
/// </summary>
public class Credential : ICredential
{
    //certificate and private key into X509Certificate2 object
    private readonly X509Certificate2 certificateWithPrivateKey;


    public Credential(ICertificate certificate, IPrivateKey privateKey)
    {
        Certificate = certificate;
        PrivateKey = privateKey;
        PasswordPhrase = privateKey.PasswordPhrase;

        PemCertificate = certificate.GetPemRepresentation();
        PemPrivateKey = privateKey.GetPemRepresentation();

        certificateWithPrivateKey = X509Certificate2.CreateFromPem(PemCertificate, PemPrivateKey);
    }

    /// <summary>
    /// Private key password
    /// </summary>
    public string PasswordPhrase { get; }

    /// <summary>
    ///  Dotnetcfdi certificate wrapper
    /// </summary>
    public ICertificate Certificate { get; }

    /// <summary>
    ///  Dotnetcfdi PrivateKey wrapper
    /// </summary>
    public IPrivateKey PrivateKey { get; }


    public string PemCertificate { get; }

    public string PemPrivateKey { get; }

    public byte[] CreatePFX()
    {
        var pfxBytes = certificateWithPrivateKey.Export(X509ContentType.Pfx, PasswordPhrase);

        return pfxBytes;
    }

    private string GetCertificatePemRepresentation()
    {
        return Certificate.GetPemRepresentation();
    }

    private string GetPrivateKeyPemRepresentation()
    {
        return PrivateKey.GetPemRepresentation();
    }

    /// <summary>
    /// Sign some data
    /// </summary>
    /// <param name="toSign">string to be signed</param>
    /// <returns>signed bytes</returns>
    /// see CredentialSettings class to see signature parameters
    public byte[] SignData(string toSign)
    {
        //Sing and get signed bytes array
        var signedBytes = PrivateKey.SignData(toSign);

        return signedBytes;
    }

    /// <summary>
    /// Verify the signature of some data
    /// </summary>
    /// <param name="dataToVerify">original data in bytes</param>
    /// <param name="signedData">signed data in bytes</param>
    /// <returns>True when the signature is valid, otherwise false</returns>
    public bool VerifyData(byte[] dataToVerify, byte[] signedData)
    {
        var isValid = PrivateKey.VerifyData(dataToVerify, signedData);

        return isValid;
    }
}