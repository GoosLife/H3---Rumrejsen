using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace H3___Rumrejsen.Models
{
    public class GalacticRoute
    {
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("start")]
        public string Start { get; private set; }

        [JsonProperty("end")]
        public string End { get; private set; }

        [JsonProperty("navigationPoints")]
        public string[] NavigationPoints { get; private set; }

        [JsonProperty("duration")]
        public string Duration { get; private set; }

        [JsonProperty("dangers")]
        public string[] Dangers { get; private set; }

        [JsonProperty("fuelUsage")]
        public string FuelUsage { get; private set; }

        [JsonProperty("description")]
        public string Description { get; private set; }
    }
}
