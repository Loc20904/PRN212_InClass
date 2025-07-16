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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFApp.ViewModel;

namespace WPFApp.Page
{
    /// <summary>
    /// Interaction logic for RoomManagerPage.xaml
    /// </summary>
    public partial class RoomManagerPage : System.Windows.Controls.Page
    {
        public RoomManagerPage()
        {
            InitializeComponent();
            this.DataContext = new RoomManagerViewModel();
        }
    }
}
