using System.Collections.Generic;
using Newtonsoft.Json;

namespace Models
{
    public class Project
    {
        [JsonProperty("project_id")]
        public string Id { get; set; }
        
        [JsonProperty("project_group")]
        public string Group { get; set; }

        [JsonProperty("environments")]
        public ICollection<Environment> Environments { get; set; }

        [JsonProperty("releases")]
        public ICollection<Release> Releases { get; set; }
    }
}
