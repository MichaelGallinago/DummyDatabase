using System.Text;

namespace Lab4
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            // Пути к файлам
            string projectPath = Environment.CurrentDirectory;

            string dataPathUsers = projectPath + "//Data//Users.csv";
            string schemePathUser = projectPath + "//Schemes//schemeUser.json";
            string dataPathBooks = projectPath + "//Data//Books.csv";
            string schemePathBook = projectPath + "//Schemes//schemeBook.json";
            string dataPathUserBook = projectPath + "//Data//UserBook.csv";
            string schemePathUserBook = projectPath + "//Schemes//schemeUserBook.json";
        }
    }
}