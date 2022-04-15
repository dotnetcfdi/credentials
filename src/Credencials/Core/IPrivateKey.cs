using System.Security.Cryptography;

namespace Credencials.Core;

public interface IPrivateKey
{
    string PlainBase64 { get; }
    string PasswordPhrase { get; }
    RSA RsaPrivateKey { get; }
    byte[] PrivateKeyBytes { get; }
    byte[] PasswordPhraseBytes { get; }
    string GetPemRepresentation();
}