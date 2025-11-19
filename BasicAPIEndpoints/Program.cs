var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Root Path");
app.MapGet("/downloads", () => "Downloads Path");
app.MapPut("/", () => "This is a PUT request to the Root Path");
app.MapDelete("/", () => "This is a DELETE request to the Root Path");
app.MapPost("/", () => "This is a POST request to the Root Path");

app.Run();
