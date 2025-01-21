using Microsoft.EntityFrameworkCore;
using Ornit.Backend.src.Shared.AppData;
using Ornit.Backend.src.Shared.Builders;
using Ornit.Backend.src.Shared.Extensions;

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
builder.ConfigureJwtValidation();
builder.ConfigureNamedHttpClients();

services.AddDbContext<AppDbContext>(o =>
{
    var connectionString = builder.Configuration.GetConnectionString("Database");
    if (string.IsNullOrEmpty(connectionString))
    {
        o.UseInMemoryDatabase("InMemoryDb");
    }
    else
    {
        o.UseNpgsql(connectionString);
    }
});

TypeScriptTypeGenerator.Create();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();