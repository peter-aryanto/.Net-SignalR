using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AppSettings.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly IServiceScopeFactory _svcScopeFactory;
    private readonly ILogger<WeatherForecastController> _logger;

    // public WeatherForecastController(ILogger<WeatherForecastController> logger)
    public WeatherForecastController(
      Microsoft.Extensions.Options.IOptions<AppSettings> appSettings,
      IServiceProvider serviceProvider,
      IServiceScopeFactory svcScopeFactory,
      ILogger<WeatherForecastController> logger
    )
    {
        _logger = logger;
        var _appSettings = appSettings.Value;
        var _sp = serviceProvider;
        _svcScopeFactory = svcScopeFactory;
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

    [HttpGet("Subscribe")]
    public IActionResult Subscribe()
    {
      Task.Run(() => {
        using var svcScope = _svcScopeFactory.CreateScope();
        var clientSideMessaging = svcScope.ServiceProvider.GetRequiredService<IHubContext<Messaging.ClientSideMessaging>>();

        var targetClients = clientSideMessaging.Clients.All;
        const string clientEvent = "ProcessMessageFromServer";
        Task SendUpdateAsync(string message) => targetClients.SendAsync(clientEvent, message);

        for (int index = 1; index <= 5; index++)
        {
          string message = $"CurrentTime is {DateTime.Now.ToString()}";
          SendUpdateAsync(message);
          System.Threading.Thread.Sleep(3000);
        }
        SendUpdateAsync("Good bye...");
      });

      return Ok();
    }
}
