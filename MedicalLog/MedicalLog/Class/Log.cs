using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLog.Class
{
    [Serializable]
    public class Log : IComparable 
    {
        public enum TypeOfService { Massage, Acupuncture, GeneralTherapy }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public TypeOfService service_given { get; set; }
        public DateTime time_of_appointment { get; }
        public String appointment_details { get; set; }

        public Log(string first_name, string last_name, TypeOfService service_given, string appointment_details)
        {
            this.first_name = first_name ?? throw new ArgumentNullException(nameof(first_name));
            this.last_name = last_name ?? throw new ArgumentNullException(nameof(last_name));
            this.service_given = service_given;
            this.time_of_appointment = DateTime.Now;
            this.appointment_details = appointment_details ?? throw new ArgumentNullException(nameof(appointment_details));
        }

        public void DisplayLog()
        {
            Console.Write
                    (
                    "Patient Name: " + this.first_name + " " + this.last_name + "\r\n" +
                    "Service Performed: " + this.service_given.ToString() + "\r\n" +
                    "Date of Appointment: " + this.time_of_appointment.ToString() + "\r\n"
                    );

            Console.WriteLine();

            Console.Write
                (
                "Appointment Details \r\n" +
                "====================================================== \r\n" +
                this.appointment_details + "\r\n"
                );

            Console.WriteLine("======================================================");
        }

        public int CompareTo(object obj)
        {
            return time_of_appointment.CompareTo(obj);
        }

        public static TypeOfService ParseService(string service)
        {
            if (service.ToLower().Equals("massage"))
            {
                return TypeOfService.Massage;
            }
            else if (service.ToLower().Equals("acupuncture"))
            {
                return TypeOfService.Acupuncture;
            }
            else
                return TypeOfService.GeneralTherapy;
        }

        public static bool CheckName(string str)
        {
            if (str.Any<Char>(char.IsDigit))
            {
                return false;
            }
            else if (str.Any<Char>(Char.IsWhiteSpace))
            {
                return false;
            }
            else
               return true;
        }
    }
}
