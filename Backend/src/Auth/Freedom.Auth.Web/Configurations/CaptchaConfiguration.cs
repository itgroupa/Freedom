namespace Freedom.Auth.Web.Configurations;

public class CaptchaConfiguration
{
    public string SiteKey { get; set; } = null!;
    public string SecretKey { get; set; } = null!;

    public void CopyTo(CaptchaConfiguration conf)
    {
        conf.SiteKey = SiteKey;
        conf.SecretKey = SecretKey;
    }
}
