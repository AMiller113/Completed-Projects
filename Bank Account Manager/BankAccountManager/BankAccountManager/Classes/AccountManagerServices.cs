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
    enum AccountType : short {Checkings = 1, Savings, Business};

     static class AccountManagerServices
    {
        public static bool CreditAccount(Account account, int credit_amount, out float newBalance)
        {
            Mutex mutex = new Mutex();
            mutex.WaitOne();
            //Critical Section - Begin
            Boolean balanceChanged = false;
            using (var context = new AccountManagerContext())
            {
                account.Balance -= credit_amount;
                newBalance = account.Balance;
                context.SaveChanges();
            }
            balanceChanged = true;
            //Critical Section - End          
            mutex.ReleaseMutex();
            return balanceChanged;
        }

        public static bool DebitAccount(Account account, float debitAmount, out float newBalance)
        {
            Mutex mutex = new Mutex();
            mutex.WaitOne();
            //Critical Section - Begin
            Boolean balanceChanged = false;
            using (var context = new AccountManagerContext())
            {              
                account.Balance -= debitAmount;
                newBalance = account.Balance;
                context.SaveChanges();
            }
            balanceChanged = true;
            //Critical Section - End
            mutex.ReleaseMutex();
            return balanceChanged;
        }

        public static bool TransferMoney(Account starting, Account destination, float amount)
        {
            Mutex mutex = new Mutex();
            mutex.WaitOne();
            //Critical Section - Begin
            Boolean transferComplete = false;
            using (var context = new AccountManagerContext()) {
                
                destination.Balance += amount;
                starting.Balance -= amount;
               
                if (starting.GetType() == typeof(SavingsAccount))
                {
                    SavingsAccount s = (SavingsAccount)starting;
                    s.WithdrawalsThisMonth++;
                }
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

                if (FindAccount(pin, name, bank, AccountType.Checkings) != null)
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
                    case AccountType.Checkings:
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

            try
            {
                name = (Bank_Name) Enum.Parse(typeof(Bank_Name), bankName, true);
            }
            catch(ArgumentException e)
            {
                MessageBox.Show("Invalid Bank Name, please try again");
            }                        
            
            return name;
        }

        public static string ParseNameOfBank(Bank_Name name)
        {
            return name.ToString();
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
      }
    }
