// using DIProject;

var builder = WebApplication.CreateBuilder(args);

// --- DI SETUP ---
// This tells the app how to create our service.
builder.Services.AddTransient<IMyService, MyService>();

var app = builder.Build();

// --- MIDDLEWARE PIPELINE ---

// Middleware #1
app.Use(async (context, next) =>
{
    var myService = context.RequestServices.GetRequiredService<IMyService>();
    myService.LogCreation("First Middleware");
    await next.Invoke();
});

// Middleware #2
app.Use(async (context, next) =>
{
    var myService = context.RequestServices.GetRequiredService<IMyService>();
    myService.LogCreation("Second Middleware");
    await next.Invoke();
});

// --- API ENDPOINTS ---
app.MapGet("/", (IMyService myService) =>
{
    myService.LogCreation("Root Endpoint");
    return Results.Ok("Check console for service creation logs");
});

app.Run();