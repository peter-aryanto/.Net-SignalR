using AppSettings.Controllers;
using Microsoft.Extensions.Options;
using Moq;
// using Microsoft.AspNetCore.Mvc;
using AppSettings;
using Microsoft.Extensions.Logging;

namespace UnitTest;

public class WeatherForecastControllerTest
{
  private const string EXPECTED_FISH = "Piranha";

  private readonly WeatherForecastController _cont;

  public WeatherForecastControllerTest()
  {
    var appSettings = new AppSettings.AppSettings { Fish1 = EXPECTED_FISH };
    var appSettingsMock = new Mock<IOptions<AppSettings.AppSettings>>();
    appSettingsMock.Setup(x => x.Value).Returns(appSettings);

    var logger = new Mock<ILogger<WeatherForecastController>>();

    _cont = new WeatherForecastController(
      appSettingsMock.Object,
      null,
      logger.Object
    );
  }

  [Fact]
  public void ReturnsCorrectResult()
  {
    var response = _cont.Get();
    // var okResponse = Assert.IsType<OkObjectResult>(response).Value;
    // var result = Assert.IsAssignableFrom<IEnumerable<WeatherForecast>>(okResponse);
    var result = Assert.IsAssignableFrom<IEnumerable<WeatherForecast>>(response);
    Assert.Contains(EXPECTED_FISH, result.First().Summary);
    Assert.Equal(5, result.ToList().Count);
  }
}