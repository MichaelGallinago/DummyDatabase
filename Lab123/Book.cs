namespace Lab123
{
    class Book
    {
        public int ID { get; private set; }
        public string Author { get; private set; }
        public string Name { get; private set; }
        public int Year { get; private set; }
        public int BooksheflID { get; private set; }
        public int ShelfID { get; private set; }
        public bool Availability { get; set; }

        public Book(int id, string author, string name, int year, int booksheflID, int shelfID, bool available)
        {
            ID = id;
            Author = author;
            Name = name;
            Year = year;
            BooksheflID = booksheflID;
            ShelfID = shelfID;
            Availability = available;
        }
    }
}
