using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManager.Classes
{
    abstract class Account
    {
        public int PinNumber { get; set; }
        public string AccountHolder { get; set; }
        public int Balance { get; set; }
        public DateTime OpenDate { get; set; }

    }
}
