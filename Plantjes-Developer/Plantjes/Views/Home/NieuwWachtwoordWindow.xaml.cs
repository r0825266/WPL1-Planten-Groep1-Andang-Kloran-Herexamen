using Plantjes.ViewModels;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace Plantjes.Views.Home
{
    /// <summary>
    /// Interaction logic for NieuwWachtwoordWindow.xaml
    /// </summary>
    public partial class NieuwWachtwoordWindow : Window
    {
        public NieuwWachtwoordWindow()
        {
            DataContext = App.Current.Services.GetService(typeof(ViewModelNieuwWachtwoord));
            InitializeComponent();
        }
        private void txtWachtwoord_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Kjell - code van Xander van andere password boxen
            if (DataContext != null) ((dynamic)DataContext)._passwordInput = new NetworkCredential(string.Empty, ((PasswordBox)sender).SecurePassword).Password;
        }
        private void txtWachtwoordHerhaal_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            // Kjell - code van Xander van andere password boxen
            if (DataContext != null) ((dynamic)DataContext)._passwordRepeatInput = new NetworkCredential(string.Empty, ((PasswordBox)sender).SecurePassword).Password;
        }
    }


}
