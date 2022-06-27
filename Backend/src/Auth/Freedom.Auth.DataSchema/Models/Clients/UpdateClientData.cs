namespace Freedom.Auth.DataSchema.Models.Clients;

public class UpdateClientData
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string[] RedirectUrls { get; set; } = null!;
    public string Secret { get; set; } = null!;
}
