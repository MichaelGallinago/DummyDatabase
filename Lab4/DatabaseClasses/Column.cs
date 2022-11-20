namespace Lab4.DatabaseClasses
{
    class Column
    {
        public string? Name { get; }
        public string? DataType { get; }

        public Column(string? name, string? dataType)
        {
            Name = name;
            DataType = dataType;
        }
    }
}