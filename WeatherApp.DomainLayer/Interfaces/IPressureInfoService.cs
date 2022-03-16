namespace WeatherApp.DomainLayer.Interfaces
{
    public interface IPressureInfoService
    {
        double GetAveragePressure(int id);
        double GetMinPressure(int id);
        double GetMaxPressure(int id);
    }
}
