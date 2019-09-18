using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XOClassLibrary;

namespace xo
{
    class Program
    {
        public static Random Random { get; } = new Random();
        public static Game Game { get; set; }

        static void Main(string[] args)
        {
            while (true)
            {
                var boardSize = 3; // EnterBoardSize();
                var userMark = ChooseMark();

                Game = new Game(new GameParams(boardSize, userMark));

                Game.MarkType lastSetMark = Game.MarkType.O;
                int turnsCounter = 0;
                if (Game.User.Mark == Game.MarkType.X)
                {
                    lastSetMark = UserTurn();
                    Update();
                    ++turnsCounter;
                }

                while (true)
                {
                    if (lastSetMark == Game.User.Mark)
                        lastSetMark = ComputerTurn();
                    else
                        lastSetMark = UserTurn();

                    Update();
                    ++turnsCounter;
                    if (Game.Board.IsThereWin())
                    {
                        DrawBoard();
                        DrawMarks();
                        if (lastSetMark == Game.User.Mark)
                            Console.WriteLine("You Won!");
                        else
                            Console.WriteLine("Computer Won!");


                        Console.WriteLine("[R]estart [Q]uit");
                        while (true)
                        {
                            var cki = Console.ReadKey();
                            if (cki.Key == ConsoleKey.R)
                                break;
                            else if (cki.Key == ConsoleKey.Q)
                                return;
                            else
                                continue;
                        }
                        break;
                    }
                    else
                    {
                        if (turnsCounter == 9)
                        {
                            Console.WriteLine("Tie!");
                            Console.ReadLine();
                            break;
                        }
                    }
                }
            }
        }

        public static Game.MarkType UserTurn()
        {
            while (true)
            {
                (int Row, int Column) field = EnterFieldToMark();
                bool result = Game.Board.Fields[field.Row, field.Column].TrySetMark(Game.User.Mark);
                if (result)
                    return Game.User.Mark;
                else
                    continue;
            }
        }

        public static Game.MarkType ComputerTurn()
        {
            while (true)
            {
                int row = Random.Next(Game.Board.Size);
                int column = Random.Next(Game.Board.Size);
                bool result = Game.Board.Fields[row, column].TrySetMark(Game.Computer.Mark);
                if (result)
                    return Game.Computer.Mark;
                else
                    continue;
            }
        }

        public static Game.MarkType ChooseMark()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose your mark:");
                Console.WriteLine("X\t O");
                var cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.X)
                    return Game.MarkType.X;
                else if (cki.Key == ConsoleKey.O)
                    return Game.MarkType.O;
                else
                    continue;
            }
        }

        public static int EnterBoardSize()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose board size, more than 1, less than 10:");
                var cki = Console.ReadKey();
                if (int.TryParse(cki.KeyChar.ToString(), out int size))
                    return size;
                else
                    continue;
            }
        }

        public static void Update()
        {
            DrawBoard();
            DrawMarks();
            DrawTip();
        }

        public static void DrawBoard()
        {
            Console.Clear();
            Console.WriteLine("    1   2   3");
            Console.WriteLine("  \u250c\u2500\u2500\u2500\u252c\u2500\u2500\u2500\u252c\u2500\u2500\u2500\u2510");
            Console.WriteLine($"1 \u2502   \u2502   \u2502   \u2502");
            Console.WriteLine("  \u251c\u2500\u2500\u2500\u253c\u2500\u2500\u2500\u253c\u2500\u2500\u2500\u2524");
            Console.WriteLine($"2 \u2502   \u2502   \u2502   \u2502");
            Console.WriteLine("  \u251c\u2500\u2500\u2500\u253c\u2500\u2500\u2500\u253c\u2500\u2500\u2500\u2524");
            Console.WriteLine($"3 \u2502   \u2502   \u2502   \u2502");
            Console.WriteLine("  \u2514\u2500\u2500\u2500\u2534\u2500\u2500\u2500\u2534\u2500\u2500\u2500\u2518");
        }

        public static void DrawMarks()
        {
            string[,] marks = new string[Game.Board.Size, Game.Board.Size];
            for (int i = 0; i < Game.Board.Size; ++i)
            {
                for (int j = 0; j < Game.Board.Size; ++j)
                {
                    marks[i, j] = Game.Board.Fields[i, j].ToString();
                    Console.SetCursorPosition((j + 1) * 4 , (i + 1) * 2);
                    Console.Write(marks[i, j]);
                }
            }
            Console.SetCursorPosition(0, 8);
        }

        public static void DrawTip()
        {
            Console.WriteLine();
            Console.WriteLine("Use arrows to move cursor, press Enter to enter mark");
        }

        public static (int Row, int Column) EnterFieldToMark()
        {
            (int left, int top) currentPosition = (4, 2);
            Update();
            while (true)
            {
                DrawMarks();
                Console.SetCursorPosition(currentPosition.left, currentPosition.top);
                var cki = Console.ReadKey();
                switch (cki.Key)
                {
                    case ConsoleKey.RightArrow:
                        if (currentPosition.left < 12)
                            currentPosition.left += 4;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (currentPosition.left > 4)
                            currentPosition.left -= 4;
                        break;
                    case ConsoleKey.UpArrow:
                        if (currentPosition.top > 2)
                            currentPosition.top -= 2;
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentPosition.top < 6)
                            currentPosition.top += 2;
                        break;
                    case ConsoleKey.Enter:
                        return ((currentPosition.top / 2) - 1, (currentPosition.left / 4) - 1);
                    default:
                        break;

                }
            }
        }
    }
}
