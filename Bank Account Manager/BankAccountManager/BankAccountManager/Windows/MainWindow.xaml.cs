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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BankAccountManager.Windows;

namespace BankAccountManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
