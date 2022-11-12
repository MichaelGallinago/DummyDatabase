﻿using System.Text;

namespace Lab123
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

            // Чтение файлов таблиц
            string[] dataUsers = CVS.GetData(dataPathUsers, schemePathUser);
            string[] dataBooks = CVS.GetData(dataPathBooks, schemePathBook);
            string[] dataUserBook = CVS.GetData(dataPathUserBook, schemePathUserBook);

            Dictionary<uint, User> users = new Dictionary<uint, User>();
            Dictionary<uint, Book> books = new Dictionary<uint, Book>();
            Dictionary<uint, (uint, DateTime)> userBook = new Dictionary<uint, (uint, DateTime)>();

            Dictionary<string, uint> maxDataLength = new Dictionary<string, uint>()
            {
                {"autor", 0}, {"book", 0}, {"user", 0}, {"date", 0}
            };

            // Увеличиваем максимальную ширину ячейки в таблице, если найдено более длинное слово
            void updateLength(string data, string key) => maxDataLength[key] = Math.Max((uint)data.Length, maxDataLength[key]);

            // Заносим  в словарь пользователей
            foreach (string row in dataUsers)
            {
                string[] cells = row.Split(";");
                users.Add(uint.Parse(cells[0]), new User(cells[1]));
            }

            // Заносим в словарь книги, которые ещё не были возвращены, но были взяты
            foreach (string row in dataUserBook)
            {
                string[] cells = row.Split(";");
                if (cells[3] != "") continue;

                var userID = uint.Parse(cells[0]);
                var date = DateTime.Parse(cells[2]);

                updateLength(users[userID].FullName, "user");
                updateLength(date.ToString(), "date");

                userBook.Add(uint.Parse(cells[1]), (userID, date));
            }

            // Заносим в словарь книги
            foreach (string row in dataBooks)
            {
                string[] cells = row.Split(";");
                var id = uint.Parse(cells[0]);
                var book = new Book(cells[1], cells[2], int.Parse(cells[3]), int.Parse(cells[4]), int.Parse(cells[5]));

                updateLength(cells[1], "autor");
                updateLength(cells[2], "book");

                if (userBook.ContainsKey(id))
                {
                    book.Availability = false;
                    book.UserBook = userBook[id];
                }

                books.Add(id, book);
            }

            WriteTable(books, users, maxDataLength);

            Console.WriteLine("Press any button to exit.");
            Console.ReadKey();
        }

        private static void WriteTable(Dictionary<uint, Book> books, Dictionary<uint, User> users, Dictionary<string, uint> maxDataLength)
        {
            string GetWPS(string key, string text) => $" {text.PadRight((int)maxDataLength[key])} ";

            var frame = $"|{GetWPS("autor", "")}|{GetWPS("book", "")}|{GetWPS("user", "")}|{GetWPS("date", "")}|".Replace(' ', '-');
            Console.WriteLine($"|{GetWPS("autor", "Автор")}|{GetWPS("book", "Название")}|{GetWPS("user", "Читает")}|{GetWPS("date", "Взял")}|");
            Console.WriteLine(frame);
            foreach (KeyValuePair<uint, Book> keyBook in books)
            {
                var book = keyBook.Value;
                Console.Write($"|{GetWPS("autor", book.Author)}|{GetWPS("book", book.Name)}|");

                string userName = "", takeDate = "";
                if (!book.Availability)
                {
                    userName = users[book.UserBook.Item1].FullName;
                    takeDate = book.UserBook.Item2.ToString();
                }
                Console.WriteLine($"{GetWPS("user", userName)}|{GetWPS("date", takeDate)}|");
            }
            Console.WriteLine(frame);
        }
    }
}