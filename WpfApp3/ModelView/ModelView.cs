using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;

namespace WpfApp3
{
    public enum Month
    { январь =1, февраль, март, апрель, май, июнь, июль, август, сентябрь, октябрь, ноябрь, декабрь }
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

       
        private void SetShortFormatHorse()
        {
            if (DateTime.Now.Hour > 12)
            { TimeH = (DateTime.Now.Hour - 12).ToString(); }
            else
                TimeH = DateTime.Now.Hour.ToString();
        }
        private void SetMothFoTXT()
        {
            switch (DateTime.Now.Month)
            {
                case 1:DataM = Month.январь.ToString();break;
                case 2:DataM = Month.февраль.ToString();break;
                case 3:DataM = Month.март.ToString();break;
                case 4:DataM = Month.апрель.ToString();break;
                case 5:DataM = Month.май.ToString();break;
                case 6:DataM = Month.июнь.ToString();break;
                case 7:DataM = Month.июль.ToString();break;
                case 8:DataM = Month.август.ToString();break;
                case 9:DataM = Month.сентябрь.ToString();break;
                case 10:DataM = Month.октябрь.ToString();break;
                case 11:DataM = Month.ноябрь.ToString();break;
                case 12:DataM = Month.декабрь.ToString();break;
                default: DataM = DateTime.Now.Month.ToString(); break;
            }
        }

        public void InitializeComponen()
        {
            Delimiter = ":";
            if (MainWindow.settings.FullFormatTime)
                TimeH = DateTime.Now.Hour.ToString();
            else
                SetShortFormatHorse();

                TimeM = DateTime.Now.Minute.ToString();
            DataD = DateTime.Now.Day.ToString();
            if (!MainWindow.settings.TypeMonth)
                DataM = DateTime.Now.Month.ToString();
            else
                SetMothFoTXT();
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