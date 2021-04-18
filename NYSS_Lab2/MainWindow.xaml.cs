using NYSS_Lab2.Controllers;
using NYSS_Lab2.Models;
using NYSS_Lab2.Pagination;
using NYSS_Lab2.Process;
using NYSS_Lab2.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NYSS_Lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ThreatController threatController;
        private PagingCollectionView shortThreatsWithPaging;
        private int[] RecordsPerPage = { 15, 30, 50 };
        private UpdateReport updateReport;

        public MainWindow()
        {
            InitializeComponent();
            threatController = new ThreatController(new ThreatBusiness(ConstantStrings.SourceFileName, ConstantStrings.SourceFileUrl, new ThreatRepository(ConstantStrings.LocalStorageFileName)));
            var shortThreats = threatController.GetAllShortThreats();
            if (shortThreats != null)
            {
                InitializeThreadList(shortThreats);
            }
            else
            {
                DisableControls();
            }
            btnUpdateReport.IsEnabled = false;
        }

        private void tbCurrentPage_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }

            if(e.Text[0]=='\r')
            {
                dataGrid.Focus();
            }
        }

        private void cbRecordsPerPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            shortThreatsWithPaging.ItemsPerPage = int.Parse(((ComboBox)sender).SelectedItem.ToString());
            shortThreatsWithPaging.MoveToPage(1);
            UpdateFields();
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            shortThreatsWithPaging.MoveToPreviousPage();
            UpdateFields();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            shortThreatsWithPaging.MoveToNextPage();
            UpdateFields();
        }

        private void UpdateFields()
        {
            tbCurrentPage.Text = $"{shortThreatsWithPaging.CurrentPage}";
            tbCountPage.Text = $"/{shortThreatsWithPaging.PageCount}";
        }

        private void tbCurrentPage_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbCurrentPage.Text))
            {
                shortThreatsWithPaging.MoveToPage(int.Parse(tbCurrentPage.Text));
            }

            UpdateFields();
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var shortThreat = (ShortThreat)((DataGridRow)sender).Item;
            new ThreatWindow(threatController.GetThreat(shortThreat.Id)).Show();
        }

        private void DisableControls()
        {
            cbRecordsPerPage.IsEnabled = false;
            btnNext.IsEnabled = false;
            btnPrev.IsEnabled = false;
            tbCurrentPage.IsEnabled = false;
        }

        private void EnableControls()
        {
            cbRecordsPerPage.IsEnabled = true;
            btnNext.IsEnabled = true;
            btnPrev.IsEnabled = true;
            tbCurrentPage.IsEnabled = true;
        }

        private void InitializeThreadList(IEnumerable<ShortThreat> shortThreats)
        {
            shortThreatsWithPaging = new PagingCollectionView(shortThreats.ToList(), RecordsPerPage[0]);
            DataContext = shortThreatsWithPaging;
            cbRecordsPerPage.ItemsSource = RecordsPerPage;
            cbRecordsPerPage.SelectedItem = RecordsPerPage[0];
            EnableControls();
            UpdateFields();
        }

        private void btnDownloadUpdate_Click(object sender, RoutedEventArgs e)
        {
            updateReport = threatController.AutomationUpdate();
            InitializeThreadList(threatController.GetAllShortThreats());
            btnUpdateReport.IsEnabled = true;
            new InfoWindow().Show();
        }

        private void btnUpdateReport_Click(object sender, RoutedEventArgs e)
        {
            new UpdateReportWindow(updateReport).Show();
        }
    }
}
