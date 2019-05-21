using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

namespace BankAccountManager.Classes
{
   public abstract class Account
    {        
        [Key]
        [Column(Order = 1)]
        public int PinNumber { get; set; }
        [Key]
        [Column(Order = 2)]
        public string AccountHolder { get; set; }
        [Key]
        [Column(Order = 3)]
        public Bank_Name BankName { get; set; }
        public float Balance { get; set; }
        public DateTime AccountOpenDate { get; set; }          
    }
}
