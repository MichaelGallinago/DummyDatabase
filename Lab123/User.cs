namespace Lab123
{
    class User
    {
        public int ID { get; private set; }
        public string FullName { get; private set; }
        public Dictionary<Book, DateTime[]> UserBooks { get; set; }

        public User(int id, string fullName)
        {
            ID = id;
            FullName = fullName;
            UserBooks = new Dictionary<Book, DateTime[]>();
        }
    }
}
