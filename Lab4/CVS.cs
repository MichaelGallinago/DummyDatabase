using System;
using System.IO;

namespace Lab4
{
    class CVS
    {
        public static string[] GetData(string filePath, string schemePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"CVS File not found on path: {filePath}");
                }
                if (!File.Exists(schemePath))
                {
                    throw new FileNotFoundException($"JSON Scheme not found on path: {schemePath}");
                }

                var lines = File.ReadAllLines(filePath);
                var scheme = JSON.GetScheme(schemePath);

                if (JSON.IsValidToScheme(lines, scheme))
                {
                    string[] result = new string[lines.Length - 1];
                    Array.Copy(lines, 1, result, 0, lines.Length - 1);
                    return result;
                }
            }
            catch (FormatException exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("The program will finished. Check your data for correctness and try again.");
                Console.WriteLine("Press any button to exit.");
                Console.ReadKey();
            }
            catch (FileNotFoundException exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("The program will finished. Add required files and try again.");
                Console.WriteLine("Press any button to exit.");
                Console.ReadKey();
            }
            return new string[] { };
        }
    }
}