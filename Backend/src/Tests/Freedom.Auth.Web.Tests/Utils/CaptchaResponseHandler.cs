using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Freedom.Auth.Web.Models.Captcha;
using Freedom.Common.Json;

namespace Freedom.Auth.Web.Tests.Utils;

public class CaptchaResponseHandler: DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var result = new HttpResponseMessage();
        result.Content = new StringContent(JsonConvert.GetJsonObj(new CaptchaResponseView
        {
            Success = true
        }));
        return result;
    }
}
