var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpLogging((o)=>{});

var app = builder.Build();

//app.UseRouting();
//app.UseAuthorization();
//app.UseAuthentication();
//app.UseExceptionHandler();

app.Use(async (context, next) =>
{
    //Logic here
    Console.WriteLine("Custom Middleware Executing...");
    await next.Invoke();
    Console.WriteLine("Custom Middleware Executed.");
     
});
app.UseHttpLogging(); //Middleware is meant for running on every route!
app.Use(async (context, next) =>
{
    //Logic here
    Console.WriteLine("Custom Middleware 1 Executing...");
    await next.Invoke();
    Console.WriteLine("Custom Middleware 1 Executed.");
    
});
app.Use(async (context, next) =>
{
    //Logic here
    Console.WriteLine("Custom Middleware 2 Executing...");
    await next.Invoke();
    Console.WriteLine("Custom Middleware 2 Executed.");
    
});

 
app.MapGet("/", () => "Hello World!");
app.MapGet("/hello", () => "This is the hello route");

// app.UseEndpoints();

app.Run();
