using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp3.View
{
   
    public partial class Setting : Window
    {
        public MainWindow win;
        public Setting(MainWindow mainWindow)
        {
            Closed += Setting_Closed;
            win = mainWindow;
            InitializeComponent();
            Color.Click += Color_Click;
            // Color_Time = MainWindow.settings.ColorTime;
            //Color_Data
            CH_Fofmat_Full.Checked += CH_Fofmat_Full_Checked;
            CH_Fofmat_Full.Unchecked += CH_Fofmat_Full_Unchecked; ;
            if (MainWindow.settings.FullFormatTime)
                CH_Fofmat_Full.IsChecked = true;
            else
                CH_Fofmat_Full.IsChecked = false;

            CH_AutoRun_ON.Checked += CH_AutoRun_ON_Checked;
            CH_AutoRun_ON.Unchecked += CH_AutoRun_ON_Unchecked;
            if (MainWindow.settings.Autorun)
                CH_AutoRun_ON.IsChecked = true;
            else
                CH_AutoRun_ON.IsChecked = false;



            CH_AutoT_ON.Checked += CH_AutoT_ON_Checked;
            CH_AutoT_ON.Unchecked += CH_AutoT_ON_Unchecked;
            if (MainWindow.settings.ThemAuto)
                CH_AutoT_ON.IsChecked = true;

            else
                CH_AutoT_ON.IsChecked = false;

                CH_Day_T.Checked += CH_Day_T_Checked;
                CH_Day_T.Unchecked += CH_Day_T_Unchecked;
            if (MainWindow.settings.ThemNightIsEnable)
            {
                if (MainWindow.settings.ThemNight)
                    CH_Day_T.IsChecked = true;
                else
                    CH_Day_T.IsChecked = false;
            }
            else
                CH_Day_T.IsEnabled = false;


            CH_txt.Checked += CH_txt_Checked;
            CH_txt.Unchecked += CH_txt_Unchecked;
            if (MainWindow.settings.TypeMonth)
                CH_txt.IsChecked = true;
            else
                CH_txt.IsChecked = false;

        }
        // закрытие настроек
        private void Setting_Closed(object sender, EventArgs e) => win.SaveSetting();
        
        //выключение месяц текстом
        private void CH_txt_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.settings.TypeMonth = false;
            (win.DataContext as ModelView).InitializeComponen();
        }
        //включение месяц текстом
        private void CH_txt_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.settings.TypeMonth = true;
            (win.DataContext as ModelView).InitializeComponen();

        }
        //вкючить дневную тему
        private void CH_Day_T_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.settings.ThemNight = false;
            win.SetDayThem();
        }
        //Включить ночную тему
        private void CH_Day_T_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.settings.ThemNight = true;
            win.SetNightThem();
        }
        // убрать из автозапуска
        private void CH_AutoRun_ON_Unchecked(object sender, RoutedEventArgs e)
        { 
            MainWindow.settings.Autorun = false;
            win.Autorun();
        }
        // добавить в автозапуск
        private void CH_AutoRun_ON_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.settings.Autorun = true;
            win.Autorun();
        }


        // 12ти часовой формат
        private void CH_Fofmat_Full_Unchecked(object sender, RoutedEventArgs e)
        {
            MainWindow.settings.FullFormatTime = false;
            (win.DataContext as ModelView).InitializeComponen();
        }
        //24 часа формат
        private void CH_Fofmat_Full_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.settings.FullFormatTime = true;
            (win.DataContext as ModelView).InitializeComponen();
        }

        // ато тема выкл
        private void CH_AutoT_ON_Unchecked(object sender, RoutedEventArgs e)
        {
            CH_Day_T.IsEnabled = true;
            MainWindow.settings.ThemNightIsEnable = true;
            MainWindow.settings.ThemAuto = false;
            win.SetDayThem();
        }
        //автотема вкл
        private void CH_AutoT_ON_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.settings.ThemAuto = true;
            CH_Day_T.IsEnabled = false;
            if (CH_Day_T.IsChecked.Value)
                CH_Day_T.IsChecked = false;
            MainWindow.settings.ThemNightIsEnable = false;
            win.AutoThem();


        }

        private void Color_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
           DialogResult rezult = MyDialog.ShowDialog();
           
           
        }
    }
}
