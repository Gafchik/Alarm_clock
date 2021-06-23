using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp3.View;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> them;
        public MainWindow()
        {
            them = new List<string>() { "DayStyle", "NightStyle" };

            InitializeComponent();
            DataContext = new ModelView();
            Application.Current.Resources.Clear();
            ResourceDictionary resource = (ResourceDictionary)Application.LoadComponent(new Uri("Style/" + them[0] + ".xaml", UriKind.Relative));
            Application.Current.Resources.MergedDictionaries.Add(resource);
        }

        private void Button_Click(object sender, RoutedEventArgs e) => new Setting().ShowDialog();


    }
}
