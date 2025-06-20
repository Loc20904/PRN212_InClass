using System.Windows.Controls;

using DE180158WPF.ViewModel;
namespace DE180158WPF.View {
public partial class StatisticPage : Page
{
    public StatisticPage()
    {
        InitializeComponent();
        DataContext = new StatisticsViewModel();
    }
}
}
