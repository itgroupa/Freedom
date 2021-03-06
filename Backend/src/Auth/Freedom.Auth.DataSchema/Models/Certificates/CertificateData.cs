namespace Freedom.Auth.DataSchema.Models.Certificates;

public class CertificateData
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
}
