namespace Freedom.Auth.DataSchema.Models.Clients;

public class AddClientData
{
    public string Name { get; set; } = null!;
    public string[] RedirectUrls { get; set; } = null!;
    public string Secret { get; set; } = null!;
}
