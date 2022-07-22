using Plantjes.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Plantjes.Views.Home
{
    /// <summary>
    /// Interaction logic for FilterWindow.xaml
    /// </summary>
    public partial class FilterWindow : Window
    {
        public FilterWindow()
        {
            DataContext = App.Current.Services.GetService(typeof(ViewModelFilter));
            InitializeComponent();
        }
    }
}
