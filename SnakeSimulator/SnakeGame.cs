using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SnakeSimulator
{
    class SnakeGame : IGame
    {
        enum SnakeMoves 
        {
            Straight,
            Left,
            Right
        }
        const string INVALID_INPUT = "1 0 0";
        Point _boardSize;

        public Point BoardSize
        {
            get { return _boardSize; }
            set { _boardSize = value; }
        }


        int _currentFoodIndex;
        ArrayList foods;

        List<Point> snake;


        public bool isGameEnded()
        {
            if (_currentFoodIndex >= foods.Count)
                return true;

            if (checkForCollision())
                return true;

            return false;
        }

        public string processInput(string inStr)
        {
            var outPutStr = INVALID_INPUT;

            switch (inStr.ToLower())
            {
                case "r":
                    break;
                case "l":
                    break;
                case "s":
                    break;
                default:
                    outPutStr = INVALID_INPUT;
                    break;
            }

            return outPutStr;
        }

        public string getFirstOutput()
        {
            return BoardSize.X.ToString() + " " + BoardSize.Y.ToString();
        }


        bool checkForCollision()
        {

            int cnt = 0;

            foreach (var item in snake)
            {
                if ((Point)item == (Point)snake[0])
                    cnt++;

                if (cnt >= 2)
                    return true;
            }
            return false;
        }

        void MoveSnke(SnakeMoves mv)
        {
            Point MoveDir = new Point(snake[0].X - snake[1].X, snake[0].Y - snake[1].Y);
                
            

        }
    }
}
