using System.Security.Cryptography.X509Certificates;

namespace Credencials.Core
{
    internal class Fiel
    {
        private readonly X509Certificate2 certificateWithPrivateKey;


        public Fiel(ICertificate certificate, IPrivateKey privateKey)
        {
            Certificate = certificate;
            PrivateKey = privateKey;
            PasswordPhrase = privateKey.PasswordPhrase;

            PemCertificate = certificate.GetPemRepresentation();
            PemPrivateKey = privateKey?.GetPemRepresentation();

            certificateWithPrivateKey = X509Certificate2.CreateFromPem(PemCertificate, PemPrivateKey);
        }


        public string? PasswordPhrase { get; }

        public ICertificate? Certificate { get; }

        public IPrivateKey? PrivateKey { get; }

        public string? PemCertificate { get; }

        public string? PemPrivateKey { get; }

        public byte[] CreatePFX()
        {
            var pfxBytes = certificateWithPrivateKey.Export(X509ContentType.Pfx, PasswordPhrase);

            return pfxBytes;
        }
    }
}