using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using APIGateway.Middlewares;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddEndpointsApiExplorer();

#region Configure Ocelot dependency 
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot().AddCacheManager(x =>
{
    x.WithDictionaryHandle();
});
#endregion

#region configure Cors Origin usin AddCores() method
builder.Services.AddCors(option=>{
    option.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});
#endregion

var app = builder.Build();

// add UseCors middleware here
app.UseCors();
app.UseHttpsRedirection();
app.UseMiddleware<TokenCheckerMiddleware>();
app.UseMiddleware<InterceptionMiddleware>();

app.UseAuthorization();

// use UseOcelot
await app.UseOcelot();

app.Run();
