var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpLogging();

app.Use(async (context, next) =>
{
    Console.WriteLine($"Request Path: {context.Request.Path}");
    await next();
    Console.WriteLine($"Response Status Code: {context.Response.StatusCode}");
    //return Task.CompletedTask;
});

app.Use(async (context, next) =>
{
    var startTime = DateTime.UtcNow;
    await next();
    var duration = DateTime.UtcNow - startTime;
    Console.WriteLine($"Response Time: {duration.TotalMilliseconds}ms");
    //return Task.CompletedTask;
});

app.MapGet("/", () => "Hello World!");

app.Run();
