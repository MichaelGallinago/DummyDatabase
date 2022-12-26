using System;
using System.Text;
using System.Collections.Generic;

namespace Lab123
{
    internal class Program
    {
        private static Dictionary<string, uint> maxDataLength = new Dictionary<string, uint>()
        {
            {"autor", 0}, {"book", 0}, {"user", 0}, {"date", 0}
        };

        private enum Path
        {
            dataUsers, schemeUsers, dataBooks, schemeBooks, dataUserBook, schemeUserBook 
        }

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            string[] paths = GetPaths(Environment.CurrentDirectory);
            string[] dataUsers = CSV.GetData(paths[(int)Path.dataUsers], paths[(int)Path.schemeUsers]);
            string[] dataBooks = CSV.GetData(paths[(int)Path.dataBooks], paths[(int)Path.schemeBooks]);
            string[] dataUserBook = CSV.GetData(paths[(int)Path.dataUserBook], paths[(int)Path.schemeUserBook]);

            Dictionary<uint, User> users = FillUsersDictionary(dataUsers);
            Dictionary<uint, UserBook> userBook = FillUserBookDictionary(dataUserBook, users);
            Dictionary<uint, Book> books = FillBooksDictionary(dataBooks, userBook);

            WriteTable(books, users, maxDataLength);
        }

        private static void UpdateLength(string data, string key)
        {
            maxDataLength[key] = Math.Max((uint)data.Length, maxDataLength[key]);
        }

        private static string[] GetPaths(string directory)
        {
            string dataPathUsers = directory + "//Data//Users.csv";
            string schemePathUser = directory + "//Schemes//schemeUser.json";
            string dataPathBooks = directory + "//Data//Books.csv";
            string schemePathBooks = directory + "//Schemes//schemeBook.json";
            string dataPathUserBook = directory + "//Data//UserBook.csv";
            string schemePathUserBook = directory + "//Schemes//schemeUserBook.json";
            return new [] { dataPathUsers, schemePathUser, dataPathBooks, schemePathBooks, dataPathUserBook, schemePathUserBook };
        }

        private static Dictionary<uint, User> FillUsersDictionary(string[] dataUsers)
        {
            Dictionary<uint, User> users = new Dictionary<uint, User>();
            foreach (string row in dataUsers)
            {
                string[] cells = row.Split(";");
                users.Add(uint.Parse(cells[0]), new User(cells[1]));
            }
            return users;
        }

        private static Dictionary<uint, UserBook> FillUserBookDictionary(string[] dataUserBook, Dictionary<uint, User> users)
        {
            Dictionary<uint, UserBook> userBook = new Dictionary<uint, UserBook>();
            foreach (string row in dataUserBook)
            {
                string[] cells = row.Split(";");
                if (cells[3] != "") continue;

                uint userID = uint.Parse(cells[0]);
                uint bookID = uint.Parse(cells[1]);
                DateTime takeDate = DateTime.Parse(cells[2]);
                DateTime returnDate = new DateTime();

                UpdateLength(users[userID].FullName, "user");
                UpdateLength(takeDate.ToString(), "date");

                userBook.Add(bookID, new UserBook(userID, takeDate, returnDate));
            }
            return userBook;
        }

        private static Dictionary<uint, Book> FillBooksDictionary(string[] dataBooks, Dictionary<uint, UserBook> userBook)
        {
            Dictionary<uint, Book> books = new Dictionary<uint, Book>();
            foreach (string row in dataBooks)
            {
                string[] cells = row.Split(";");
                uint id = uint.Parse(cells[0]);
                Book book = new Book(cells[1], cells[2], uint.Parse(cells[3]), uint.Parse(cells[4]), uint.Parse(cells[5]));

                UpdateLength(cells[1], "autor");
                UpdateLength(cells[2], "book");

                if (userBook.ContainsKey(id))
                {
                    book.Availability = false;
                    book.UserBook = userBook[id];
                }

                books.Add(id, book);
            }
            return books;
        }

        private static void WriteTable(Dictionary<uint, Book> books, Dictionary<uint, User> users, Dictionary<string, uint> maxDataLength)
        {
            string GetWPS(string key, string text) => $" {text.PadRight((int)maxDataLength[key])} ";

            string frame = $"|{GetWPS("autor", "")}|{GetWPS("book", "")}|{GetWPS("user", "")}|{GetWPS("date", "")}|".Replace(' ', '-');
            Console.WriteLine($"|{GetWPS("autor", "Автор")}|{GetWPS("book", "Название")}|{GetWPS("user", "Читает")}|{GetWPS("date", "Взял")}|");
            Console.WriteLine(frame);
            foreach (KeyValuePair<uint, Book> keyBook in books)
            {
                Book book = keyBook.Value;
                Console.Write($"|{GetWPS("autor", book.Author)}|{GetWPS("book", book.Name)}|");

                string userName = "", takeDate = "";
                if (!(book.Availability || book.UserBook == null))
                {
                    userName = users[book.UserBook.UserID].FullName;
                    takeDate = book.UserBook.TakeDate.ToString();
                }
                Console.WriteLine($"{GetWPS("user", userName)}|{GetWPS("date", takeDate)}|");
            }
            Console.WriteLine(frame);

            Console.WriteLine("Press any button to exit.");
            Console.ReadKey();
        }
    }
}