using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using System.Windows;
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
        List<Uri> them; // коллекция с темами
        public string setings_foder_name = "\\Alarm clock";
       
        public string path_setting = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); // путь для настроек
        public string path_exe = Environment.CurrentDirectory; // путь для ехе
        public MainWindow()
        {
            path_setting += setings_foder_name;
            t.Interval = 10000;
            t.Elapsed += T_Elapsed;
            t.Start();
           if( !Directory.Exists(path_setting))
                Directory.CreateDirectory(path_setting);
           

            path_setting += "\\setting.xml";
            path_exe += "\\WpfApp3.exe";
            if (Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + setings_foder_name).ToList().Exists(i => i == path_setting))
            {
                try
                {
                    using (FileStream fs = new FileStream(path_setting, FileMode.Open))
                    {
                        settings = (Settings)formatter.Deserialize(fs);
                    }
                }
                catch (Exception)
                { SaveSetting(); }
            }
            else
            { SaveSetting(); }


            them = new List<Uri>
            {
               new Uri("Style/" + "DayStyle" + ".xaml", UriKind.Relative) ,
               new Uri("Style/" + "NightStyle" + ".xaml", UriKind.Relative) ,

            };

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

        public void T_Elapsed(object sender, ElapsedEventArgs e) => AutoThem();
        public void AutoThem()
        {
            if (settings.ThemAuto)
            {
                if (DateTime.Now.Hour > 18)
                    SetNightThem();
                else
                    SetDayThem();
            }

            CheckArlam();
        }

        public void CheckArlam()
        {
            if (settings.Arlam)
            {
                if (DateTime.Now.Hour == settings.Hour &&
                    DateTime.Now.Minute == settings.Min)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        var win = new Arlam_Go();
                        win.ShowDialog();
                    });
                   
                }
            }
        }

            void Button_Setting_Click(object sender, RoutedEventArgs e) => new Setting(this).ShowDialog();
            void Button_Arlam_Click(object sender, RoutedEventArgs e) => new Arlam(this).ShowDialog();

            public void SaveSetting()
            {
                try
                {
                    using (FileStream fs = new FileStream(path_setting, FileMode.Truncate, FileAccess.Write))
                    { formatter.Serialize(fs, settings); }
                }
                catch (Exception)
                {
                    using (FileStream fs = new FileStream(path_setting, FileMode.OpenOrCreate))
                    { formatter.Serialize(fs, settings); }
                }

            }
            public void SetDayThem() => Application.Current.Resources.Source = them[0];
            public void SetNightThem() => Application.Current.Resources.Source = them[1];


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

