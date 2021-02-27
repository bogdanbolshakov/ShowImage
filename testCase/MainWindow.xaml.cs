using System.Windows;
using testCase.ViewerViewModel;

namespace testCase
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewerVM();
        }
    }
}
