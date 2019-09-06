using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HotelManager
{
    [Serializable]
    class HotelDatabase
    {
        private static RoomData[] rooms = new RoomData[8];
        private List<Reservation> reservations;
        private const string file_path = "Hotel Data.bin";
        private const string log_path = "Reservation Log.txt";
        private static HotelDatabase database;

        private HotelDatabase()
        {
            rooms[0] = new RoomData(RoomType.Single, 50);
            rooms[1] = new RoomData(RoomType.Twin, 50);
            rooms[2] = new RoomData(RoomType.Studio, 30);
            rooms[3] = new RoomData(RoomType.Joint, 20);
            rooms[4] = new RoomData(RoomType.Deluxe, 15);
            rooms[5] = new RoomData(RoomType.Suite, 8);
            rooms[6] = new RoomData(RoomType.Penthouse, 3);
            rooms[7] = new RoomData(RoomType.Presidential, 1);

            reservations = new List<Reservation>();
        }

        public static HotelDatabase GetDatabase()
        {
            if (database != null)
            {
                return database;
            }

            if (File.Exists(file_path) && database == null)
            {
                database = LoadDB();
                return database;
            }
            else
                return new HotelDatabase();
        }

        public void AddReservation(Reservation reservation)
        {
            if (reservation!=null)
            {
                reservations.Add(reservation);
                SaveDB();
            }
        }

        public static void SaveDB()
        {
            try
            {
                using (FileStream stream = File.OpenWrite(file_path))
                {
                    BinaryFormatter binary = new BinaryFormatter();
                    binary.Serialize(stream, binary);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The Database has failed to save, data may become corrupted");
                Console.WriteLine(e.StackTrace);

            }

        }
        public static HotelDatabase LoadDB()
        {
            HotelDatabase data = null;
            try
            {
                using (FileStream file = new FileStream(file_path, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    data = (HotelDatabase)binaryFormatter.Deserialize(file);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The Database has failed to load, data may be corrupted, loading database defaults");
                Console.WriteLine(e.StackTrace);
            }

            if (data == null)
            {
                return new HotelDatabase();
            }
            else
                return data;

        }


        public enum RoomType { Single = 60, Twin = 90, Studio = 140, Joint = 200, Deluxe = 300, Suite = 600, Penthouse = 800, Presidential = 1500 };

        [Serializable]
        private struct RoomData
        {
            RoomType room;
            public int Number_Of_Rooms { get; set; }

            public RoomData(RoomType roomType, int numOfRooms)
            {
                room = roomType;
                Number_Of_Rooms = numOfRooms;
            }

            public RoomType getRoom()
            {
                return room;
            }
        }

        public static RoomType ParseRoomType(int roomType)
        {
            if (roomType == 0 || roomType < 0 || roomType > 8)
            {
                throw new IllegalDataException();
            }
            if (rooms[roomType].Number_Of_Rooms > 0)
            {
                rooms[roomType].Number_Of_Rooms--;
                return rooms[roomType].getRoom();
            }
            else
            {
                Console.WriteLine("There are no more rooms of that type available, please choose another upon restart.");
                throw new IllegalDataException();
            }
        }

        public class IllegalDataException : Exception {}
    }
}
