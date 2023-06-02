using Microsoft.AspNetCore.Mvc;

namespace AppSettings.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    // public WeatherForecastController(ILogger<WeatherForecastController> logger)
    public WeatherForecastController(
      Microsoft.Extensions.Options.IOptions<AppSettings> appSettings,
      IServiceProvider serviceProvider,
      ILogger<WeatherForecastController> logger
    )
    {
        _logger = logger;
        var _appSettings = appSettings.Value;
        var _sp = serviceProvider;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("Returns the forecast");
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            // Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            Summary = $"{AppSettingsProvider.Settings.Fish1} says: {Summaries[Random.Shared.Next(Summaries.Length)]}"
        })
        .ToArray();
    }
}
