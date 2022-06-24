using Freedom.Common.Json.Tests.Models;
using NUnit.Framework;

namespace Freedom.Common.Json.Tests;

public class JsonConvertTests
{
    private const string TesterName = "Alexey";
    private readonly string _sourceString = "{\r\n  " + '"' + "name" + '"' + ": " + '"' + "Alexey" + '"' + "\r\n}";

    [Test]
    public void GetStringTest()
    {
        var model = new TestModel
        {
            Name = TesterName
        };

        var json = JsonConvert.GetJsonObj(model);

        Assert.IsTrue(json == _sourceString);
    }

    [Test]
    public void GetObjectTest()
    {
        var convertResult = JsonConvert.GetObjFromJson<TestModel>(_sourceString);

        Assert.IsTrue(convertResult?.Name == TesterName);
    }
}
