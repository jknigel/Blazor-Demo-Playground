var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed(_ => true);
    });
});

var app = builder.Build();
app.UseCors();
//app.UseHttpsRedirection();

app.MapGet("/products", () =>
    new[] { new { Id = 1, Name = "Laptop" }, new { Id = 2, Name = "Phone" } });

app.Run();
