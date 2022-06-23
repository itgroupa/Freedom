using System.Security.Cryptography.X509Certificates;

namespace Freedom.Common.Crypto.Models;

public class GeneratedCertificate
{
    public string PublicPart { get; set; }
    public string PrivatePart { get; set; }
    public X509Certificate ClientCertificate { get; set; }
}
