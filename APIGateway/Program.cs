var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();


app.Run();
