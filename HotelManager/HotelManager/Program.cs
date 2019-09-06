using System;
using static HotelManager.HotelDatabase;

namespace HotelManager
{
    class Program
    {
        static void Main(string[] args)
        {
            HotelDatabase hotelDatabase = HotelDatabase.GetDatabase();
            DisplayWelcomeMessage();
            try
            {
                RoomType room = GetRoomType();
                string name = GetName();
                DateTime date = GetDate();
                int daysStaying = GetDays();
                ConfirmSubmission(hotelDatabase, room, name, date, daysStaying);
            }
            catch (Exception)
            {
                Console.WriteLine("An error has occurred. Please restart."); 
            }

            Console.Read();
        }

        private static void ConfirmSubmission(HotelDatabase hotelDatabase, RoomType room, string name, DateTime date, int daysStaying)
        {
            Console.WriteLine("Please review your information.");
            Console.Write
                (
                "Name: " + name + "\r\n" +
                "Room Type: " + room.ToString() + "\r\n" +
                "Date of Arrival: " + date.ToString() + "\r\n" +
                "Days Staying: " + daysStaying + "\r\n" +
                "Total Cost: " + (int)room * daysStaying + "\r\n"
                );
            Console.WriteLine("Is this what you want (y/n)?");
            string response = Console.ReadLine();
            if (response.ToLower().Equals("y"))
            {
                hotelDatabase.AddReservation(new Reservation(name, room, date, daysStaying));
                Console.WriteLine("Thank you for choosing to stay with us, I hope you enjoy your stay!");
            }
            else if (response.ToLower().Equals("n"))
            {
                Console.WriteLine("Well, maybe next time then. Take care!");
            }
            else
            {
                Console.WriteLine("Well, maybe next time then. Take care!");
            }
        }

        private static int GetDays()
        {
            Console.WriteLine("Please tell us how many days you will be staying.");
            string days = Console.ReadLine();
            if (int.TryParse(days, out int numDays))
            {
                return numDays;
            }
            else
            {
                throw new IllegalDataException();
            }
        }

        private static DateTime GetDate()
        {
            Console.WriteLine("Enter the date of your arrival (yyyy-dd-mm)");
            string date = Console.ReadLine();
            if (date.Equals("") || date == null)
            {
                throw new IllegalDataException();
            }
           
            bool result = DateTime.TryParse(date + " 12:00:00AM", out DateTime arrivalDate);
            if (result)
            {
                if (arrivalDate >= DateTime.Now)
                {
                    return arrivalDate;
                }
                else
                {
                    Console.WriteLine("No time travelers are allowed at our hotel. Please try again.");
                    throw new IllegalDataException();
                }
                
            }
            else
            {
                throw new IllegalDataException();
            }
        }

        private static RoomType GetRoomType()
        {
            Console.WriteLine("Room type's available \n" +
                "===========================================\n" +
                "1) Single: $"+(int)RoomType.Single+ "\n" +
                "2) Twin: $" + (int)RoomType.Twin + "\n" +
                "3) Studio: $" + (int)RoomType.Studio + "\n" +
                "4) Joint: $" + (int)RoomType.Joint + "\n" +
                "5) Deluxe: $" + (int)RoomType.Deluxe + "\n" +
                "6) Suite: $" + (int)RoomType.Suite + "\n" +
                "7) Penthouse: $" + (int)RoomType.Penthouse + "\n" +
                "8) Presidential: $" + (int)RoomType.Presidential + "\n");

            bool result = int.TryParse(Console.ReadLine(), out int roomType);

            if (!result)
            {
                throw new IllegalDataException();
            }

            return HotelDatabase.ParseRoomType(roomType);
        }

        public static string GetName()
        {
            Console.WriteLine("Please enter your full name.");
            string name = Console.ReadLine();
            if (ValidateName(name))
            {
                return name;
            }
            else
            {
                throw new IllegalDataException();
            }
            
        }

        private static bool ValidateName(string name)
        {
            if (name.Equals("") || name == null)
            {
                Console.WriteLine("Please enter a valid name.");
                return false;
            }
            foreach (char item in name)
            {
                if (char.IsDigit(item))
                {
                    Console.WriteLine("Please enter a valid name (Name has a number in it).");
                    return false;
                }
            }

            return true;
        }

        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("If your in the market for a great hotel that will suit all your travel needs, look no further.\n" +
                "Simply choose the type of room your looking for and we can start! (Pick a number from the list)\n ");
        }

        
    }
}
