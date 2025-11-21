var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMyService, MyService>();

var app = builder.Build();

app.Use(async (context, next) =>
{
    var myService = context.RequestServices.GetRequiredService<IMyService>();
    myService.LogCreation("First Middleware");
    await next.Invoke();
});

app.Use(async (context, next) =>
{
    var myService = context.RequestServices.GetRequiredService<IMyService>();
    myService.LogCreation("Second Middleware");
    await next.Invoke();
});

app.MapGet("/", (IMyService myService) =>
{
    myService.LogCreation("Root");
    return Results.Ok("Check the console for service creation logs");
});

app.Run();

public interface IMyService
{
    void LogCreation(string message);
}

public class MyService: IMyService
{
    private int _serviceId;

    public MyService()
    {
        _serviceId = new Random().Next(1, 1000);
    }

    public void LogCreation(string message)
    {
        Console.WriteLine($"Service ID: {_serviceId} - {message}");
    }
}