namespace Lab123
{
    class User
    {
        public string FullName { get; private set; }

        public User(string fullName)
        {
            FullName = fullName;
        }
    }
}
