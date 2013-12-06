using System;
using System.Collections.Generic;
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

        bool run()
        {

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

            process.Start();

            process.StandardInput.WriteLine(GameInput.getFirstOutput());

            while (!GameInput.isGameEnded())
            {
                var outStr = process.StandardOutput.ReadLine();

                process.StandardInput.WriteLine(GameInput.processInput(outStr));
            }
            return true;
        }
    }
}
