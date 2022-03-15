using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.DomainLayer.Exeptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
