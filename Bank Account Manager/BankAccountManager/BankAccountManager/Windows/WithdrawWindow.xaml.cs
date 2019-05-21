using BankAccountManager.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BankAccountManager.Windows
{
    /// <summary>
    /// Interaction logic for WithdrawWindow.xaml
    /// </summary>
    public partial class WithdrawWindow : Window
    {
        Account account;

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
                MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                AccountManagerServices.ShowAccountDetails(account, mainWindow);
                Close();
            }           
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) { Close(); }
    }
}
