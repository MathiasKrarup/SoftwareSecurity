using Microsoft.Extensions.DependencyInjection;
using SoftwareSecurity.Model;
using SoftwareSecurity.Services.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Security.Cryptography;
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
        private readonly IServiceProvider _serviceProvider;

        public ObservableCollection<Credential> Credentials { get; set; } = new ObservableCollection<Credential>();


        public MainWindow(ICredentialService credentialService, IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _credentialService = credentialService;
            _serviceProvider = serviceProvider;

            DataContext = this;

            LoadCredentials();
        }

        private async void LoadCredentials()
        {
            try
            {
                var credentials = await _credentialService.GetAllCredentialsAsync();
                Credentials.Clear();
                foreach (var credential in credentials)
                {
                    Credentials.Add(credential);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading credentials", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async void AddNewCredential_Click(object sender, RoutedEventArgs e)
        {
            var addCredentialView = _serviceProvider.GetRequiredService<AddCredentialView>();
            var result = addCredentialView.ShowDialog();
            if (result == true)
            {
                LoadCredentials();
            }
        }


        private void EditCredential_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is Credential selectedCredential)
            {
                var editCredentialView = new AddCredentialView(_credentialService, selectedCredential);
                var result = editCredentialView.ShowDialog();
                if (result == true)
                {
                    LoadCredentials();
                }
            }
            else
            {
                MessageBox.Show("Please select a credential to edit", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private async void DeleteCredential_Click(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is Credential selectedCredential)
            {
                var result = MessageBox.Show($"Are you sure you want to delete the credential for {selectedCredential.Website}?",
                                             "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _credentialService.DeleteCredentialAsync(selectedCredential.Id);
                        MessageBox.Show("Credential deleted", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadCredentials();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while deleting the credential {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("select a credential to delete", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }



        private void GeneratePassword_Click(object sender, RoutedEventArgs e)
        {
            var passwordGeneratorWindow = new PasswordGeneratorWindow();
            passwordGeneratorWindow.ShowDialog();
        }



        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            var filterText = SearchTextBox.Text.ToLower();
            if (string.IsNullOrWhiteSpace(filterText))
            {
                CredentialDataGrid.ItemsSource = Credentials;
            }
            else
            {
                CredentialDataGrid.ItemsSource = new ObservableCollection<Credential>(
                    Credentials.Where(c => c.Website.ToLower().Contains(filterText) || c.Username.ToLower().Contains(filterText))
                );
            }
        }

    }
}