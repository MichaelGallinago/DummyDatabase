using System.Text;

namespace Lab123
{
    internal class Program
    {
        static void Main()
        {
            string projectPath = Environment.CurrentDirectory;

            string dataPathUsers = projectPath + "//Data//Users.csv";
            string schemePathUser = projectPath + "//Schemes//schemeUser.json";
            string dataPathBooks = projectPath + "//Data//Books.csv";
            string schemePathBook = projectPath + "//Schemes//schemeBook.json";
            string dataPathUserBook = projectPath + "//Data//UserBook.csv";
            string schemePathUserBook = projectPath + "//Schemes//schemeUserBook.json";

            string[] dataUsers = CVS.GetData(dataPathUsers, schemePathUser);
            string[] dataBooks = CVS.GetData(dataPathBooks, schemePathBook);
            string[] dataUserBook = CVS.GetData(dataPathUserBook, schemePathUserBook);

            if (dataUsers.Length == 0 || dataBooks.Length == 0) return;

            Dictionary<uint, User> users = new Dictionary<uint, User>();
            Dictionary<uint, Book> books = new Dictionary<uint, Book>();
            Dictionary<uint, (uint, DateTime)> userBook = new Dictionary<uint, (uint, DateTime)>();

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
                userBook.Add(uint.Parse(cells[1]), (uint.Parse(cells[0]), DateTime.Parse(cells[2])));
            }

            foreach (string row in dataBooks)
            {
                string[] cells = row.Split(";");
                var id = uint.Parse(cells[0]);
                var book = new Book(cells[1], cells[2], int.Parse(cells[3]), int.Parse(cells[4]), int.Parse(cells[5]));

                if (userBook.ContainsKey(id))
                {
                    book.Availability = false;
                    book.UserBook = userBook[id];
                }

                books.Add(id, book);
            }

            WriteTable(books, users);

            Console.WriteLine("Press any button to exit.");
            Console.ReadKey();
        }

        private static void WriteTable(Dictionary<uint, Book> books, Dictionary<uint, User> users)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("| Автор | Название | Читает | Взял |");
            foreach (KeyValuePair<uint, Book> keyBook in books)
            {
                var book = keyBook.Value;
                Console.Write($"| {book.Author} | {book.Name} |");
                if (book.Availability)
                {
                    Console.WriteLine(" | |");
                }
                else
                {
                    var userData = book.UserBook;
                    Console.WriteLine($" {users[userData.Item1].FullName} | {userData.Item2} |");
                }
            }
        }
    }
}