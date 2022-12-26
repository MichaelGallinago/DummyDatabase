using System.Text;
using System.IO;
using System;
using System.Collections.Generic;
using Lab4.DatabaseClasses;
using Lab4.SchemeClasses;

namespace Lab4
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            GetPaths(out string dataPath, out string schemePath);
            ReadFiles(dataPath, schemePath, out List<string[]> files, out List<Scheme> schemes);
            WriteDatabase(CreateDatabase(files, schemes));
        }

        private static void GetPaths(out string dataPath, out string schemePath)
        {
            Console.WriteLine("Enter database name");
            string databaseName = Console.ReadLine();
            string projectPath = Environment.CurrentDirectory;
            dataPath = $"{projectPath}\\Data\\{databaseName}_File";
            schemePath = $"{projectPath}\\Schemes\\{databaseName}_Scheme";
        }

        private static void WriteDatabase(List<Table> tables)
        {
            Console.WriteLine();
            foreach (Table table in tables)
            {
                foreach (var row in table.Rows)
                {
                    Console.WriteLine(String.Join(";", row.Value));
                }
                Console.WriteLine();
            }
            Console.WriteLine("Press any button to exit.");
            Console.ReadKey();
        }
        
        private static void ReadFiles(string dataPath, string schemePath, out List<string[]> files, out List<Scheme> schemes)
        {
            files = new List<string[]>();
            schemes = new List<Scheme>();

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
        }

        private static List<Table> CreateDatabase(List<string[]> files, List<Scheme> schemes)
        {
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