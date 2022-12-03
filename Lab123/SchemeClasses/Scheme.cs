using Newtonsoft.Json;
using System.Collections.Generic;

namespace Lab123.SchemeClasses
{
    public class Scheme
    {
        [JsonProperty("name")]
        public string? Name { get; private set; }

        [JsonProperty("columns")]
        public List<SchemeColumn> Columns { get; private set; } = new List<SchemeColumn>();
    }
}
