using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3
{
   public class Arlam_ModelView : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged; // ивент обновления
        public void OnPropertyChanged([CallerMemberName] string prop = "")
           => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion
        public Arlam_ModelView()
        { InitializeComponen(); }
            

        
        public void InitializeComponen()
        {
            if(MainWindow.settings.Arlam)
            {
                TimeH = MainWindow.settings.Hour.ToString();
                TimeM = MainWindow.settings.Min.ToString();
            }
            else
            {
                TimeH = "Не";
                TimeM = "назначен";
            }
            
        }
        private string timeH; // Часы


        public string TimeH
        {
            get { return timeH; }
            set { timeH = value;  OnPropertyChanged("TimeH"); }
        }

        internal void SetArlam(int H, int M)
        {
            MainWindow.settings.Arlam = true;
            MainWindow.settings.Hour = H;
            MainWindow.settings.Min = M;
            TimeH = H.ToString();
            TimeM = M.ToString();
        }

        private string timeM; // МИнуты

       

        public string TimeM
        {
            get { return timeM; }
            set { timeM = value; OnPropertyChanged("TimeM"); }
        }
    }
}
