namespace Freedom.Auth.DataSchema.Models.Certificates;

public class UpdateCertificateData
{
    public Guid Id { get; set; }
    public byte[] Data { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
}
