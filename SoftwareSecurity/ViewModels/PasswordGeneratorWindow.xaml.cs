using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for PasswordGeneratorWindow.xaml
    /// </summary>
    public partial class PasswordGeneratorWindow : Window
    {
        public PasswordGeneratorWindow()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            int passwordLength = (int)LengthSlider.Value;

            bool includeUppercase = IncludeUppercaseCheckBox.IsChecked == true;
            bool includeLowercase = IncludeLowercaseCheckBox.IsChecked == true;
            bool includeNumbers = IncludeNumbersCheckBox.IsChecked == true;
            bool includeSymbols = IncludeSymbolsCheckBox.IsChecked == true;

            if (!includeUppercase && !includeLowercase && !includeNumbers && !includeSymbols)
            {
                MessageBox.Show("select at least one character type", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string password = GeneratePassword(passwordLength, includeUppercase, includeLowercase, includeNumbers, includeSymbols);

            GeneratedPasswordTextBox.Text = password;
        }


        private string GeneratePassword(int length, bool includeUppercase, bool includeLowercase, bool includeNumbers, bool includeSymbols)
        {
            const string uppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowercaseChars = "abcdefghijklmnopqrstuvwxyz";
            const string numberChars = "0123456789";
            const string symbolChars = "!@#$%^&*()";

            StringBuilder validChars = new StringBuilder();

            if (includeUppercase)
                validChars.Append(uppercaseChars);
            if (includeLowercase)
                validChars.Append(lowercaseChars);
            if (includeNumbers)
                validChars.Append(numberChars);
            if (includeSymbols)
                validChars.Append(symbolChars);

            if (validChars.Length == 0)
                return string.Empty;

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

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(GeneratedPasswordTextBox.Text))
            {
                Clipboard.SetText(GeneratedPasswordTextBox.Text);
                MessageBox.Show("Password copied", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Generate a password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
