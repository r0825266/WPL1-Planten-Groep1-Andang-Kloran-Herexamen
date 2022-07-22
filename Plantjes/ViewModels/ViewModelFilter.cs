using Microsoft.Toolkit.Mvvm.Input;
using Plantjes.Views.Home;
using Plantjes.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Plantjes.ViewModels
{
    internal class ViewModelFilter
    {
        public ViewModelFilter()
        {
            cancelCommand = new RelayCommand(CancelFilterClick);
        }

        public RelayCommand cancelCommand { get; set; }

        public void CancelFilterClick()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            Application.Current.Windows[2]?.Close();
        }
    }


}
