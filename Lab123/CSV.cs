using System;
using System.IO;

namespace Lab123
{
    internal class CSV
    {
        public static string[] GetData(string filePath, string schemePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"CSV File not found on path: {filePath}");
                }

                if (!File.Exists(schemePath))
                {
                    throw new FileNotFoundException($"JSON Scheme not found on path: {schemePath}");
                }

                var lines = File.ReadAllLines(filePath);
                var scheme = JSON.GetScheme(schemePath);

                if (ValidatorCSV.IsValidToScheme(lines, scheme))
                {
                    string[] result = new string[lines.Length - 1];
                    Array.Copy(lines, 1, result, 0, lines.Length - 1);
                    return result;
                }
            }
            catch (FormatException exception)
            {
                DisplayException(exception.Message, "Check your data for correctness and try again");
            }
            catch (FileNotFoundException exception)
            {
                DisplayException(exception.Message, "Add required files and try again");
            }
            return new string[] { };
        }

        private static void DisplayException(string exceptionText, string solutionText)
        {
            Console.WriteLine(exceptionText);
            Console.WriteLine($"The program will finished. {solutionText}.");
            Console.WriteLine("Press any button to exit.");
            Console.ReadKey();
        }
    }
}