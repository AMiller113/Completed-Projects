using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using static MedicalLog.Class.Log;

namespace MedicalLog.Class
{
    [Serializable]
    class Logbook
    {
        IList<Log> logs;
        const string path = "Logbook.bin";

        public void DisplayLogbook()
        {
            int index = logs.Count - 1;

            foreach (var log in logs)
            {
                Console.Write("Log Number: "+ index + "\r\n");
                log.DisplayLog();

                if (index != 0)
                {
                    index--;
                }
                else
                    break;
            }
        }

        public bool WriteEntry()
        {
            Console.WriteLine("Patients First Name: ");
            string fName = Console.ReadLine();
            if (!Log.CheckName(fName))
            {
                return false;
            }

            Console.WriteLine("Patients Last Name: ");
            string lName = Console.ReadLine();
            if (!Log.CheckName(lName))
            {
                return false;
            }

            Console.WriteLine("Procedure Performed: ");
            string procedure = Console.ReadLine();
            if (procedure.Equals(""))
            {
                return false;
            }
            TypeOfService service = Log.ParseService(procedure);

            Console.WriteLine("Appointment details (Type 'END' on its own line and hit enter when finished): ");
            string line = "";    
            StringBuilder stringBuilder = new StringBuilder();

            while (true)
            {
                line = Console.ReadLine();
                if (line.Equals("END"))
                {
                    break;
                }
                stringBuilder.Append(line);
            }

            logs.Add(new Log(fName, lName,service, stringBuilder.ToString()));
            this.Save();
            Console.WriteLine("Entry saved successfully!");
            return true;
        }

        public bool RemoveEntry(int log_index)
        {
            if ((log_index) >= logs.Count || log_index < 0)
                return false;
            else
            {
                logs.RemoveAt(log_index);
                this.Save();
                return true;
            }
        }
        public bool Save()
        {
            try { 
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, this);
                stream.Close();
            return true;
            }
            catch (IOException)
            {
                Console.WriteLine("There has been an error saving your logbook.");
                return false;
            }
        }
        public static Logbook Load()
        {
            Logbook logbook = new Logbook(); ;
            
            if (File.Exists(path))
            {
                try
                {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                    logbook = (Logbook)formatter.Deserialize(stream);
                    stream.Close();
                }
                catch(IOException e)
                {
                    Console.WriteLine("An IO exception has occured, file may be corrupted");
                    Console.WriteLine(e.Data);
                }
            }
           
            return logbook;
        }

        private Logbook()
        {
            this.logs = new List<Log>();
        }
    }
}
