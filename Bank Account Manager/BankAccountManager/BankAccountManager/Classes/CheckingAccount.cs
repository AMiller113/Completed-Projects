using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManager.Classes
{
    class CheckingAccount : Account
    {
        public int MonthlyTransferAmount { get; set; }

        public virtual ICollection<CheckingAccount> CheckingAccounts { get; set; }
    }
}
