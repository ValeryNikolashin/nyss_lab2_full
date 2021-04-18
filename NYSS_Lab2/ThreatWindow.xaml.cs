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
    /// Interaction logic for ThreatWindow.xaml
    /// </summary>
    public partial class ThreatWindow : Window
    {
        private readonly List<Threat> _threat = new List<Threat>();
        public ThreatWindow(Threat threat)
        {
            InitializeComponent();
            _threat.Add(threat);
            dataGrid.DataContext = _threat;
        }
    }
}
