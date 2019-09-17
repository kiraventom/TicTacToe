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
            var boardSize = 3; // EnterBoardSize();
            var playerMark = ChooseMark();

            Game = new Game(new GameParams(boardSize, playerMark));

            if (Game.User.Mark == Game.MarkType.X)
            {
                while (true)
                {
                    DrawBoard();
                    PlayerTurn();
                    DrawBoard();
                    if (Game.Board.IsThereWin())
                    {
                        Console.WriteLine("You Won!");
                        Console.ReadLine();
                        break;
                    }
                    DrawBoard();
                    ComputerTurn();
                    DrawBoard();
                    if (Game.Board.IsThereWin())
                    {
                        Console.WriteLine("Computer Won!");
                        Console.ReadLine();
                        break;
                    }
                }
            }
            else
            {
                while (true)
                {
                    DrawBoard();
                    ComputerTurn();
                    DrawBoard();
                    if (Game.Board.IsThereWin())
                    {
                        Console.WriteLine("Computer Won!");
                        Console.ReadLine();
                        break;
                    }
                    DrawBoard();
                    PlayerTurn();
                    DrawBoard();
                    if (Game.Board.IsThereWin())
                    {
                        Console.WriteLine("You Won!");
                        Console.ReadLine();
                        break;
                    }
                }
            }
            
        }

        public static void PlayerTurn()
        {
            while (true)
            {
                var (Row, Column) = EnterFieldToMark();
                bool result = Game.Board.Fields[Row, Column].TrySetMark(Game.User.Mark);
                if (result)
                    break;
                else
                    continue;
            }
        }

        public static void ComputerTurn()
        {
            while (true)
            {
                int row = Random.Next(Game.Board.Size);
                int column = Random.Next(Game.Board.Size);
                bool result = Game.Board.Fields[row, column].TrySetMark(Game.Computer.Mark);
                if (result)
                    break;
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
                Console.WriteLine("1. X\t 2. O");
                var cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.X || cki.Key == ConsoleKey.D1)
                    return Game.MarkType.X;
                else if (cki.Key == ConsoleKey.O || cki.Key == ConsoleKey.D2)
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

        public static void DrawBoard()
        {
            string[,] marks = new string[Game.Board.Size, Game.Board.Size];
            for (int i = 0; i < Game.Board.Size; ++i)
            {
                for (int j = 0; j < Game.Board.Size; ++j)
                {
                    marks[i, j] = Game.Board.Fields[i, j].ToString();
                }
            }

            Console.Clear();
            Console.WriteLine("\u250c\u2500\u2500\u2500\u252c\u2500\u2500\u2500\u252c\u2500\u2500\u2500\u2510");
            Console.WriteLine($"\u2502 {marks[0, 0]} \u2502 {marks[0, 1]} \u2502 {marks[0, 2]} \u2502");
            Console.WriteLine("\u251c\u2500\u2500\u2500\u253c\u2500\u2500\u2500\u253c\u2500\u2500\u2500\u2524");
            Console.WriteLine($"\u2502 {marks[1, 0]} \u2502 {marks[1, 1]} \u2502 {marks[1, 2]} \u2502");
            Console.WriteLine("\u251c\u2500\u2500\u2500\u253c\u2500\u2500\u2500\u253c\u2500\u2500\u2500\u2524");
            Console.WriteLine($"\u2502 {marks[2, 0]} \u2502 {marks[2, 1]} \u2502 {marks[2, 2]} \u2502");
            Console.WriteLine("\u2514\u2500\u2500\u2500\u2534\u2500\u2500\u2500\u2534\u2500\u2500\u2500\u2518");
        }

        public static (int Row, int Column) EnterFieldToMark()
        {
            while (true)
            {
                Console.Clear();
                DrawBoard();
                Console.WriteLine("Enter row and column, separated with space:");
                var cki = Console.ReadLine();
                if (cki.Length == 3 &&
                    int.TryParse(cki[0].ToString(), out int row) &&
                    int.TryParse(cki[2].ToString(), out int column) &&
                    row >= 0 && row < Game.Board.Size &&
                    column >= 0 && column < Game.Board.Size)
                {
                    return (row, column);
                }
                else
                    continue;
            }
        }
    }
}
