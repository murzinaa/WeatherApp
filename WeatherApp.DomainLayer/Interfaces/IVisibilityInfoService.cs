namespace WeatherApp.DomainLayer.Interfaces
{
    public interface IVisibilityInfoService
    {
        double GetAverageVisibility(int id);
        double GetMinVisibility(int id);
        double GetMaxVisibility(int id);
    }
}
