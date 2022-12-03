using System.IO;
using Lab123.SchemeClasses;
using Newtonsoft.Json;

namespace Lab123
{
    internal class JSON
    {
        public static Scheme GetScheme(string path)
        {
            return JsonConvert.DeserializeObject<Scheme>(File.ReadAllText(path));
        }
    }
}