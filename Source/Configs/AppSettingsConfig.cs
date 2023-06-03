namespace AppSettings;

internal static class AppSettingsConfig
{
  public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfigurationSection appSettings)
  {
    services.Configure<AppSettings>(appSettings);
    return services;
  } 
}