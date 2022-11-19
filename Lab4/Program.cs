using System.Text;
using System.IO;
using System;
using System.Collections.Generic;

namespace Lab4
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Enter database name");
            string databaseName = Console.ReadLine();
            string projectPath = Environment.CurrentDirectory;
            string dataPath = $"{projectPath}\\Data\\{databaseName}_File";
            string schemePath = $"{projectPath}\\Schemes\\{databaseName}_Scheme";

            (List<string[]>, List<Scheme>) filesSchemes = ReadFiles(dataPath, schemePath);
            List<Table> tables = CreateDatabase(filesSchemes);

            Console.WriteLine("Press any button to exit.");
            Console.ReadKey();
        }

        private static (List<string[]>, List<Scheme>) ReadFiles(string dataPath, string schemePath)
        {
            List<string[]> files = new List<string[]>();
            List<Scheme> schemes = new List<Scheme>();
            int number = 1;
            while (true)
            {
                string currentFilePath = $"{dataPath}{number}.csv";
                string currentSchemePath = $"{schemePath}{number}.json";
                if (!(File.Exists(currentFilePath) && File.Exists(currentSchemePath)))
                {
                    if (files.Count == 0)
                    {
                        Console.WriteLine("The database with this name was not found");
                    }
                    break;
                }
                files.Add(CSV.GetData(currentFilePath, currentSchemePath));
                schemes.Add(JSON.GetScheme(currentSchemePath));
                number++;
            }
            return (files, schemes);
        }

        private static List<Table> CreateDatabase((List<string[]>, List<Scheme>) filesSchemes)
        {
            List<string[]> files = filesSchemes.Item1;
            List<Scheme> schemes = filesSchemes.Item2;
            List<Table> tables = new List<Table>();
            if (files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    tables.Add(new Table(files[i], schemes[i]));
                }
                Console.WriteLine("The database has been successfully created");
            }
            return tables;
        }
    }
}