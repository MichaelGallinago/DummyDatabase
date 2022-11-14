using System;
namespace Lab123
{
    class UserBook
    {
        public uint UserID { get; }
        public DateTime TakeDate { get; }
        public DateTime ReturnDate { get; }

        public UserBook(uint userID, DateTime takeDate, DateTime returnDate)
        {
            UserID = userID;
            TakeDate = takeDate;
            ReturnDate = returnDate;
        }
    }
}
