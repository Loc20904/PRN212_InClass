using System.Windows.Controls;
using DE180158WPF.ViewModel;

namespace DE180158WPF.View
{
    public partial class CustomerManagePage : Page
    {
        public CustomerManagePage()
        {
            InitializeComponent();
            DataContext = new CustomerManagerViewModel();
        }
    }
}