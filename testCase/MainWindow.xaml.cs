using System.Windows;
using testCase.ViewerViewModel;

namespace testCase
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewerVM();
        }
    }
}
