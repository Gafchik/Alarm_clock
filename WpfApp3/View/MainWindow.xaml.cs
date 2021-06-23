using System;
using System.Collections.Generic;
using System.IO;
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
        
        XmlSerializer formatter = new XmlSerializer(typeof(Settings));
        public static Settings settings = new Settings();
        List<string> them;
        public string path_setting = Environment.CurrentDirectory;
        public MainWindow()
        {
            path_setting += "\\setting.xml";
            var q = Directory.GetFiles(Environment.CurrentDirectory).ToList();
         // if (Directory.GetFiles(Environment.CurrentDirectory).ToList().Exists(i => i == (Environment.CurrentDirectory+="\\setting.xml")))
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
                {  SaveSetting(); }
            }
            else
            {  SaveSetting();  }
           
           
            them = new List<string>() { "DayStyle", "NightStyle" };

            InitializeComponent();
            DataContext = new ModelView();
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
    }
}
