using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccountManager.Classes
{
    class BusinessAccount : Account
    {
        public string business_name { get; set; }

        public virtual ICollection<BusinessAccount> BusinessAccounts { get; set; }
    }
}
