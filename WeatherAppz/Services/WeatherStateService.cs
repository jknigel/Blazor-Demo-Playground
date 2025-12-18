using System.Net.Http.Json;
using WeatherAppz.Models;
namespace WeatherAppz.Services;
public class WeatherStateService : IDisposable
{
    public WeatherData? CurrentWeather { get; private set; }
    public List<User>? Users { get; private set; }
    public bool IsLoading { get; private set; }

    public event Action? OnStateChange;

    private readonly HttpClient _http;
    private CancellationTokenSource? _cts;

    public WeatherStateService(HttpClient http)
    {
        _http = http;
    }

    public async Task FetchWeatherAsync(string location)
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();
        IsLoading = true;
        NotifyStateChanged();

        try
        {
            var apiKey = "YOUR_API_KEY";
            CurrentWeather = await _http.GetFromJsonAsync<WeatherData>(
                $"https://api.weatherapi.com/v1/current.json?key={apiKey}&q={location}", _cts.Token);
        }
        catch (OperationCanceledException) { /* Ignored */ }
        catch (Exception ex) { Console.WriteLine(ex.Message); CurrentWeather = null; }

        IsLoading = false;
        NotifyStateChanged();
    }

    public async Task FetchUsersAsync()
    {
        _cts?.Cancel();
        _cts = new CancellationTokenSource();
        IsLoading = true;
        NotifyStateChanged();

        try
        {
            Users = await _http.GetFromJsonAsync<List<User>>(
                "https://jsonplaceholder.typicode.com/users", _cts.Token);
        }
        catch (OperationCanceledException) { /* Ignored */ }
        catch (Exception ex) { Console.WriteLine(ex.Message); Users = null; }

        IsLoading = false;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnStateChange?.Invoke();

    public void Dispose()
    {
        _cts?.Dispose();
    }
}