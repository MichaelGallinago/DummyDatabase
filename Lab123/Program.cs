namespace Lab123
{
    internal class Program
    {
        static void Main()
        {
            var book1 = new Book(10, "Отец демократии", "Демократия1", 1000, 4, 1, true);
            var book2 = new Book(12, "Отец демократии", "Демократия2", 1004, 4, 4, true);
            var book3 = new Book(13, "Отец демократии", "Демократия3", 1008, 4, 3, true);
            var book4 = new Book(16, "Отец демократии", "Демократия4", 1012, 4, 3, false);

            var user = new User(4, "Сын демократии");

            user.UserBooks = new Dictionary<Book, DateTime[]>
            {
                {book1, new DateTime[] { DateTime.Parse("12.06.2002"), DateTime.Parse("23.06.2002") }},
                {book2, new DateTime[] { DateTime.Parse("17.06.2002") }}
            };

            Console.WriteLine("Глянем-ка, что там у читателя под именем " + user.FullName);
            foreach (var book in user.UserBooks)
            {
                Console.WriteLine(book.Key.Name);
                Console.WriteLine("Книга взята " + book.Value[0]);
                if (book.Value.Length > 1)
                {
                    Console.WriteLine("Книга возвращена " + book.Value[0]);
                }
                else
                {
                    Console.WriteLine("Но не возвращена");
                    book.Key.Availability = false;
                }    
            }

            Console.WriteLine("Доступность книг");
            Console.WriteLine($"{book1.Name} : {book1.Availability}" );
            Console.WriteLine($"{book2.Name} : {book2.Availability}");
            Console.WriteLine($"{book3.Name} : {book3.Availability}");
            Console.WriteLine($"{book4.Name} : {book4.Availability}");

            Console.ReadKey();
        }
    }
}