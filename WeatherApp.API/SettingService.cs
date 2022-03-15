namespace WeatherApp.API
{
    public class SettingService
    {
        public string ApiKey { get; }

        public SettingService(string apiKey)
        {
            ApiKey = apiKey;
        }
    }
}
