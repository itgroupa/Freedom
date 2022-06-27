﻿namespace Freedom.Auth.Business.Models.Clients;

public class UpdateClientBusiness
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string[] RedirectUrls { get; set; } = null!;
    public string Secret { get; set; } = null!;
}
