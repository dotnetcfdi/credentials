using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Credencials.Common;

namespace Credencials.Core
{
    public class PrivateKey : IPrivateKey
    {
        public PrivateKey(string plainBase64, string passwordPhrase)
        {
            PasswordPhrase = passwordPhrase;
            PlainBase64 = plainBase64;
            RsaPrivateKey = RSA.Create();
            RsaPrivateKey.ImportEncryptedPkcs8PrivateKey(PasswordPhraseBytes, PrivateKeyBytes, out _);
        }

        public string PlainBase64 { get; }
        public string PasswordPhrase { get; }
        public RSA RsaPrivateKey { get; }

        public byte[] PrivateKeyBytes
        {
            get => Convert.FromBase64String(PlainBase64);
        }

        public byte[] PasswordPhraseBytes
        {
            get => Encoding.ASCII.GetBytes(PasswordPhrase);
        }


        public string GetPemRepresentation()
        {
            var keyPem = new string(PemEncoding.Write(Flags.PemPrivateKey, RsaPrivateKey.ExportPkcs8PrivateKey()));
            return keyPem;
        }

        private string SignData(string password, byte[] pfx, string toSign, HashAlgorithmName algorithm)
        {
            //get bytes array to sing
            var bytesToSign = toSign.GetBytes();

            //Sing and get signed bytes array
            var signedBytes = RsaPrivateKey.SignData(bytesToSign, algorithm, RSASignaturePadding.Pss);

            //Converts signed bytes to base64
            return signedBytes.ToBase64String();
        }
    }
}