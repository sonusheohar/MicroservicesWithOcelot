using Ocelot.DependencyInjection;
using Ocelot.Middleware;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddEndpointsApiExplorer();

#region Configure Ocelot dependency 
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot();
#endregion

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

// use UseOcelot
await app.UseOcelot();

app.Run();
