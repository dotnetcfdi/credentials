using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Credencials.Common;

namespace Credencials.Core
{
    public class Certificate : ICertificate
    {
        private readonly X509Certificate2 _x509Certificate2;

        public Certificate(string plainBase64)
        {
            PlainBase64 = plainBase64;
            _x509Certificate2 = new X509Certificate2(CertificatePlainBytes);
        }


        public string PlainBase64 { get; }


        public byte[] CertificatePlainBytes
        {
            get => Convert.FromBase64String(PlainBase64);
        }


        /// <summary>
        /// Tax identification number. In México is RFC.
        /// </summary>
        public string? Subject
        {
            get => _x509Certificate2?.Subject;
        }

        /// <summary>
        /// Issuer
        /// </summary>
        public string? Issuer
        {
            get => _x509Certificate2?.Issuer;
        }

        /// <summary>
        /// Version
        /// </summary>
        public int Version
        {
            get => _x509Certificate2.Version;
        }

        /// <summary>
        /// Valid Date
        /// </summary>
        public DateTime NotBefore
        {
            get => _x509Certificate2.NotBefore;
        }

        /// <summary>
        /// Expiry Date
        /// </summary>
        public DateTime NotAfter
        {
            get => _x509Certificate2.NotAfter;
        }

        /// <summary>
        /// Thumbprint
        /// </summary>
        public string Thumbprint
        {
            get => _x509Certificate2.Thumbprint;
        }

        /// <summary>
        /// Serial Number
        /// </summary>
        public string SerialNumber
        {
            get => _x509Certificate2.SerialNumber;
        }

        /// <summary>
        /// Friendly Name
        /// </summary>
        public string? FriendlyName
        {
            get => _x509Certificate2.PublicKey.Oid.FriendlyName;
        }

        /// <summary>
        /// Public Key Format
        /// </summary>
        public string PublicKeyFormat
        {
            get => _x509Certificate2.PublicKey.EncodedKeyValue.Format(true);
        }

        /// <summary>
        /// Raw Data Length
        /// </summary>
        public int RawDataLength
        {
            get => _x509Certificate2.RawData.Length;
        }

        /// <summary>
        /// Certificate Raw Data 
        /// </summary>
        public byte[] RawDataBytes
        {
            get => _x509Certificate2.RawData;
        }


        public string GetPemRepresentation()
        {
            var certPem = new string(PemEncoding.Write(Flags.PemCertificate, _x509Certificate2.RawData));

            return certPem;
        }
    }
}