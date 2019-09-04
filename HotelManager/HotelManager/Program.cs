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
                
            }
            catch (Exception)
            {
               
                throw;
            }
               
            
            
        }

        private static RoomType GetRoomType()
        {
            Console.WriteLine("Room type's available \n" +
                "===========================================\n" +
                "1) Single\n" +
                "2) Twin\n" +
                "3) Studio\n" +
                "4) Joint\n" +
                "5) Deluxe\n" +
                "6) Suite\n" +
                "7) Penthouse\n" +
                "8) Presidential\n");

            int.TryParse(Console.ReadLine(), out int roomType);

            return HotelDatabase.ParseRoomType(roomType);
        }

        public static string GetName()
        {
            Console.WriteLine("Please enter your full name.");
            return Console.ReadLine();
        }
       

        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine("If your in the market for a great hotel that will suit all your travel needs, look no further.\n" +
                "Simply choose the type of room your looking for and we can start.! (Pick a number from the list)\n ");
        }

        
    }
}
