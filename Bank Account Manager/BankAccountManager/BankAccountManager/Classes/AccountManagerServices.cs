using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BankAccountManager.Classes
{
    [Flags]
    public enum Bank_Name : short { Chase = 1, Capitol, BOA, HSBC, TD, Citi, Morgan, Goldman }
    [Flags]
    enum AccountType : short {Checking = 1, Savings, Business};

    
    
     static class AccountManagerServices
    {
        const float max_transfer = 100000f; //Maximum transfer at one time

        public static bool DebitAccount(Account account, float debitAmount)
        {
            if (debitAmount > max_transfer)
            {
                MessageBox.Show("Amount exceeds maximum single transaction limit.");
                return false;
            }

            Mutex mutex = new Mutex();
            mutex.WaitOne();
            //Critical Section - Begin
            bool balanceChanged = false;
            using (var context = new AccountManagerContext())
            {              
                account.Balance -= debitAmount;
                context.Entry(account).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            balanceChanged = true;
            //Critical Section - End          
            mutex.ReleaseMutex();
            return balanceChanged;
        }

        public static bool CreditAccount(Account account, float creditAmount)
        {
            if (creditAmount > max_transfer)
            {
                MessageBox.Show("Amount exceeds maximum single transaction limit.");
                return false;
            }

            Mutex mutex = new Mutex();
            mutex.WaitOne();
            //Critical Section - Begin
            bool balanceChanged = false;
            using (var context = new AccountManagerContext())
            {              
                account.Balance += creditAmount;
                context.Entry(account).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            balanceChanged = true;
            //Critical Section - End
            mutex.ReleaseMutex();
            return balanceChanged;
        }

        public static bool TransferMoney(Account starting, Account destination, float amount)
        {
            if (amount > max_transfer)
            {
                MessageBox.Show("Amount exceeds maximum single transaction limit.");
                return false;
            }

            Mutex mutex = new Mutex();
            mutex.WaitOne();
            //Critical Section - Begin
            bool transferComplete = false;
            using (var context = new AccountManagerContext()) {
                
                destination.Balance += amount;
                starting.Balance -= amount;
               
                if (starting.GetType() == typeof(SavingsAccount))
                {
                    SavingsAccount s = (SavingsAccount)starting;
                    s.WithdrawalsThisMonth++;
                }
                context.Entry(starting).State = System.Data.Entity.EntityState.Modified;
                context.Entry(destination).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                transferComplete = true;
            }
            //Critical Section - End
            mutex.ReleaseMutex();           
            return transferComplete;
        }

       public static bool VerifyPin(int pin)
        {
            if (pin.ToString().Length != 4)
                return false;

            return true;
        }

       public static bool CreateAccount(int pin, float monthlyTransfer, String name, Bank_Name bank)
       {
            Mutex mutex = new Mutex();
            mutex.WaitOne();
            using (var context = new AccountManagerContext())
            {
                if (!VerifyPin(pin))
                {
                    return false;
                }

                if (FindAccount(pin, name, bank, AccountType.Checking) != null)
                {
                    return false;
                }

                CheckingAccount account = new CheckingAccount
                {
                    PinNumber = pin,
                    AccountHolder = name,
                    BankName = bank,
                    Balance = 0,                                      
                    AccountOpenDate = DateTime.Parse(DateTime.Today.ToString()),
                    MonthlyTransferAmount = monthlyTransfer,
                };

                context.CheckingAccounts.Add(account);
                context.SaveChanges();
            }
            mutex.ReleaseMutex();

            return true;
        }

       public static bool CreateAccount(int pin, String name, Bank_Name bank)
        {
            Mutex mutex = new Mutex();
            mutex.WaitOne();
            using (var context = new AccountManagerContext())
            {
                if (!VerifyPin(pin))
                {
                    return false;
                }

                if (FindAccount(pin,name,bank,AccountType.Savings) != null)
                {
                    return false;
                }

                SavingsAccount account = new SavingsAccount
                {
                    PinNumber = pin,
                    AccountHolder = name,
                    BankName = bank,
                    Balance = 0,
                    AccountOpenDate = DateTime.Parse(DateTime.Today.ToString()),
                    WithdrawalsThisMonth = 0
                };

                context.SavingsAccounts.Add(account);
                context.SaveChanges();
            }
            mutex.ReleaseMutex();

            return true;
        }

       public static bool CreateAccount(int pin, String name, String businessName,Bank_Name bank)
        {
            Mutex mutex = new Mutex();
            mutex.WaitOne();
            using (var context = new AccountManagerContext())
            {
                if (!VerifyPin(pin))
                {
                    return false;
                }

                if (FindAccount(pin, name, bank, AccountType.Business) != null)
                {
                    return false;
                }

                BusinessAccount account = new BusinessAccount
                {
                    PinNumber = pin,
                    AccountHolder = name,
                    BankName = bank,
                    BusinessName = businessName,
                    Balance = 0,
                    AccountOpenDate = DateTime.Parse(DateTime.Today.ToString())
                };

                context.BusinessAccounts.Add(account);
                context.SaveChanges();
            }
            mutex.ReleaseMutex();

            return true;
        }

       public static Account FindAccount(int pin, String name, Bank_Name bank, AccountType type)
        {
            using (var context = new AccountManagerContext())
            {
                Account account;
                switch (type)
                {
                    case AccountType.Checking:
                        account = context.CheckingAccounts.Where(s => s.PinNumber == pin && s.AccountHolder == name && s.BankName == bank).FirstOrDefault<CheckingAccount>();
                        break;
                    case AccountType.Savings:
                        account = context.SavingsAccounts.Where(s => s.PinNumber == pin && s.AccountHolder == name && s.BankName == bank).FirstOrDefault<SavingsAccount>();
                        break;
                    case AccountType.Business:
                        account = context.BusinessAccounts.Where(s => s.PinNumber == pin && s.AccountHolder == name && s.BankName == bank).FirstOrDefault<BusinessAccount>();
                        break;
                    default:
                        account = null;
                        break;
                }
                return account;
            }
        }
   
        public static Bank_Name ParseBankName(string bankName)
        {
            bankName = bankName.Trim();
            Bank_Name name = 0;

            switch (bankName)
            {
                case "Bank of America":
                    name = Bank_Name.BOA;
                    break;
                case "Morgan Stanely":
                    name = Bank_Name.Morgan;
                    break;
                case "Capitol One":
                    name = Bank_Name.Capitol;
                    break;
                case "Goldman Sachs":
                    name = Bank_Name.Goldman;
                    break;
                case "TD Bank":
                    name = Bank_Name.TD;
                    break;
                case "Chase":
                    name = Bank_Name.Chase;
                    break;
                case "HSBC":
                    name = Bank_Name.HSBC;
                    break;
                default:                    
                    return name;                  
            }
            return name;            
        }

        public static string ParseNameOfBank(Bank_Name name)
        {
            string bankName;

            if (name == 0)
            {
                return null;
            }

            switch (name)
            {
                case Bank_Name.BOA:
                    bankName = "Bank of America";
                    break;
                case Bank_Name.Morgan:
                    bankName = "Morgan Stanley";
                    break;
                case Bank_Name.Capitol:
                    bankName = "Capitol One";
                    break;
                case Bank_Name.Goldman:                  
                    bankName = "Goldman Sachs";
                    break;
                case Bank_Name.TD:                  
                    bankName = "TD Bank";
                    break;
                case Bank_Name.Chase:                    
                    bankName = "Chase";
                    break;
                case Bank_Name.HSBC:                  
                    bankName = "HSBC";
                    break;
                default:
                    bankName = null;
                    break;
            }
            return bankName;
        }

        public static AccountType ParseAccountType(string type)
        {
            type = type.Trim();
            AccountType account = 0;

            try
            {
                account = (AccountType)Enum.Parse(typeof(AccountType), type, true);
            }
            catch (ArgumentException e)
            {
                MessageBox.Show("Invalid account type, please try again");
            }

            return account;
        }
        public static void ShowAccountDetails(Account account, MainWindow window)
        {
            AccountType accountType;

            if (account.GetType() == typeof(CheckingAccount))
                accountType = AccountType.Checking;
            else if (account.GetType() == typeof(SavingsAccount))
                accountType = AccountType.Savings;
            else
                accountType = AccountType.Business;

            if (accountType == AccountType.Checking)
            {
                window.UserOutput.Text = "Account Holder: " + account.AccountHolder + "\r\n"
                    + "Account Type: " + accountType.ToString() + "\r\n"
                    + "Current Balance: $" + account.Balance + "\r\n"
                    + "Bank Name: " + AccountManagerServices.ParseNameOfBank(account.BankName) + "\r\n";
                return;
            }

            if (accountType == AccountType.Savings)
            {
                window.UserOutput.Text = "Account Holder: " + account.AccountHolder + "\r\n"
                    + "Account Type: " + accountType.ToString() + "\r\n"
                    + "Current Balance: $" + account.Balance + "\r\n"
                    + "Bank Name: " + AccountManagerServices.ParseNameOfBank(account.BankName) + "\r\n"
                    + "Withdrawals this Month: " + ((SavingsAccount)account).WithdrawalsThisMonth + "\r\n";
            }

            if (accountType == AccountType.Business)
            {
                window.UserOutput.Text = "Account Holder: " + account.AccountHolder + "\r\n"
                   + "Business Name: " + ((BusinessAccount)account).BusinessName + "\r\n"
                   + "Account Type: " + accountType.ToString() + "\r\n"
                   + "Current Balance: $" + account.Balance + "\r\n"
                   + "Bank Name: " + AccountManagerServices.ParseNameOfBank(account.BankName) + "\r\n";
            }
        }
    }
    }
