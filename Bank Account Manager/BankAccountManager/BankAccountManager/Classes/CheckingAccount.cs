using System.Collections.Generic;

namespace BankAccountManager.Classes
{
    //Concrete Class inheriting from the Account class, used as a Entity/Table in the database

    class CheckingAccount : Account
    {
        public float MonthlyTransferAmount { get; set; }

        public virtual ICollection<CheckingAccount> CheckingAccounts { get; set; }
    }
}
