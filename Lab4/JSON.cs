using Newtonsoft.Json;
using System.IO;
using System;
using Lab4.SchemeClasses;

namespace Lab4
{
    internal class JSON
    {
        public static bool IsValidToScheme(string[] rows, Scheme scheme)
        {
            for (int i = 1; i < rows.Length; i++)
            {
                string[] rowElements = rows[i].Split(";");
                for (int j = 0; j < rowElements.Length; j++)
                {
                    string rowСell = rowElements[j];
                    bool isCorrecType = true;
                    switch (scheme.Elements[j].Type)
                    {
                        case "uint":
                            isCorrecType = uint.TryParse(rowСell, out _);
                            break;
                        case "bool":
                            isCorrecType = bool.TryParse(rowСell, out _);
                            break;
                        case "dateTime":
                            isCorrecType = DateTime.TryParse(rowСell, out _);
                            break;
                        case "dateTime?":
                            isCorrecType = DateTime.TryParse(rowСell, out _) || rowСell == "";
                            break;
                    }

                    if (!isCorrecType)
                        return ThrowErrorMessage(scheme.Name, i, j, rowElements[j]);
                }
            }
            return IsColumnsNamesValid(rows[0], scheme);
        }

        private static bool IsColumnsNamesValid(string row, Scheme scheme)
        {
            string[] rowElements = row.Split(";");
            for (int i = 0; i < rowElements.Length; i++)
                if (rowElements[i] != scheme.Elements[i].Name)
                    return ThrowErrorMessage(scheme.Name, 0, i, rowElements[i]);
            return true;
        }

        private static bool ThrowErrorMessage(string? name, int row, int column, string element)
        {
            throw new FormatException($"Wrong type in file '{name}' row '{row}' and column '{column}' element: {element}");
        }

        public static Scheme GetScheme(string path)
        {
            return JsonConvert.DeserializeObject<Scheme>(File.ReadAllText(path));
        }
    }
}