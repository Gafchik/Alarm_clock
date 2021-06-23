using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;

namespace WpfApp3
{
    internal class ModelView : INotifyPropertyChanged
    {
        Timer t; 
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged; // ивент обновления
        public void OnPropertyChanged([CallerMemberName] string prop = "")
           => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion

        public ModelView()
        {
            t = new Timer();
            InitializeComponen();
            t.Interval = 1000;
            t.Elapsed += T_Elapsed;
            t.Start();
        }

        private void T_Elapsed(object sender, ElapsedEventArgs e) => InitializeComponen();



        public void InitializeComponen()
        {
            Delimiter = ":";
            TimeH = DateTime.Now.Hour.ToString();
            TimeM = DateTime.Now.Minute.ToString();
            DataD = DateTime.Now.Day.ToString();
            DataM = DateTime.Now.Month.ToString();
            DataY = DateTime.Now.Year.ToString();
        }

        private string delimiter; // разделитель

        public string Delimiter
        {
            get { return delimiter; }
            set { delimiter = value; OnPropertyChanged("Delimiter"); }
        }

        private string timeH; // Часы

        public string TimeH
        {
            get { return timeH; }
            set { timeH = value; OnPropertyChanged("TimeH"); }
        }


        private string timeM; // МИнуты

        public string TimeM
        {
            get { return timeM; }
            set { timeM = value; OnPropertyChanged("TimeM"); }
        }
        private string dataD; // Дни

        public string DataD
        {
            get { return dataD; }
            set { dataD = value; OnPropertyChanged("DataD"); }
        }

        private string dataM; // месяца

        public string DataM
        {
            get { return dataM; }
            set { dataM = value; OnPropertyChanged("DataM"); }
        }

        private string dataY; // год

        public string DataY
        {
            get { return dataY; }
            set { dataY = value; OnPropertyChanged("DataY"); }
        }





       
    }
}