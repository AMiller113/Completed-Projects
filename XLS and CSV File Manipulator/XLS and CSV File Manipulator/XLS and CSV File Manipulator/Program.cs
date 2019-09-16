using System;

namespace XLS_and_CSV_File_Manipulator
{
    class Program
    {
        static void Main(string[] args)
        {

            //CSV_Manager csv = new CSV_Manager(@"c: \users\a.miller\downloads\beginner's guide to python data analysis & visualization\beginner's guide to python data analysis & visualization\[tutsgalaxy.com] - beginner's guide to python data analysis & visualization\data\aapl.csv");           
            //csv.PrintCSV();

            string[] test = { "7", "25.6", "3.4", "4.8", "18.4" };
            string[] test3 = { "2016-09-30", "2016-09-29", "2016-09-28", "2015-09-27", "2016-09-26" };
            DateTime[] dateTime = new DateTime[test3.Length];

            for (int i = 0; i < dateTime.Length; i++)
            {
                dateTime[i] = DateTime.Parse(test3[i]);               
            }

            Console.WriteLine(dateTime[0].TimeOfDay);
            float[] test2 = new float[test.Length];
            Array.Sort(dateTime);

            for (int i = 0; i < test.Length; i++)
            {
                test2[i] = float.Parse(test[i]);
            }
            Array.Sort(test2);
            foreach (var item in dateTime)
            {
                Console.Write(item.Date + " ");
            }
            Console.Read();
        }
    }
}
