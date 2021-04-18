using NYSS_Lab2.Controllers;
using NYSS_Lab2.Process;
using NYSS_Lab2.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace NYSS_Lab2
{
    /// <summary>
    /// Interaction logic for InfoWindow.xaml
    /// </summary>
    public partial class CheckoutWindow : Window
    {
        private readonly ThreatController _threatController;

        public CheckoutWindow()
        {
            InitializeComponent();
            _threatController = new ThreatController(new ThreatBusiness(ConstantStrings.SourceFileName, ConstantStrings.SourceFileUrl, new ThreatRepository(ConstantStrings.LocalStorageFileName)));
            if (new FileInfo(ConstantStrings.LocalStorageFileName).Exists && _threatController.GetAllShortThreats()!=null)
            {
                MainWindowShow();
            }         
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            _threatController.AutomationUpdate();
            MainWindowShow();
        }

        private void MainWindowShow()
        {
            new MainWindow().Show();
            Close();
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            MainWindowShow();
        }
    }
}
