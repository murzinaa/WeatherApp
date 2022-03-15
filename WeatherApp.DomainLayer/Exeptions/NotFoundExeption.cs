using System;

namespace WeatherApp.DomainLayer.Exeptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
