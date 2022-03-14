using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace WeatherApp.APIProviders.Models
{
    public class Clouds
    {
        [JsonPropertyName("all")]
        public int All { get; set; }
    }
}
