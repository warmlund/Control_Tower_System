using Control_Tower_System_BLL;
using System.Windows;

namespace Control_Tower_System_PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new ViewModel(new ControlTower());
            InitializeComponent();
        }
    }
}