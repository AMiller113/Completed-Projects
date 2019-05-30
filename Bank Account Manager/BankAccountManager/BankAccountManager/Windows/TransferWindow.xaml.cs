using BankAccountManager.Classes;
using System;
using System.Linq;
using System.Windows;

namespace BankAccountManager.Windows
{

    public partial class TransferWindow : Window
    {

        Account account;

        public TransferWindow()
        {
            InitializeComponent();
        }

        public TransferWindow(ref Account account)
        {
            this.account = account;
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            //Gets the user input and parses it into a float while also performing integrity testing

            bool success_pin = int.TryParse(PinInput.GetLineText(0), out int pin);
            bool success_transferAmount = float.TryParse(TransferAmountInput.GetLineText(0), out float transferAmount);
            string name = HolderNameInput.GetLineText(0);
            Bank_Name bank = AccountManagerServices.ParseBankName(BankNameInput.GetLineText(0));
            AccountType type = AccountManagerServices.ParseAccountType(AccountTypeInput.GetLineText(0));

            //Failure State actions

            if (!success_pin)
            {
                MessageBox.Show("Invalid Pin Number");
                return;
            }

            if (bank == 0)
            {
                MessageBox.Show("Invalid bank name (specific banks may not be supported)");
                return;
            }

            if (type == 0)
            {
                MessageBox.Show("Invalid account type.");
                return;
            }

            Account receivingAccount = AccountManagerServices.FindAccount(pin, name, bank, type);

            if (receivingAccount == null)
            {
                MessageBox.Show("Account not found");
                return;
            }

            bool complete = AccountManagerServices.TransferMoney(account,receivingAccount, transferAmount);

            //Gets a reference to the main window and updates the output

            if (complete)
            {
                MessageBox.Show("Transaction Completed. Your first account balance is $" + account.Balance + Environment.NewLine+ "Your second account balnace is $"+ receivingAccount.Balance);
                MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                AccountManagerServices.ShowAccountDetails(account, mainWindow);
                Close();
            }
        }
    }
}
