namespace WeatherApp.DomainLayer.Services.Interfaces
{
    public interface ITemperatureInfoService
    {
        double GetAverageTemperature(int id);
        double GetMinTemperature(int id);
        double GetMaxTemperature(int id);
    }
}
