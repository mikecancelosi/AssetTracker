using Castle.Windsor;
using System.Windows;

namespace AssetTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var container = new WindsorContainer();

        }
    }
}
