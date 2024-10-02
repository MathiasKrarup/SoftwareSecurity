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

namespace SoftwareSecurity
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            CheckFirstTimeSetup();
        }

        private void CheckFirstTimeSetup()
        {
            if (!MasterPasswordExists())
            {
                var setupWindow = new SetupMasterPasswordWindow();
                setupWindow.ShowDialog();
            }
        }

        private bool MasterPasswordExists()
        {
            // Implement logic to check if set later
            return false; // Placeholder change later
        }

        private void UnlockButton_Click(object sender, RoutedEventArgs e)
        {
            var enteredPassword = MasterPasswordBox.Password;

            if (ValidateMasterPassword(enteredPassword))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                ErrorTextBlock.Text = "Incorrect master password. Please try again.";
            }
        }

        private bool ValidateMasterPassword(string password)
        {
            return password == "password123"; //Placeholder change later
        }
    }

}
