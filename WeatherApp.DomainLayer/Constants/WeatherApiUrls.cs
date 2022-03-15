namespace WeatherApp.DomainLayer.Constants
{
    public static class WeatherApiUrls
    {
        public static string ReturnUrl(string cityName, string apiKey)
        {
            return $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&units=metric&appid={apiKey}";
        }

    }
}
