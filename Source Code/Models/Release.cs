using System.Collections.Generic;
using Newtonsoft.Json;

namespace Models
{
    public class Release
    {
        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("deployments")]
        public ICollection<Deployment> Deployments { get; set; }
    }
}
