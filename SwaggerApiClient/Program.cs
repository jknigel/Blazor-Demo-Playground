using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;
using CustomNamespace;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
        app.MapControllers();

        app.Run();
        //Task.Run(() => app.RunAsync());
        // await Task.Delay(3000);
        // await new ClientGenerator().GenerateClient();
        var httpClient = new HttpClient();
        var client = new CustomApiClient("http://localhost:8080", httpClient);
        var user = await client.UserAsync(1);
        Console.WriteLine($"Fetched User: {user}");
    }
}   
