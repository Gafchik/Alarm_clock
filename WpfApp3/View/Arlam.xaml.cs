using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace WpfApp3.View
{
    /// <summary>
    /// Логика взаимодействия для Arlam.xaml
    /// </summary>
    public partial class Arlam : Window 
    {
        public MainWindow win;
        public Arlam(MainWindow mainWindow)
        {
            win = mainWindow;
            Closed += Arlam_Closed;
            InitializeComponent();
            DataContext = new Arlam_ModelView();
            H_Slider.ValueChanged += H_Slider_ValueChanged;
            M_Slider.ValueChanged += M_Slider_ValueChanged;
            arlam.IsChecked = MainWindow.settings.Arlam;
            arlam.Checked += Arlam_Checked;
            arlam.Unchecked += Arlam_Unchecked;
        }

        private void Arlam_Closed(object sender, EventArgs e) => win.SaveSetting();
       

        private void Arlam_Unchecked(object sender, RoutedEventArgs e) => MainWindow.settings.Arlam = false;

        private void Arlam_Checked(object sender, RoutedEventArgs e) => MainWindow.settings.Arlam = true;


        private void M_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => Txt_M.Text = Convert.ToInt32(M_Slider.Value).ToString();


        private void H_Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)=> Txt_H.Text = Convert.ToInt32(H_Slider.Value).ToString();


        private void Button_Click(object sender, RoutedEventArgs e) => (DataContext as Arlam_ModelView).SetArlam(Convert.ToInt32(H_Slider.Value), Convert.ToInt32(M_Slider.Value));
        
    }
}
