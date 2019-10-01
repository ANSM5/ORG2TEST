using System;
using Newtonsoft.Json;

namespace Models
{
    public class Deployment
    {
        [JsonProperty("environment")]
        public string Environment { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
