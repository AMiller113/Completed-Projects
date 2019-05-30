using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccountManager.Classes
{
    /*
     * 
     * Abstract Account class used to hold common traits of all the concrete account entities
     * 
     */

    public abstract class Account
    {    

        /*
         * 
         * Composite Key of the Pin,Account Holder, and Bank Name.
         * The idea is to not allow one person to have two accounts of the same type at the same bank.
         * 
         */

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
