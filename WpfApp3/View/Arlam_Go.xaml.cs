using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp3.View
{
    /// <summary>
    /// Логика взаимодействия для Arlam_Go.xaml
    /// </summary>
    public partial class Arlam_Go : Window
    {
       
        public Arlam_Go()
        {
           // path_waw += "\\q.waw";
            InitializeComponent();
            Loaded += Arlam_Go_Loaded;
            MainWindow.settings.Arlam = false;
        }

        private void Arlam_Go_Loaded(object sender, RoutedEventArgs e) => Play();
       
        async void Play()
        {       
                for (int i = 0; i < 100; i++)
                { SystemSounds.Asterisk.Play(); }                  
        }
        private void Button_Click(object sender, RoutedEventArgs e)=> Close();

    }
}
