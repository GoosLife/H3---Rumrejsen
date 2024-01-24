using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace H3___Rumrejsen.Models
{
    public class GalacticRoute
    {
        [JsonProperty("name")]
        public string Name { get; }

        [JsonProperty("start")]
        public string Start { get; }

        [JsonProperty("end")]
        public string End { get; }

        [JsonProperty("navigationPoints")]
        public string[] NavigationPoints { get; }

        [JsonProperty("duration")]
        public string Duration { get; }

        [JsonProperty("dangers")]
        public string[] Dangers { get; }

        [JsonProperty("fuelUsage")]
        public string FuelUsage { get; }

        [JsonProperty("description")]
        public string Description { get; }
    }
}
