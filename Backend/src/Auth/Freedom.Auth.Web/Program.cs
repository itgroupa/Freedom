using System.Net;
using System.Security.Claims;
using Freedom.Auth.Business;
using Freedom.Auth.Cache;
using Freedom.Auth.Dal;
using Freedom.Auth.Web.Configurations;
using Freedom.Auth.Web.Const;
using Freedom.Auth.Web.Interfaces;
using Freedom.Auth.Web.Services;
using Freedom.Common.Mapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

var HttpClientHandler = () => new HttpClientHandler
{
    ClientCertificateOptions = ClientCertificateOption.Manual,
    DefaultProxyCredentials = CredentialCache.DefaultCredentials
};

var builder = WebApplication.CreateBuilder(args);

var redisConfiguration = builder.Configuration.GetSection("Redis");
var mongoConfiguration = builder.Configuration.GetSection("Mongo");
var captchaConfiguration = builder.Configuration.GetSection("Captcha");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddLocalization();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization(config =>
{
    config.AddPolicy(Policies.AuthPolicy, policy =>
    {
        policy.RequireClaim(ClaimTypes.Role);
        policy.RequireClaim(ClaimTypes.Email);
        policy.RequireClaim(ClaimTypes.Name);
        policy.RequireClaim(ClaimTypes.NameIdentifier);
    });
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IAuthorizationService, FreedomAuthorizationService>();
builder.Services.AddTransient<AuthenticationStateProvider, FreedomAuthenticationStateProvider>();
builder.Services.AddTransient<ISessionService, SessionService>();

builder.Services.AddMapperFiles();

builder.Services.AddCache(redisConfiguration);
builder.Services.AddDal(mongoConfiguration);
builder.Services.AddBusiness();

builder.Services.AddTransient<IUserViewService, UserViewService>();
builder.Services.AddHttpClient<ICaptchaVerificationService, CaptchaVerificationService>()
    .ConfigurePrimaryHttpMessageHandler(HttpClientHandler);

builder.Services.Configure<CaptchaConfiguration>(captchaConfiguration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
