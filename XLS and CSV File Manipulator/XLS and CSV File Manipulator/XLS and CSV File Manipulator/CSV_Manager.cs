using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace XLS_and_CSV_File_Manipulator
{
    class CSV_Manager
    {
        private string[,] data;
        private string[] column_names;
        private string path;
        private int rows;
        private int columns;

        private CSV_Manager(string path)
        {
            this.ParseCSV(ref path);
        }

        public void ParseCSV(ref string path)
        {
            this.path = path;
            TextFieldParser text = new TextFieldParser(path);
            text.SetDelimiters(",");

            column_names = text.ReadFields();
            this.columns = column_names.Length;

            List<string[]> dataHold = new List<string[]>();
            while (!text.EndOfData)
            {    
                dataHold.Add(text.ReadFields());
            }

            this.rows = dataHold.Count;
            this.data = new string[rows,columns];
            
            int row = 0;
            foreach (var fields in dataHold)
            {
                for (int col = 0; col < fields.Length; col++)
                {
                    data[row, col] = fields[col];
                }
                row++;
            }   
        }

        public string Path { get => path; set => path = value; }
        public int Rows { get => rows; set => rows = value; }
        public int Columns { get => columns; set => columns = value; }
        public string[] Column_names { get => column_names; set => column_names = value; }
        public string[,] Data { get => data; set => data = value; }
    }
}
