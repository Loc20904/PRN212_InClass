using System.Windows.Controls;
using WPFApp.ViewModel;

namespace WPFApp.Page
{
    public partial class CustomerManagePage : System.Windows.Controls.Page
    {
        public CustomerManagePage()
        {
            InitializeComponent();
            DataContext = new CustomerManagerViewModel();
        }
    }
}