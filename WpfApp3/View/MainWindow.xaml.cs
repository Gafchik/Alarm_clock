using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using WpfApp3.Model;
using WpfApp3.View;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer t = new Timer();
        XmlSerializer formatter = new XmlSerializer(typeof(Settings)); // серелизатор для настроек
        public static Settings settings = new Settings(); // класс для настроек
        List<string> them; // коллекция с темами
        public string path_setting = Environment.CurrentDirectory; // путь для настроек
        public string path_exe = Environment.CurrentDirectory; // путь для ехе
        public MainWindow()
        {        
            t.Interval = 10000;
            t.Elapsed += T_Elapsed;
            t.Start();

            path_setting += "\\setting.xml";
            path_exe += "\\WpfApp3.exe";
            if (Directory.GetFiles(Environment.CurrentDirectory).ToList().Exists(i => i == path_setting))
            {
                try
                {
                    using (FileStream fs = new FileStream("setting.xml", FileMode.Open))
                    {
                        settings = (Settings)formatter.Deserialize(fs);
                    }
                }
                catch (Exception)
                { SaveSetting(); }
            }
            else
            { SaveSetting(); }


            them = new List<string>() { "DayStyle", "NightStyle" };

            InitializeComponent();
            DataContext = new ModelView();
            if (settings.ThemAuto)
                AutoThem();
            else
            {
                if (settings.ThemNightIsEnable)
                {
                    if (settings.ThemNight)
                        SetNightThem();
                    else
                        SetDayThem();
                }
                else
                    SetDayThem();
            }
        }

        private void T_Elapsed(object sender, ElapsedEventArgs e) => AutoThem();
        public void AutoThem()
        {
            if (settings.ThemAuto)
            {
                if (DateTime.Now.Hour > 18)
                    SetNightThem();
                else
                    SetDayThem();
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e) => new Setting(this).ShowDialog();
        
       public  void SaveSetting()
        {
            try
            {
                using (FileStream fs = new FileStream("setting.xml", FileMode.Truncate, FileAccess.Write))
                { formatter.Serialize(fs, settings); }
            }
            catch (Exception)
            {
                using (FileStream fs = new FileStream("setting.xml", FileMode.OpenOrCreate))
                { formatter.Serialize(fs, settings); }
            }
            
        }
        public void SetDayThem()
        {
            Application.Current.Resources.Clear();
            ResourceDictionary resource = (ResourceDictionary)Application.LoadComponent(new Uri("Style/" + them[0] + ".xaml", UriKind.Relative));
            Application.Current.Resources.MergedDictionaries.Add(resource);
           
        }
        public void SetNightThem()
        {
            Application.Current.Resources.Clear();
            ResourceDictionary resource = (ResourceDictionary)Application.LoadComponent(new Uri("Style/" + them[1] + ".xaml", UriKind.Relative));
            Application.Current.Resources.MergedDictionaries.Add(resource);
           
        }

        internal void Autorun()
        {
            if (settings.Autorun)
            {
                var autostart = Registry.CurrentUser.CreateSubKey("Software").
                        CreateSubKey("Microsoft").
                        CreateSubKey("Windows").
                        CreateSubKey("CurrentVersion").
                        CreateSubKey("Run");
                autostart.SetValue("Alarm_clock", path_exe);
            }
            else
            {
                var autostart = Registry.CurrentUser.CreateSubKey("Software").
                       CreateSubKey("Microsoft").
                       CreateSubKey("Windows").
                       CreateSubKey("CurrentVersion").
                       CreateSubKey("Run");
                autostart.DeleteValue("Alarm_clock");
            }
        }
    }
}
