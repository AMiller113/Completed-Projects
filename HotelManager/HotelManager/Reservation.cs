using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HotelManager.HotelDatabase;

namespace HotelManager
{
    [Serializable]
    class Reservation
    {
        public string ReservationHolder { get; set; }
        public int ReservationID { get; set; }
        public RoomType RoomReserved { get; set; }
        public DateTime ArrivalDay { get; set; }
        public int DaysStaying { get; set; }
        public int Cost { get; set; }

        public Reservation(string reservationHolder, RoomType roomReserved, DateTime arrivalDay, int daysStaying)
        {
            ReservationHolder = reservationHolder ?? throw new ArgumentNullException(nameof(reservationHolder));           
            RoomReserved = roomReserved;           
            ArrivalDay = arrivalDay;
            DaysStaying = daysStaying;
            Cost = (int) this.RoomReserved * this.DaysStaying;
            ReservationID = (reservationHolder.GetHashCode() * Cost) % 9999;
        }
    }
}
