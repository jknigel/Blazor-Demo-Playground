var builder = WebApplication.CreateBuilder(args);

// This service tells the app how to use Controller files.
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

// This command tells the app to activate the Controller routes.
app.MapControllers();

app.Run();