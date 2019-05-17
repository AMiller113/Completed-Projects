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
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            int pin;
            string name, bankName, accountType;
            Bank_Name bank;
            AccountType type;

            try
            {
                pin = int.Parse(PinInput.GetLineText(0));
                name = UserNameInput.GetLineText(0);
                bankName = BankNameInput.GetLineText(0);
                accountType = AccountTypeInput.GetLineText(0);
                bank = AccountManagerServices.ParseBankName(bankName);
                type = AccountManagerServices.ParseAccountType(accountType);
            }
            catch(Exception i)
            {
                MessageBox.Show("There is an error in your submission");
                return;
            }

            Account account = AccountManagerServices.FindAccount(pin,name,bank,type);

            if (account == null)
            {
                MessageBox.Show("Account not found");
                return;
            }

            MainWindow window = ((MainWindow)Application.Current.MainWindow);

            window.UserOutput.Text = "Account Holder: " + account.AccountHolder + "\r\n"
                + "Account Type: " + type.ToString() + "\r\n"
                + "Current Balance: " + account.Balance + "\r\n"
                + "Bank Name: " + account.BankName.ToString() + "\r\n";

            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e){}
    }
}
