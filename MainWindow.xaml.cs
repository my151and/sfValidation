using System.Windows;

namespace sfValidation
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MyObject data { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            data = new MyObject();
            DataContext = this;
        }
    }
}
