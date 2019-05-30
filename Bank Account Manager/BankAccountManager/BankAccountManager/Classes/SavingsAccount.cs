using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManager.Classes
{
    //Concrete Class inheriting from the Account class, used as a Entity/Table in the database

    class SavingsAccount : Account
    {
        public int WithdrawalsThisMonth { get; set; }

        public virtual ICollection<SavingsAccount> SavingsAccounts{ get; set; }
    }
}
