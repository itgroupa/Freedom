namespace Freedom.Auth.DataSchema.Models.Certificates;

public class AddCertificateData
{
    public byte[] Data { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
}
