using Freedom.Auth.Web.Configurations;
using Freedom.Auth.Web.Interfaces;
using Freedom.Auth.Web.Models.Captcha;
using Freedom.Common.Json;
using Microsoft.Extensions.Options;

namespace Freedom.Auth.Web.Services;

internal class CaptchaVerificationService : ICaptchaVerificationService
{
    private readonly HttpClient _httpClient;
    private readonly CaptchaConfiguration _captchaConfiguration;

    public CaptchaVerificationService(IOptions<CaptchaConfiguration> captchaConfiguration, HttpClient httpClient)
    {
        _httpClient = httpClient;
        _captchaConfiguration = captchaConfiguration.Value;
    }

    public async Task<bool> IsCaptchaValid(CaptchaBase model)
    {
        const string googleVerificationUrl = "https://www.google.com/recaptcha/api/siteverify";


        var response = await _httpClient.PostAsync($"{googleVerificationUrl}?secret={_captchaConfiguration.SecretKey}&response={model.Token}", null);
        var jsonString = await response.Content.ReadAsStringAsync();
        var captchaVerification = JsonConvert.GetObjFromJson<CaptchaResponseView>(jsonString);

        if (captchaVerification == null) throw new NullReferenceException("Captcha is not valid");

        return captchaVerification.Success;
    }
}
