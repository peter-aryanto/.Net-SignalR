namespace AppSettings
{
  public class AppSettings
  {
    public AppSettings()
    {
      AppSettingsProvider.Settings = this;
    }

    public string Fish1 { get; set; }
  }

  internal static class AppSettingsProvider
  {
    internal static AppSettings Settings { get; set; }
  }
}