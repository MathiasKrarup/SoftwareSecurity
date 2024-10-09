using SoftwareSecurity.Model;
using SoftwareSecurity.Services.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SoftwareSecurity
{
    /// <summary>
    /// Interaction logic for AddCredentialView.xaml
    /// </summary>
    public partial class AddCredentialView : Window
    {
        private readonly ICredentialService _credentialService;
        private Credential _credential;


        public AddCredentialView(ICredentialService credentialService, Credential credential = null)
        {
            InitializeComponent();
            _credentialService = credentialService;
            _credential = credential;

            if (_credential != null)
            {
                WebsiteTextBox.Text = _credential.Website;
                UsernameTextBox.Text = _credential.Username;
                PasswordBox.Password = _credential.Password;
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var website = WebsiteTextBox.Text.Trim();
            var username = UsernameTextBox.Text.Trim();
            var password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(password))
            {
                ErrorTextBlock.Text = "Password is required.";
                return;
            }

            if (_credential == null)
            {
                var newCredential = new Credential
                {
                    Website = website, 
                    Username = username,
                    Password = password
                };

                try
                {
                    await _credentialService.AddCredentialAsync(newCredential);
                    MessageBox.Show("Credential added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    ErrorTextBlock.Text = "An error occurred while adding the credential";
                }
            }
            else
            {
                _credential.Website = website; 
                _credential.Username = username; 
                _credential.Password = password;

                try
                {
                    await _credentialService.UpdateCredentialAsync(_credential);
                    MessageBox.Show("Credential updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    ErrorTextBlock.Text = "An error occurred while updating the credential";
                }
            }
        }



        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void GeneratePassword_Click(object sender, RoutedEventArgs e)
        {
            var generatedPassword = GenerateRandomPassword(10); 
            PasswordBox.Password = generatedPassword;
            MessageBox.Show("Password generated and set", "Password Generated", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private string GenerateRandomPassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
            StringBuilder result = new StringBuilder();
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (result.Length < length)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    result.Append(validChars[(int)(num % (uint)validChars.Length)]);
                }
            }
            return result.ToString();
        }
    }
}
