using Joke.Server.Extensions;
using Joke.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddHttpClient();
builder.Services.AddServices();

// Add Yarp Reverse Proxy for Local Development
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseStaticFiles();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// SPA-specific routing
if (app.Environment.IsDevelopment())
{
    var spaDevServer = app.Configuration.GetValue<string>("SpaDevServerUrl");
    if (!string.IsNullOrEmpty(spaDevServer))
    {
        // proxy any non API requests that we think should go to the vite dev server
        app.MapReverseProxy();
    }
}

app.Run();
