using Freedom.Auth.Web.Models.Captcha;

namespace Freedom.Auth.Web.Interfaces;

public interface ICaptchaVerificationService
{
    Task<bool> IsCaptchaValid(CaptchaBase model);
}
