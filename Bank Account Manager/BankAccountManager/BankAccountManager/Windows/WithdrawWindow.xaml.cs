using BankAccountManager.Classes;
using System.Linq;
using System.Windows;

namespace BankAccountManager.Windows
{
    public partial class WithdrawWindow : Window
    {
        //The account that will have the widraw function called on it 
        Account account;


        //The constructors are overloaded, The first is not in use currently. 
        //I opted to use this method to pass the currently loaded account to the various windows.

        public WithdrawWindow()
        {
            InitializeComponent();
        }

        public WithdrawWindow(ref Account account)
        {
            this.account = account;
            InitializeComponent();
        }
         
        private void WithdrawalConfirm_Click(object sender, RoutedEventArgs e)
        {
            //Gets the user input and parses it into a float while also performing integrity testing

            bool success = float.TryParse(WithdrawalSubmission.GetLineText(0), out float creditAmount);

            bool complete;

            if (success)
            {
                complete = AccountManagerServices.DebitAccount(account, creditAmount);
            }
            else
            {
                MessageBox.Show("Invalid submission detected");
                return;
            }

            if (complete)
            {
                MessageBox.Show("Transaction Completed. Your new balance is $" + account.Balance);

                //Gets a reference to the main window and updates the output

                MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                AccountManagerServices.ShowAccountDetails(account, mainWindow);
                Close();
            }           
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) { Close(); }
    }
}
