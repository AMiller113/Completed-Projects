using System;

namespace XLS_and_CSV_File_Manipulator
{
    class Program
    {
        static void Main(string[] args)
        {
            
            CSV_Manager csv = new CSV_Manager(@"C: \Users\A.Miller\Downloads\Beginner's Guide to Python Data Analysis & Visualization\Beginner's Guide to Python Data Analysis & Visualization\[Tutsgalaxy.com] - Beginner's Guide to Python Data Analysis & Visualization\Data\AAPL.csv");           
            csv.PrintCSV();                      
            Console.Read();
        }
    }
}
