using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeSimulator
{
    interface IGame
    {
        bool isGameEnded();

        string processInput(string inStr);

        string getFirstOutput();
        string getSecondOutput();
    }
}
