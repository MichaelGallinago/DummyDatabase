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
            string dataPath = $"{projectPath}//Data//{databaseName}_File";
            string schemePath = $"{projectPath}//Schemes//{databaseName}_Scheme";

            // Чтение файлов таблиц
            List<string[]> Files = new List<string[]>();
            int number = 1;
            while (true)
            {
                if (!(File.Exists(dataPath + number) && File.Exists(schemePath + number)))
                {
                    if (Files.Count == 0)
                    {
                        Console.WriteLine("The database with this name was not found");
                    }
                    break;
                }
                Files.Add(CVS.GetData(dataPath + number, schemePath + number));
                number++;
            }

            Console.WriteLine("Press any button to exit.");
            Console.ReadKey();
        }
    }
}