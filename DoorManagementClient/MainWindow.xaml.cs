using System.Windows;
using Autofac;
using DoorManagementClient.ViewModel;

namespace DoorManagementClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Bootstrap.Init();
            this.DataContext = new MainWindowViewModel(Bootstrap.Container.Resolve<IDoorManagementServiceIntegration>());
        }
    }
}
