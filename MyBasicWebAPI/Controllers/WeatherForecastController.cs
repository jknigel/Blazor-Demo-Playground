using Microsoft.AspNetCore.Mvc;

namespace MyWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    // This is a fake, in-memory database to make the API work.
    private static List<WeatherForecast> _database = new();

    // This is just some sample data.
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot"
    };
    
    // GET Method
    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        // For this example, we'll just generate new data every time.
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Id = index,
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();
    }

    // POST Method
    [HttpPost]
    public IActionResult Post([FromBody] WeatherForecast forecast)
    {
        Console.WriteLine("Adding new forecast to the 'database'...");
        _database.Add(forecast);
        return Ok(forecast);
    }

    // PUT Method
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] WeatherForecast forecast)
    { 
        Console.WriteLine($"Updating forecast with ID: {id}");
        // In a real app, you'd find and update the item in _database.
        return NoContent();
    }

    // DELETE Method
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    { 
        Console.WriteLine($"Deleting forecast with ID: {id}");
        // In a real app, you'd find and remove the item from _database.
        return NoContent();
    }
}