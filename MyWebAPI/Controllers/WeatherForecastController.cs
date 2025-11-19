using Microsoft.AspNetCore.Mvc;

namespace MyWebAPI.Controllers;

// These attributes go ABOVE the class definition.
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    // The 'Summaries' array goes INSIDE the class.
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    // The GET Method goes INSIDE the class.
    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        var rng = new Random();
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = rng.Next(-20, 55),
            Summary = Summaries[rng.Next(Summaries.Length)]
        }).ToArray();
    }

    // The POST Method goes INSIDE the class.
    [HttpPost]
    public IActionResult Post([FromBody] WeatherForecast forecast)
    {
        // For this example, we'll just log it to the console.
        Console.WriteLine($"Received new forecast for {forecast.Date}: {forecast.Summary}");
        return Ok(forecast); // Sends the same data back with a "200 OK" status.
    }

    // The PUT Method goes INSIDE the class.
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] WeatherForecast forecast)
    {
        // This is just a placeholder to make the code work.
        Console.WriteLine($"Received update for ID {id}.");
        return NoContent(); // "204 No Content" is a standard success response for an update.
    }

    // The DELETE Method goes INSIDE the class.
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        // This is just a placeholder.
        Console.WriteLine($"Received delete request for ID {id}.");
        return NoContent(); // "204 No Content" is a standard success response for a delete.
    }
}