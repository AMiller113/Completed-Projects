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
using BankAccountManager.Classes;

namespace BankAccountManager.Windows
{
    /// <summary>
    /// Interaction logic for FindWindow.xaml
    /// </summary>
    public partial class FindWindow : Window
    {
        public FindWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {                            
            bool success = int.TryParse(PinInput.GetLineText(0),out int pin);
            string name = HolderNameInput.GetLineText(0);
            Bank_Name bank = AccountManagerServices.ParseBankName(BankNameInput.GetLineText(0));
            AccountType type = AccountManagerServices.ParseAccountType(AccountTypeInput.GetLineText(0));

            if (!success)
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

            Account account = AccountManagerServices.FindAccount(pin, name, bank, type);

            if (account == null)
            {
                MessageBox.Show("Account not found");
                return;
            }

            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

            if (mainWindow == null)
            {
                MainWindow window = new MainWindow(ref account);
                AccountManagerServices.ShowAccountDetails(account, window);
                window.Show();
            }
            else
            {
                AccountManagerServices.ShowAccountDetails(account, mainWindow);
                mainWindow.Show();
            }

            Close();
        }

        

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }       
    }
}
