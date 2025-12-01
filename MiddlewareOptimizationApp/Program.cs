var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5294);
});

var app = builder.Build();

// 1. Security Logging Middleware
app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode >= 400)
    {
        Console.WriteLine($"Security Event: {context.Request.Path} - Status Code: {context.Response.StatusCode}");
    }
});

// 2. Simulated HTTPS Enforcement Middleware
app.Use(async (context, next) =>
{
    if (context.Request.Query["secure"] != "true")
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Simulated HTTPS required.");
        return;
    }
    await next();
});

// 3. Input Validation Middleware
app.Use(async (context, next) =>
{
    var input = context.Request.Query["input"];
    if (!IsValidInput(input))
    {
        if (!context.Response.HasStarted)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Invalid Input");
        }
        return;
    }

    await next();
});

// 4. Authorization Middleware
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/unauthorized")
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Unauthorized access.");
        return;
    }
    await next();
});

//5. Auithentication and Secure Cookie Middleware
app.Use(async (context, next) =>
{
    // Simulate authentication with a query parameter (e.g., "?authenticated=true")
    var isAuthenticated = context.Request.Query["authenticated"] == "true";
    if (!isAuthenticated)
    {
        if (!context.Response.HasStarted)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Access Denied");
        }
        return;
    }

    context.Response.Cookies.Append("SecureCookie", "SecureData", new CookieOptions
    {
        HttpOnly = true,
        Secure = true
    });

    await next();
});

// 6. Asynchronous Processing Middleware
app.Use(async (context, next) =>
{
    await Task.Delay(100);
    await context.Response.WriteAsync("Processed Asynchronously\n");
    await next();
});

static bool IsValidInput(string? input)
{
    // Checks for any unsafe characters or patterns, including "<script>"
    return string.IsNullOrEmpty(input) || (input.All(char.IsLetterOrDigit) && !input.Contains("<script>"));
}

// Final Response Middleware
app.Run(async (context) =>
{
    if (!context.Response.HasStarted)
    {
        await context.Response.WriteAsync("Final Response from Application\n");
    }
});