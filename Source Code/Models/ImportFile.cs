using System.Collections.Generic;
using Newtonsoft.Json;

namespace Models
{
    public class ImportFile
    {
        [JsonProperty("projects")]
        public ICollection<Project> Projects { get; set; }
    }
}
