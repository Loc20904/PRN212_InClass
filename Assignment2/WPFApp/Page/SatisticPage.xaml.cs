using System.Windows.Controls;
using WPFApp.ViewModel;

namespace WPFApp.Page {
public partial class StatisticPage : System.Windows.Controls.Page
    {
    public StatisticPage()
    {
        InitializeComponent();
        DataContext = new StatisticsViewModel();
    }
}
}
