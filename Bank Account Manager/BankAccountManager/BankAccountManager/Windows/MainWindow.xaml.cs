using BankAccountManager.Classes;
using System.Windows;
using BankAccountManager.Windows;

namespace BankAccountManager
{
    //The main window hosts the User output window as well as being a hub to all the functions of the application
    //This window is actually created after the find window initially as
    //Displaying this window without a loaded account wouldnt be logical

    public partial class MainWindow : Window
    {
        Account account;

        public MainWindow() { InitializeComponent(); }

        public MainWindow(ref Account account) { this.account = account; InitializeComponent(); }

        private void Deposit_Click(object sender, RoutedEventArgs e)  { DepositWindow deposit = new DepositWindow(ref account); deposit.ShowDialog(); }
        
        private void Withdraw_Click(object sender, RoutedEventArgs e) { WithdrawWindow withdrawWindow = new WithdrawWindow(ref account); withdrawWindow.ShowDialog(); }
        
        private void FindAccount_Click(object sender, RoutedEventArgs e) { FindWindow findWindow = new FindWindow();  findWindow.ShowDialog(); }

        private void Transfer_Click(object sender, RoutedEventArgs e) { TransferWindow transfer = new TransferWindow(ref account); transfer.Show(); }
       
    }
}
