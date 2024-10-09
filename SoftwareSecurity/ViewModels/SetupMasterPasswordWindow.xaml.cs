using SoftwareSecurity.Services.Interfaces;
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
        private readonly IMasterPasswordService _masterPasswordService;

        public SetupMasterPasswordWindow(IMasterPasswordService masterPasswordService)
        {
            InitializeComponent();
            _masterPasswordService = masterPasswordService;
        }

        private async void SetPasswordButton_Click(object sender, RoutedEventArgs e)
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

            
            await _masterPasswordService.SetMasterPasswordAsync(password);

            this.Close();
        }

    }
}
