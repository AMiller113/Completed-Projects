using System.Collections.Generic;

namespace BankAccountManager.Classes
{
    //Concrete Class inheriting from the Account class, used as a Entity/Table in the database

    class BusinessAccount : Account
    {
        public string BusinessName { get; set; }

        public virtual ICollection<BusinessAccount> BusinessAccounts { get; set; }
    }
}
