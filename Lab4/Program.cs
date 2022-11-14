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
            string schemePath = $"{projectPath}//Schemes//{databaseName}";

            int number = 1;
            List<string[]>
            while (File.Exists(dataPath + number))
            {
                
                number++;
            }
        }
    }
}