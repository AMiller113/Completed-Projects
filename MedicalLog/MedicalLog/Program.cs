using MedicalLog.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLog
{
    class Program
    {
        static void Main(string[] args)
        {
            Logbook logbook = Logbook.Load();
            Console.WriteLine("Welcome back! Choose a function by picking it's number.");
            bool exit = false;
            while (!exit)
            {
                int.TryParse(Console.ReadLine(), out int choice);
                if (choice == 0)
                {
                    Console.WriteLine("Invalid Option, please enter a valid choice.");
                    continue;
                }

                exit = HandleChoice(logbook, exit, choice);

            }
        }

        private static bool HandleChoice(Logbook logbook, bool exit, int choice)
        {
            switch (choice)
            {
                case 1:
                    logbook.DisplayLogbook();
                    break;
                case 2:
                    logbook.WriteEntry();
                    break;
                case 3:
                    RemoveEntry(logbook);
                    break;
                case 4:
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid Option, please enter a valid choice.");
                    break;
            }

            return exit;
        }

        private static void RemoveEntry(Logbook logbook)
        {
            logbook.DisplayLogbook();
            Console.WriteLine("Select the index of the log you wish to remove.");
            Console.ReadLine();
            int.TryParse(Console.ReadLine(), out int index);
            logbook.RemoveEntry(index);
        }

        public static void ShowMenu()
        {
            Console.Write(
                "1. Display Logs \r\n" +
                "2. Write new entry \r\n" +
                "3. Remove Entry \r\n" +
                "4. Exit" 
                );
        }
    }
}
