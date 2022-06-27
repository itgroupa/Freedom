namespace Freedom.Auth.Business.Models.Certificates;

public class UpdateCertificateBusiness
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string Password { get; set; } = null!;
}
