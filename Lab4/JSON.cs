using Newtonsoft.Json;
using System.IO;
using Lab4.SchemeClasses;

namespace Lab4
{
    internal class JSON
    {
        public static Scheme GetScheme(string path)
        {
            return JsonConvert.DeserializeObject<Scheme>(File.ReadAllText(path));
        }
    }
}