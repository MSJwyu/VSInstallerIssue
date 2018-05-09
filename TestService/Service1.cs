using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestService
{
    public partial class Service1 : ServiceBase
    {
        bool _running;
        string _logPath;
        public Service1()
        {
            InitializeComponent();
            _logPath = AppDomain.CurrentDomain.BaseDirectory;
        }

        protected override void OnStart(string[] args)
        {
            _running = true;
            WriteToLog("Started...");

            //while (true)
            //{
            //    WriteToLog("Tick");
            //    Thread.Sleep(5000);

            //    if (!_running)
            //        break;
            //}
        }

        protected override void OnStop()
        {
            _running = false;
            WriteToLog("Stopped...");
        }

        private void WriteToLog(string text)
        {
            System.IO.File.AppendAllText($"{_logPath}\\log.txt", $"[{DateTime.Now.ToShortDateString()} - {DateTime.Now.ToShortTimeString()}]:{text}\r\n");
        }
    }
}
