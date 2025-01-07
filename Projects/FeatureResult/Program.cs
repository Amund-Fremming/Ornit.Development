using FeatureResult.src.Shared.AppData;
using FeatureResult.src.Shared.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddControllers();
services.AddLogging();
services.AddResponseCompression(o => o.EnableForHttps = true);

services.AddLibraries();
services.AddRepositories();
services.AddServices();

services.ConfigureSwaggerAuthentication();
builder.ConfigureJwt();

services.AddDbContext<AppDbContext>(o =>
{
    var connectionString = builder.Configuration.GetConnectionString("Database");
    if (String.IsNullOrEmpty(connectionString))
    {
        o.UseInMemoryDatabase("InMemoryDb");
    }
    else
    {
        o.UseNpgsql(connectionString);
    }
});

/*
services.AddAuthentication(o =>
{
    o.DefaultScheme = IdentityConstants.ApplicationScheme;
    o.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    o.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
})
.AddGoogle(o =>
{
    o.ClientId = builder.Configuration["Authentication:Google:ClientId"] ?? throw new KeyNotFoundException("Missing Google client id.");
    o.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"] ?? throw new KeyNotFoundException("Missing Google client secret.");
})
.AddFacebook(o =>
{
    o.ClientId = builder.Configuration["Authentication:Facebook:AppId"] ?? throw new KeyNotFoundException("Missing Facebook app id.");
    o.ClientSecret = builder.Configuration["Authentication:Facebook:AppSecret"] ?? throw new KeyNotFoundException("Missing Facebook app secret.");
});
*/

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();