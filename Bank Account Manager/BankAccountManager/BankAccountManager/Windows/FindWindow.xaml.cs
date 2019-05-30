using System.Linq;
using System.Windows;
using BankAccountManager.Classes;

namespace BankAccountManager.Windows
{


    public partial class FindWindow : Window
    {
        public FindWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            //Gets the user input and parses it into a float while also performing integrity testing

            bool success = int.TryParse(PinInput.GetLineText(0),out int pin);
            string name = HolderNameInput.GetLineText(0);
            Bank_Name bank = AccountManagerServices.ParseBankName(BankNameInput.GetLineText(0));
            AccountType type = AccountManagerServices.ParseAccountType(AccountTypeInput.GetLineText(0));

            //Failure State actions
            
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

            //This window appears can appear before or after the main window 
            //Thusly the Application class function may return null
            //In that case a new instance of the main window will be created

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
