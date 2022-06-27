using Freedom.Auth.Business;
using Freedom.Auth.Cache;
using Freedom.Auth.Dal;
using Freedom.Auth.DataSchema;
using Freedom.Common.Mapper;

var builder = WebApplication.CreateBuilder(args);

var redisConfiguration = builder.Configuration.GetSection("Redis");
var mongoConfiguration = builder.Configuration.GetSection("Mongo");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddMapperFiles();

builder.Services.AddCache(redisConfiguration);
builder.Services.AddDal(mongoConfiguration);
builder.Services.AddBusiness();

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
