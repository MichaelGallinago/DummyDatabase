namespace Lab123
{
    class Book
    {
        public string Author { get; }
        public string Name { get; }
        public uint Year { get; }
        public uint BooksheflID { get; }
        public uint ShelfID { get; }
        public bool Availability { get; set; }
        public UserBook? UserBook { get; set; }

        public Book(string author, string name, uint year, uint booksheflID, uint shelfID)
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