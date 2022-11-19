using System;
using System.Collections.Generic;

namespace Lab4
{
    class Table
    {
        public string? Name { get; }
        public Dictionary<Column, object[]> Rows { get; set; } = new Dictionary<Column, object[]>();

        public Table(string[] file, Scheme scheme)
        {
            Name = scheme.Name;

            string[][] fileElements = GetFileElements(file);
            for (int i = 0; i < scheme.Elements.Count; i++) 
            {
                Column column = new Column(scheme.Elements[i].Name, scheme.Elements[i].Type);
                object[] columnElements = GetColumnElements(fileElements, column.DataType, i);

                Rows.Add(column, columnElements);
            }
        }

        private object[] GetColumnElements(string[][] fileElements, string dataType, int number)
        {
            if (fileElements.Length == 0) 
                return new object[0];

            object[] columnElements = new object[fileElements[0].Length];
            for (int j = 0; j < fileElements[0].Length; j++)
            {
                string element = fileElements[j][number];
                switch (dataType)
                {
                    case "int":
                        columnElements[j] = int.Parse(element);
                        break;
                    case "uint":
                        columnElements[j] = uint.Parse(element);
                        break;
                    case "dateTime":
                        columnElements[j] = DateTime.Parse(element);
                        break;
                    case "float":
                        columnElements[j] = float.Parse(element);
                        break;
                    default:
                        columnElements[j] = element;
                        break;
                }
            }

            return columnElements;
        }

        private string[][] GetFileElements(string[] file)
        {
            string[][] elements = new string[file.Length][];
            for (int i = 0; i < file.Length; i++)
                elements[i] = file[i].Split(';');
            return elements;
        }
    }
    
    class Column
    {
        public string? Name { get; }
        public string DataType { get; }

        public Column(string? name, string dataType)
        {
            Name = name;
            DataType = dataType;
        }
    }
}