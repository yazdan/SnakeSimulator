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
        List<Point> foods;

        List<Point> snake;

        List<Point> moves;

        public SnakeGame()
        {
            moves = new List<Point>();
            moves.Add(new Point( 0, 1));
            moves.Add(new Point(-1, 0));
            moves.Add(new Point( 0,-1));
            moves.Add(new Point( 1, 0));
            snake = new List<Point>();
            snake.Add(new Point(2, 0));
            snake.Add(new Point(1, 0));
            snake.Add(new Point(0, 0));
        }
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
                    MoveSnke(SnakeMoves.Right);
                    break;
                case "l":
                    MoveSnke(SnakeMoves.Left);
                    break;
                case "s":
                    MoveSnke(SnakeMoves.Straight);
                    break;
                default:
                    outPutStr = INVALID_INPUT;
                    break;
            }

            if (checkForCollision())
            {
                outPutStr = INVALID_INPUT;
            }
            else
            {
                outPutStr = getFoodStr();
            }
            return outPutStr;
        }

        public string getFirstOutput()
        {
            return BoardSize.X.ToString() + " " + BoardSize.Y.ToString();
        }

        public string getSecondOutput()
        {
            return getFoodStr();
        }

        string getFoodStr()
        {
            return  string.Format("0 {0} {1}", foods[_currentFoodIndex].X, foods[_currentFoodIndex].Y);
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

            int moveIndex = moves.IndexOf(MoveDir);

            switch (mv)
            {
                case SnakeMoves.Left:
                    MoveDir = moves[(moveIndex - 1 + moves.Count) % moves.Count];
                    break;
                case SnakeMoves.Right:
                    MoveDir = moves[(moveIndex + 1) % moves.Count];
                    break;
                case SnakeMoves.Straight:
                    break;
            }

            Point nextHead = new Point(snake[0].X + MoveDir.X, snake[0].Y + MoveDir.Y);

            if (nextHead.X < 0)
                nextHead.X += BoardSize.X;

            if (nextHead.Y < 0)
                nextHead.Y += BoardSize.Y;

            if (nextHead != foods[_currentFoodIndex])
            {
                snake.RemoveAt(snake.Count - 1);
            }
            else
            {
                _currentFoodIndex++;
            }

            snake.Insert(0,nextHead);
            
        }

        public void makeRandomGame(int xMax, int yMax)
        {
            Random rnd = new Random();

            BoardSize = new Point(rnd.Next(3, xMax), rnd.Next(3, yMax));

            int foodCount = rnd.Next(1,BoardSize.X * BoardSize.Y);

            foods = new List<Point>(foodCount);

            for (int i = 0; i < foodCount; i++)
            {
                foods.Add(new Point(rnd.Next(BoardSize.X), rnd.Next(BoardSize.Y)));
            }
        }
    }
}
