using Plantjes.ViewModels;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace Plantjes.Views.Home
{
    /// <summary>
    /// written by Andang Kloran - on code of Xander
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        public ChangePasswordWindow()
        {
            DataContext = App.Current.Services.GetService(typeof(ViewModelChangePassword));
            InitializeComponent();
        }
        private void txtWachtwoord_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null) ((dynamic)DataContext)._passwordInput = new NetworkCredential(string.Empty, ((PasswordBox)sender).SecurePassword).Password;
        }
        private void txtWachtwoordHerhaal_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null) ((dynamic)DataContext)._passwordRepeatInput = new NetworkCredential(string.Empty, ((PasswordBox)sender).SecurePassword).Password;
        }
    }


}
