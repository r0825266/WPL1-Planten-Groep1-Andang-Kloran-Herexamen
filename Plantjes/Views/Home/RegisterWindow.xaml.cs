using System.Net;
using System.Windows;
using System.Windows.Controls;
using Plantjes.ViewModels;

namespace Plantjes.Views.Home;

/// <summary>
///     Interaction logic for RegisterWindow.xaml
/// </summary>
public partial class RegisterWindow : Window {
    public RegisterWindow() {
        DataContext = App.Current.Services.GetService(typeof(ViewModelRegister));
        InitializeComponent();
    }
    private void txtWachtwoord_PasswordChanged(object sender, RoutedEventArgs e) {
        // Xander - PasswordBox
        if (DataContext != null) ((dynamic)DataContext)._passwordInput = new NetworkCredential(string.Empty, ((PasswordBox)sender).SecurePassword).Password;
        // end Xander
    }
    private void txtWachtwoordHerhaal_OnPasswordChanged(object sender, RoutedEventArgs e) {
        // Xander - PasswordBox
        if (DataContext != null) ((dynamic)DataContext)._passwordRepeatInput = new NetworkCredential(string.Empty, ((PasswordBox)sender).SecurePassword).Password;
        // end Xander
    }
}