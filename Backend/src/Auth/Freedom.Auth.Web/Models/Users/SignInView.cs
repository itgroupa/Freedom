using System.ComponentModel.DataAnnotations;
using Freedom.Auth.Web.Models.Captcha;

namespace Freedom.Auth.Web.Models.Users;

public class SignInView : CaptchaBase
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
