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
        String userName;
        Account currentAccount;

        public MainWindow()
        {
            InitializeComponent();

            FindWindow findWindow = new FindWindow();

            findWindow.ShowDialog();      

        //    using (AccountManagerContext context = new AccountManagerContext()) {
        //        SavingsAccount account = new SavingsAccount
        //        {
        //            PinNumber = 2010,
        //            AccountHolder = "James Doe",
        //            BankName = Bank_Name.Chase,
        //            Balance = 1212,
        //            AccountOpenDate = DateTime.Parse(DateTime.Today.ToString()),
        //            WithdrawalsThisMonth = 0
        //    };

        //    context.SavingsAccounts.Add(account);
        //    context.SaveChanges();
        //}
        }

        private void Deposit_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Withdraw_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FindAccount_Click(object sender, RoutedEventArgs e)
        {
              FindWindow findWindow = new FindWindow();

              findWindow.ShowDialog();
        }

        private void Transfer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Switch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
