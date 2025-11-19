public class WeatherForecast
{
    public int Id { get; set; } // We need an ID for PUT and DELETE
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
}