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
        private string _logPath;
        private CancellationTokenSource cts = new CancellationTokenSource();
        private Task mainTask = null;
        private TimeSpan WaitAfterSuccessInterval = TimeSpan.FromSeconds(5);
        private TimeSpan WaitAfterErrorInterval = TimeSpan.FromSeconds(5);

        public Service1()
        {
            InitializeComponent();
            _logPath = AppDomain.CurrentDomain.BaseDirectory;
        }

        protected override void OnStart(string[] args)
        {
            mainTask = new Task(Poll, cts.Token, TaskCreationOptions.LongRunning);
            mainTask.Start();
        }

        protected override void OnStop()
        {
            cts.Cancel();
            mainTask.Wait();
        }

        private void Poll()
        {
            CancellationToken cancellation = cts.Token;
            TimeSpan interval = TimeSpan.Zero;
            while (!cancellation.WaitHandle.WaitOne(interval))
            {
                try
                {
                    WriteToLog("Tick");
                    if (cancellation.IsCancellationRequested)
                    {
                        break;
                    }
                    interval = WaitAfterSuccessInterval;
                }
                catch (Exception caught)
                {
                    // Log the exception.
                    WriteToLog($"Error: {caught}");
                    interval = WaitAfterErrorInterval;
                }
            }
        }

        private void WriteToLog(string text)
        {
            System.IO.File.AppendAllText($"{_logPath}\\log.txt", $"[{DateTime.Now.ToShortDateString()} - {DateTime.Now.ToShortTimeString()}]:{text}\r\n");
        }
    }
}
