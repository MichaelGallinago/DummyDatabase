using Newtonsoft.Json;

namespace Lab123
{
    internal class JSON
    {
        public static bool IsValidToScheme(string[] rows, Scheme scheme)
        {
            for (int i = 1; i < rows.Length; i++)
            {
                string[] rowСells = rows[i].Split(";");
                for (int j = 0; j < rowСells.Length; j++)
                {
                    string rowСell = rowСells[j];
                    bool isCorrecType = true;
                    switch (scheme.Elements[j].Type)
                    {
                        case "uint": isCorrecType = uint.TryParse(rowСell, out _); break;
                        case "bool": isCorrecType = bool.TryParse(rowСell, out _); break;
                        case "dateTime":  isCorrecType = DateTime.TryParse(rowСell, out _); break;
                        case "dateTime'": isCorrecType = DateTime.TryParse(rowСell, out _) || rowСell == ""; break;
                    }

                    if (!isCorrecType) 
                        return ThrowErrorMessage(i, j, rowСells[j]);
                }
            }
            return IsColumnsNamesValid(rows[0], scheme);
        }

        private static bool IsColumnsNamesValid(string row, Scheme scheme)
        {
            string[] rowСells = row.Split(";");
            for (int i = 0; i < rowСells.Length; i++)
                if (rowСells[i] != scheme.Elements[i].Name)
                    return ThrowErrorMessage(0, i, rowСells[i]);
            return true;
        }

        private static bool ThrowErrorMessage(int row, int column, string element)
        {
            throw new FormatException($"Wrong type in row {row} and column {column} element: {element}");
        }

        public static Scheme GetScheme(string path)
        {
            return JsonConvert.DeserializeObject<Scheme>(File.ReadAllText(path));
        }
    }

    class SchemeElement
    {
        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("type")]
        public string Type { get; private set; }
    }

    class Scheme
    {
        [JsonProperty("columns")]
        public List<SchemeElement> Elements { get; private set; } = new List<SchemeElement>();
    }
}
