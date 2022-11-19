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

            // Пути к файлам
            string projectPath = Environment.CurrentDirectory;
            Console.WriteLine("Enter database name");
            string databaseName = Console.ReadLine();
            string dataPath = $"{projectPath}\\Data\\{databaseName}_File";
            string schemePath = $"{projectPath}\\Schemes\\{databaseName}_Scheme";

            // Чтение файлов таблиц
            List<string[]> Files = new List<string[]>();
            List<Scheme> Schemes = new List<Scheme>();
            int number = 1;
            while (true)
            {
                string currentFilePath = $"{dataPath}{number}.csv";
                string currentSchemePath = $"{schemePath}{number}.json";
                if (!(File.Exists(currentFilePath) && File.Exists(currentSchemePath)))
                {
                    if (Files.Count == 0)
                    {
                        Console.WriteLine("The database with this name was not found");
                    }
                    break;
                }
                Files.Add(CSV.GetData(currentFilePath, currentSchemePath));
                Schemes.Add(JSON.GetScheme(currentSchemePath));
                number++;
            }

            List<Table> Tables = new List<Table>();
            if (Files.Count > 0)
            {
                for (int i = 0; i < Files.Count; i++)
                {
                    Tables.Add(new Table(Files[i], Schemes[i]));
                }
                Console.WriteLine("The database has been successfully created");
            }

            Console.WriteLine("Press any button to exit.");
            Console.ReadKey();
        }
    }
}