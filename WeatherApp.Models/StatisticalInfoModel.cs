namespace WeatherApp.Models
{
    public class StatisticalInfoModel
    {
        public int id { get; set; }
        public string CityName { get; set; }
        public double CurrentTemperature { get; set; }
        public double AverageTemperature { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
    }
}
