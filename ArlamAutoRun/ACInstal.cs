﻿using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace ArlamAutoRun
{
    [RunInstaller(true)]
    public partial class ACInstal : System.Configuration.Install.Installer
    {
        ServiceInstaller _service_installer = new ServiceInstaller();
        ServiceProcessInstaller _process_installer = new ServiceProcessInstaller();
        public ACInstal()
        {
            InitializeComponent();
            //обьязательно !
            _process_installer.Account = ServiceAccount.LocalSystem; // тип учетной записи от которой запускать
            _service_installer.StartType = ServiceStartMode.Manual; // вид запуска 
            _service_installer.ServiceName = "Arlam"; // имя службы в спике
            _service_installer.Description = "запускает мой будильник"; // описание

            _service_installer.AfterInstall += _installer_AfterInstall; // после установки
            _service_installer.AfterRollback += _installer_AfterRollback; // после ошибки
            _service_installer.AfterUninstall += _installer_AfterUninstall; // после удаления

            Installers.Add(_service_installer);
            Installers.Add(_process_installer);
        }

        private void _installer_AfterUninstall(object sender, InstallEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Удалили", ConsoleColor.Blue);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void _installer_AfterRollback(object sender, InstallEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Не получилось");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void _installer_AfterInstall(object sender, InstallEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Установилось");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
