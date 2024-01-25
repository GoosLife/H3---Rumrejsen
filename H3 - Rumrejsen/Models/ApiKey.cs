using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace H3___Rumrejsen.Models
{
    public class ApiKey
    {
        [JsonProperty("key")]
        public string Key { get; private set; }
        [JsonProperty("type")]
        public string Type { get; private set; }
        [JsonProperty("expires")]
        public DateTime? Expires { get; private set; }

        public virtual bool IsValid()
        {
            if (DateTime.Now > Expires)
            {
                return false;
            }

            return true;
        }
    }
}
