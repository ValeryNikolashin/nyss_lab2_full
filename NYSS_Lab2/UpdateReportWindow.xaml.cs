using NYSS_Lab2.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for UpdateReportWindow.xaml
    /// </summary>
    public partial class UpdateReportWindow : Window
    {
        public UpdateReportWindow(UpdateReport updateReport)
        {
            InitializeComponent();
            var statusAsText = updateReport.IsSuccessed ? "Успешно." : "Не успешно.";
            tbStatus.Text += $" {statusAsText}";
            if (!updateReport.IsSuccessed)
            {
                tbErrorMessage.Text = $"Ошибка в процессе обновления. {updateReport.ErrorMessage}";
            }
            else
            {
                tbCountOfChanges.Text = $"Количество изменений: {updateReport.Logs.Count}";
                DataContext = updateReport.Logs;
            }
        }
    }
}
