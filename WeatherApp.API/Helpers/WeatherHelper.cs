using System;

namespace WeatherApp.API.Helpers
{
    public class WeatherHelper
    {
        public DateTime GetDateTime(string dateTime)
        {
            DateTime date;

            if (dateTime == null)
                date = DateTime.Now;
            else
                date = DateTime.ParseExact(dateTime, "yyyy-MM-dd HH:mm:ss",
                                   System.Globalization.CultureInfo.InvariantCulture);
            return date;
        }
    }
}
