namespace WeatherApp.DomainLayer.Interfaces
{
    public interface IHumidityInfoService
    {
        double GetAverageHumidity(int id);
        double GetMinHumidity(int id);
        double GetMaxHumidity(int id);
    }
}
