using System;
using NUnit.Framework;

namespace Freedom.Common.Crypto.Tests;

public class CertificationTests
{
    [Test]
    public void GenerateCertificationTest()
    {
        const string name = "test.com";
        const string password = "test";

        var expiration  = DateTimeOffset.UtcNow.AddYears(5);

        var selfSignedCertificate = CertificateUtils.GenerateSelfSignedCertificate(name, expiration);

        var certificateResult = CertificateUtils.GetCertificate(selfSignedCertificate, name, expiration);

        Assert.IsNotEmpty(certificateResult.PrivatePart);
        Assert.IsNotEmpty(certificateResult.PublicPart);

        var finalResult = CertificateUtils.ExportCertificate(certificateResult.ClientCertificate, password);

        Assert.IsNotEmpty(finalResult);
    }
}
