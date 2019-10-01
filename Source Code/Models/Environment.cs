using Newtonsoft.Json;

namespace Models
{
    public class Environment
    {
        [JsonProperty("environment")]
        public string Name { get; set; }
    }
}
