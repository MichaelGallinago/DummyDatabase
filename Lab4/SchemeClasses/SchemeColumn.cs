using Newtonsoft.Json;

namespace Lab4.SchemeClasses
{
    public class SchemeColumn
    {
        [JsonProperty("name")]
        public string? Name { get; private set; }

        [JsonProperty("type")]
        public string? Type { get; private set; }
    }
}