﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp3.Model
{
    [Serializable]
    public class Settings
    {
        public Settings()
        {
                       
            FullFormatTime = true;
            Autorun = false;
            ThemNight = false;
            ThemAuto = false;
            if (ThemAuto)
                ThemNightIsEnable = false;
            else
                ThemNightIsEnable = true;
            TypeMonth = false;
        }

        public Color ColorTime { get; set; }
        public Color ColorDate { get; set; }
        public bool FullFormatTime { get; set; }
        public bool Autorun { get; set; }
        public bool ThemAuto { get; set; }
        public bool ThemNightIsEnable { get; set; }
        public bool ThemNight { get; set; }
        public bool TypeMonth { get; set; }
        
    }
}