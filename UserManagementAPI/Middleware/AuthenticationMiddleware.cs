namespace UserManagementAPI.Middleware;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKey = "TechHiveSecretKey123";

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // --- THIS IS THE "SMART BOUNCER" LOGIC ---
        // We check if the request path starts with "/api".
        // This is the "VIP area" that we want to protect.
        if (!context.Request.Path.StartsWithSegments("/api"))
        {
            // If the path does NOT start with "/api", it's a public area.
            // We just let the request pass through to the next middleware
            // without doing any key checks.
            await _next(context);
            return;
        }
        // ---------------------------------------------

        // If the code reaches this point, it means the path IS "/api/something",
        // so now we enforce the security check.

        if (!context.Request.Headers.TryGetValue("X-Api-Key", out var extractedApiKey))
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("API Key was not provided.");
            return; // Stop the request
        }

        if (!ApiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("Invalid API Key.");
            return; // Stop the request
        }

        // If the key is valid, let the request proceed to the API controller.
        await _next(context);
    }
}