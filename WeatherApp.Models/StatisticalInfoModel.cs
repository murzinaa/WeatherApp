namespace WeatherApp.Models
{
    public class StatisticalInfoModel
    {
        public int id { get; set; }
        public string CityName { get; set; }
        public TemperatureInfoModel Temperature { get; set; }
        public VisibilityInfoModel Visibility { get; set; }
        public HumidityInfoModel Humidity { get; set; }
        public PressureInfoModel Pressure { get; set; }
    }
    
}
