using System.Windows.Controls;
using DE180158WPF.ViewModel;

namespace DE180158WPF.View
{
    public partial class BookingManagePage : Page
    {
        public BookingManagePage()
        {
            InitializeComponent();
            DataContext = new BookingManagerViewModel();
        }
    }
}