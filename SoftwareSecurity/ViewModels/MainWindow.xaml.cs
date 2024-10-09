using Microsoft.Extensions.DependencyInjection;
using SoftwareSecurity.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoftwareSecurity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ICredentialService _credentialService;

        public MainWindow(ICredentialService credentialService)
        {
            InitializeComponent();
            _credentialService = credentialService;

            LoadCredentials();
        }

        private void LoadCredentials()
        {
            throw new NotImplementedException();
        }

        private void AddNewCredential_Click()
        {
            throw new NotImplementedException();
        }

        private void EditCredential_Click()
        {
            throw new NotImplementedException();

        }

        private void DeleteCredential_Click()
        {
            throw new NotImplementedException();
        }

        private void GeneratePassword_Click()
        {
            throw new NotImplementedException();
        }

        private void SearchTextBox_KeyUp()
        {
            throw new NotImplementedException();
        }
    }
}