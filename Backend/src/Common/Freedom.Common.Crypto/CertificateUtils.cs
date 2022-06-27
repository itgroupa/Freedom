using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Freedom.Common.Crypto.Models;

namespace Freedom.Common.Crypto;

public static class CertificateUtils
{
    public static X509Certificate2 GenerateSelfSignedCertificate(string url, DateTimeOffset expiration)
    {
        var rsaKey = RSA.Create(2048);

        var subject = $"CN={url}";

        var certReq = new CertificateRequest(subject, rsaKey, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        certReq.CertificateExtensions.Add(new X509BasicConstraintsExtension(true, false, 0, true));
        certReq.CertificateExtensions.Add(new X509SubjectKeyIdentifierExtension(certReq.PublicKey, false));

        var caCert = certReq.CreateSelfSigned(DateTimeOffset.Now, expiration);

        return caCert;
    }

    public static GeneratedCertificate GetCertificate(X509Certificate2 certificate, string url, DateTimeOffset expiration)
    {
        var subject = $"CN={url}";

        var clientKey = RSA.Create(2048);

        var clientReq = new CertificateRequest(subject, clientKey, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        clientReq.CertificateExtensions.Add(new X509BasicConstraintsExtension(false, false, 0, false));
        clientReq.CertificateExtensions.Add(
            new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature | X509KeyUsageFlags.NonRepudiation, false));
        clientReq.CertificateExtensions.Add(new X509SubjectKeyIdentifierExtension(clientReq.PublicKey, false));

        var serialNumber = BitConverter.GetBytes(DateTime.Now.ToBinary());

        var clientCert = clientReq.Create(certificate, DateTimeOffset.Now, expiration, serialNumber);

        var publicKey = new StringBuilder();
        publicKey.AppendLine("-----BEGIN CERTIFICATE-----");
        publicKey.AppendLine(Convert.ToBase64String(clientCert.RawData, Base64FormattingOptions.InsertLineBreaks));
        publicKey.AppendLine("-----END CERTIFICATE-----");

        var algorithmName = clientKey.SignatureAlgorithm.ToUpper();
        var privateKey = new StringBuilder();
        privateKey.AppendLine($"-----BEGIN {algorithmName} PRIVATE KEY-----");
        privateKey.AppendLine(Convert.ToBase64String(clientKey.ExportRSAPrivateKey(), Base64FormattingOptions.InsertLineBreaks));
        privateKey.AppendLine($"-----END {algorithmName} PRIVATE KEY-----");

        var result = new GeneratedCertificate
        {
            ClientCertificate = clientCert,
            PrivatePart = privateKey.ToString(),
            PublicPart = publicKey.ToString()
        };

        return result;
    }

    public static byte[] ExportCertificate(X509Certificate certificate, string password)
    {
        var bytes = certificate.Export(X509ContentType.Cert, password);

        return bytes;
    }
}
