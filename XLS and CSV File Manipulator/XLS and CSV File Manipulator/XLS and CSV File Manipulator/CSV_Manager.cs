using System;
using System.Collections.Generic;
using System.Linq;
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

        public CSV_Manager(string path)
        {
            this.ParseCSVAsync(path);
        }

        public async Task ParseCSVAsync(string path)
        {
            this.path = path;
            TextFieldParser text = new TextFieldParser(path);
            text.SetDelimiters(",");
            column_names = text.ReadFields();
            this.columns = column_names.Length;
            Task<List<string[]>> dataHold =  GetColumnRowDataAsync(text);
            this.rows = dataHold.Result.Count;
            this.data = new string[rows, columns];
            PopulateData(dataHold.Result);
        }

        private void PopulateData(List<string[]> dataHold)
        {
            int row = 0;
            foreach (var fields in dataHold)
            {
                row = PopulateAsync(row, fields).Result;
            }
        }

        private async Task<int> PopulateAsync(int row, string[] fields)
        {
            for (int col = 0; col < fields.Length; col++)
            {
               await Task.Run(() => data[row, col] = fields[col]);
            }
            row++;
            return row;
        }

        private async Task<List<string[]>> GetColumnRowDataAsync(TextFieldParser text)
        {
            
            List<string[]> dataHold = new List<string[]>();
            while (!text.EndOfData)
            {
                await Task.Run(() => { dataHold.Add(text.ReadFields());});
            }
            return dataHold;
        }

        public void PrintCSV()
        {
            for (int i = 0; i < this.Column_names.Length; i++)
            {
                var line = (i == (this.Column_names.Length - 1)) ? (this.Column_names[i] + "\r\n") : (this.Column_names[i] + ",\t");
                Console.Write(line);
            }
            Console.WriteLine("============================================================================");
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    var line = (j == (this.Columns - 1)) ? (this.Data[i, j] + "\r\n") : (this.Data[i, j] + ",");
                    Console.Write(line);
                }
            }
        }
        public string Path { get => path; set => path = value; }
        public int Rows { get => rows; set => rows = value; }
        public int Columns { get => columns; set => columns = value; }
        public string[] Column_names { get => column_names; set => column_names = value; }
        public string[,] Data { get => data; set => data = value; }
    }
}
