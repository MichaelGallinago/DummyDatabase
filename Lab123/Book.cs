namespace Lab123
{
    class Book
    {
        public string Author { get; private set; }
        public string Name { get; private set; }
        public int Year { get; private set; }
        public int BooksheflID { get; private set; }
        public int ShelfID { get; private set; }
        public bool Availability { get; set; }
        public (uint, DateTime) UserBook { get; set; }

        public Book(string author, string name, int year, int booksheflID, int shelfID)
        {
            Author = author;
            Name = name;
            Year = year;
            BooksheflID = booksheflID;
            ShelfID = shelfID;
            Availability = true;
        }
    }
}
