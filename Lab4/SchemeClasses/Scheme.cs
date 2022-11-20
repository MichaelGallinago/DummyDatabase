using Newtonsoft.Json;
using System.Collections.Generic;

namespace Lab4.SchemeClasses
{
    class Scheme
    {
        [JsonProperty(PropertyName = "name")]
        public string? Name { get; private set; }

        [JsonProperty("columns")]
        public List<SchemeColumn> Elements { get; private set; } = new List<SchemeColumn>();
    }
}