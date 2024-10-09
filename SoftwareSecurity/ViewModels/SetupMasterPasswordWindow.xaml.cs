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

namespace SoftwareSecurity.ViewModels
{ 
    /// <summary>
    /// Interaction logic for SetupMasterPasswordWindow.xaml
    /// </summary>
    public partial class SetupMasterPasswordWindow : Window
{
    private readonly IMasterPasswordService _masterPasswordService;
    private readonly IEncryptionService _encryptionService;
    private readonly IServiceProvider _serviceProvider;


    public SetupMasterPasswordWindow(
               IMasterPasswordService masterPasswordService,
               IEncryptionService encryptionService,
               IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _masterPasswordService = masterPasswordService;
        _encryptionService = encryptionService;
        _serviceProvider = serviceProvider;
    }

    private async void SetPasswordButton_Click(object sender, RoutedEventArgs e)
    {
        var newPassword = NewPasswordBox.Password;
        var confirmPassword = ConfirmPasswordBox.Password;

        if (string.IsNullOrWhiteSpace(newPassword))
        {
            ErrorTextBlock.Text = "Password cannot be empty.";
            return;
        }

        if (newPassword != confirmPassword)
        {
            ErrorTextBlock.Text = "Passwords do not match.";
            return;
        }

        try
        {
            await _masterPasswordService.SetMasterPasswordAsync(newPassword);

            await _encryptionService.InitializeAsync(newPassword);

            var loginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            this.Close();
        }
        catch (Exception ex)
        {
            ErrorTextBlock.Text = "An error occurred while setting the master password.";
        }
    }

}
}
