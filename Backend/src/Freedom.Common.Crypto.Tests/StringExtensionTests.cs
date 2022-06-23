using NUnit.Framework;

namespace Freedom.Common.Crypto.Tests;

public class StringExtensionTests
{
    [Test]
    public void Md5Test()
    {
        const string password = "Test";

        var md5 = password.GetHashMd5();

        Assert.IsTrue(md5 == "0CBC6611F5540BD0809A388DC95A615B");
    }
}
