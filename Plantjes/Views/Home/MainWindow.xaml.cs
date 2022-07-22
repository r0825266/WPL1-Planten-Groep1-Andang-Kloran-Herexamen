using System.Windows;
using Plantjes.ViewModels;

namespace Plantjes.Views.Home; 

/// <summary>
///     Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    public MainWindow() {
        DataContext = App.Current.Services.GetService(typeof(ViewModelMain));
        InitializeComponent();
    }

    private void btn_ClickHideImage(object sender, RoutedEventArgs e)
    {
        img_vives_logo_main.Visibility = Visibility.Hidden;
 
    }
   
}