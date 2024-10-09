using Microsoft.Extensions.DependencyInjection;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IMasterPasswordService _masterPasswordService;
        private readonly IEncryptionService _encryptionService;
        private readonly IServiceProvider _serviceProvider;

        public LoginWindow(
            IMasterPasswordService masterPasswordService,
            IEncryptionService encryptionService,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _masterPasswordService = masterPasswordService;
            _encryptionService = encryptionService;
            _serviceProvider = serviceProvider;
            CheckFirstTimeSetup();
        }

        // Used for first time setup
        private async void CheckFirstTimeSetup()
        {
            if (!await _masterPasswordService.IsMasterPasswordSetAsync())
            {
                var setupWindow = new SetupMasterPasswordWindow(_masterPasswordService);
                setupWindow.Show();
                this.Close();
            }
        }


        private async void UnlockButton_Click(object sender, RoutedEventArgs e)
        {
            var enteredPassword = MasterPasswordBox.Password;

            if (await _masterPasswordService.ValidateMasterPasswordAsync(enteredPassword))
            {
                await _encryptionService.InitializeAsync(enteredPassword);

                var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                ErrorTextBlock.Text = "Incorrect master password. Please try again.";
            }
        }


    }

}
