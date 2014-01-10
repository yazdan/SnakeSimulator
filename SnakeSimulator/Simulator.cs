using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SnakeSimulator
{
    class Simulator
    {
        string _execPath;

        public string ExecPath
        {
            get { return _execPath; }
            set { _execPath = value; }
        }


        IGame _gameInput;

        internal IGame GameInput
        {
            get { return _gameInput; }
            set { _gameInput = value; }
        }

        public StringBuilder logs = new StringBuilder();

        public void run(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int outInterval = 1000;
            int cnt = 0;

            if (GameInput == null)
                throw new Exception("Game must not be null");

            ProcessStartInfo startInfo = new ProcessStartInfo(ExecPath);

            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardInput = true;
            startInfo.UseShellExecute = false;

            startInfo.CreateNoWindow = true;
            startInfo.ErrorDialog = false;
            Process process = new Process();
            process.StartInfo = startInfo;

            worker.ReportProgress(0, "Process Created");
            process.Start();
            worker.ReportProgress(0, "Process Started");

            var first = GameInput.getFirstOutput();
            
            worker.ReportProgress(0, "Out: " + first);
            process.StandardInput.WriteLine(first);
            
            var second = GameInput.getSecondOutput();
            worker.ReportProgress(0, "Out: " + second);
            process.StandardInput.WriteLine(second);

            while (!GameInput.isGameEnded())
            {
                if (process.HasExited)
                {
                    worker.ReportProgress(0, "process exited abnormally");
                    break;
                }
                var outStr = process.StandardOutput.ReadLine();
                logs.AppendLine("In :" + outStr);
                
                var input = GameInput.processInput(outStr);
                logs.AppendLine("Out:" + input);
                process.StandardInput.WriteLine(input);

                if (cnt % outInterval == 0)
                {
                    worker.ReportProgress(0, logs.ToString());
                    logs.Clear();
                }
                cnt++;
            }
            
        }

        
    }
}
