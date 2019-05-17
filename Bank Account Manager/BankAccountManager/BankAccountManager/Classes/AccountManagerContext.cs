using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManager.Classes
{
    class AccountManagerContext : DbContext
    {
        public AccountManagerContext() : base("Server = (LocalDB)\\MSSQLLocalDB;Database=BankAccountManager;Integrated Security = True;") {}
        public virtual DbSet<CheckingAccount> CheckingAccounts { get; set; }
        public virtual DbSet<SavingsAccount> SavingsAccounts { get; set; }
        public virtual DbSet<BusinessAccount> BusinessAccounts { get; set; }
    }
}
