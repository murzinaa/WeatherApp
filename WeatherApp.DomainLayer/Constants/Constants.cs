namespace WeatherApp.DomainLayer.Constants
{
    public static class Constants
    {
        public static class ExceptionMessages
        {
            public static class Temperature
            {
                public const string NotFoundException = "Weather condition cannot be found";
            }

            public static class City
            {
                public const string NotFoundException = "City cannot be found";
                public const string CityAlreadyCreated = "City is already created";
            }
        }
    }
}
