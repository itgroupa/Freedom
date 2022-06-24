using System.Security.Cryptography.X509Certificates;

namespace Freedom.Common.Crypto.Models;

public class GeneratedCertificate
{
    public string PublicPart { get; set; } = null!;
    public string PrivatePart { get; set; } = null!;
    public X509Certificate ClientCertificate { get; set; } = null!;
}
