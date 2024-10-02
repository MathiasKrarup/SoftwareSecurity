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
    /// Interaction logic for SetupMasterPasswordWindow.xaml
    /// </summary>
    public partial class SetupMasterPasswordWindow : Window
    {
        public SetupMasterPasswordWindow()
        {
            InitializeComponent();
        }

        private void SetPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var password = MasterPasswordBox.Password;
            var confirmPassword = ConfirmPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(password))
            {
                ErrorTextBlock.Text = "Password cannot be empty.";
                return;
            }

            if (password != confirmPassword)
            {
                ErrorTextBlock.Text = "Passwords do not match.";
                return;
            }

            
            SaveMasterPassword(password);

            this.Close();
        }

        /// <summary>
        /// Implement a secure way of storing the password
        /// </summary>
        /// <param name="password"></param>
        private void SaveMasterPassword(string password)
        {
            throw new NotImplementedException();
        }

    }
}
