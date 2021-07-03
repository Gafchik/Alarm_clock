using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ArlamAutoRun
{
    public partial class AutoRun : ServiceBase
    {
       
        private bool _active = false;
        public AutoRun()
        {
            InitializeComponent();
            CanStop = true;
            CanShutdown = true;
            CanPauseAndContinue = true;
            AutoLog = true;
           


        }

        protected override void OnStart(string[] args) =>Task.Run(()=> Go());
      

        protected override void OnStop() => _active = false;

        private void Go()
        {
            string _path = null;
            _active = true;
            lock (this)
            {                 
               while (_active)
                {
                    try
                    {
                       var _autorun_arr = Registry.CurrentUser.CreateSubKey("Software").
                           CreateSubKey("Microsoft").
                           CreateSubKey("Windows").
                           CreateSubKey("CurrentVersion").
                           CreateSubKey("Run").GetValueNames();
                        _autorun_arr.ToList().ForEach(i => EventLog.WriteEntry(i));

                            _path = Registry.CurrentUser.CreateSubKey("Software").
                           CreateSubKey("Microsoft").
                           CreateSubKey("Windows").
                           CreateSubKey("CurrentVersion").
                           CreateSubKey("Run").GetValue("Alarm_clock").ToString();

                        EventLog.WriteEntry("path "+_path);
                            
                     
                        

                        if (! Process.GetProcesses().ToList().Exists(i=> i.ProcessName == "Fucking Clock.exe"))
                        {
                            EventLog.WriteEntry("не нашли в процессах");
                            if (_path != null)
                            {

                                EventLog.WriteEntry("путь не нал");
                                Process.Start(_path.ToString());
                                EventLog.WriteEntry("запустили");
                            }
                        }
                        Thread.Sleep(300);
                    }
                    catch (Exception e) { EventLog.WriteEntry(e.Message); }
                   
                  
                    
                }
            }
        }
    }
}
