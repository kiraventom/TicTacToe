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
            XO xo = new XO();
            while (true)
            {
                var boardSize = 3; // EnterBoardSize();
                var gameMode = xo.ChooseGameMode();
                Game.MarkType? userMark = null;
                if (gameMode == Game.Mode.UserVsComputer)
                    userMark = xo.ChooseMark();

                Game = new Game(new GameParams(boardSize, gameMode, userMark));

                if (xo.Start(Game))
                    return;
            }
        }
    }

    public class XO
    {
        public Random Random { get; }

        public XO()
        {
            Random = new Random();
        }

        public bool Start(Game game)
        {
            bool gameFinished = false;
            Game.MarkType lastSetMark = Game.MarkType.O;
            int turnsCounter = 0;

            while (true)
            {
                NewTurn(game, ref turnsCounter, ref lastSetMark);
                Update(game.Board);

                if (game.Board.IsThereWin())
                {
                    gameFinished = true;
                    Console.WriteLine($"{lastSetMark.ToString()} Won!");
                }
                else if (turnsCounter == 9)
                {
                    gameFinished = true;
                    Console.WriteLine("Tie!");
                }

                if (gameFinished)
                {
                    Console.WriteLine("[R]estart [Q]uit");
                    return ShouldGameQuit();
                }  
            }
        }

        public bool ShouldGameQuit()
        {
            while (true)
            {
                var cki = Console.ReadKey();
                switch (cki.Key)
                {
                    case ConsoleKey.R:
                        return false;
                    case ConsoleKey.Q:
                        return true;
                    default:
                        break;
                }   
            }
        }

        public void NewTurn(Game game, ref int turnsCounter, ref Game.MarkType lastSetMark)
        {
            if (lastSetMark == game.Player1.Mark)
            {
                while (true)
                {
                    var coords = GetCoords(game.Player2, game.Board);
                    if (game.Player2.TryMakeTurn(game.Board, coords))
                        break;
                }
                lastSetMark = game.Player2.Mark;
            }
            else
            {
                while (true)
                {
                    var coords = GetCoords(game.Player1, game.Board);
                    if (game.Player1.TryMakeTurn(game.Board, coords))
                        break;
                }
                lastSetMark = game.Player1.Mark;
            }
            ++turnsCounter;
        }

        public (int row, int column) GetCoords(Player player, Board board)
        {
            if (player is User)
                return ChooseFieldToMark(board);
            else 
            {
                return (player as Computer).ChooseFieldToMark(board);
            }
        }

        public Game.Mode ChooseGameMode()
        {
            Console.Clear();
            Console.WriteLine("Choose game mode:");
            Console.WriteLine("User [V]s computer\t[C]omputer Vs computer\t[U]ser Vs user");
            while (true)
            {
                var cki = Console.ReadKey();
                switch (cki.Key)
                {
                    case ConsoleKey.V:
                        return Game.Mode.UserVsComputer;
                    case ConsoleKey.C:
                        return Game.Mode.ComputerVsComputer;
                    case ConsoleKey.U:
                        return Game.Mode.UserVsUser;
                    default:
                        break;
                }
            }
        }

        public Game.MarkType ChooseMark()
        {
            Console.Clear();
            Console.WriteLine("Choose your mark:");
            Console.WriteLine("X\t O");
            while (true)
            {
                var cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.X)
                    return Game.MarkType.X;
                else if (cki.Key == ConsoleKey.O)
                    return Game.MarkType.O;
                else
                    continue;
            }
        }

        //public int EnterBoardSize()
        //{
        //    Console.Clear();
        //    Console.WriteLine("Choose board size, more than 1, less than 10:");
        //    while (true)
        //    {
        //        var cki = Console.ReadKey();
        //        if (int.TryParse(cki.KeyChar.ToString(), out int size))
        //            return size;
        //        else
        //            continue;
        //    }
        //}

        public void Update(Board board)
        {
            Console.Clear();
            DrawTip();
            DrawBoard();
            DrawMarks(board);
        }

        public void DrawBoard()
        {
            Console.WriteLine("    1   2   3");
            Console.WriteLine("  \u250c\u2500\u2500\u2500\u252c\u2500\u2500\u2500\u252c\u2500\u2500\u2500\u2510");
            Console.WriteLine($"1 \u2502   \u2502   \u2502   \u2502");
            Console.WriteLine("  \u251c\u2500\u2500\u2500\u253c\u2500\u2500\u2500\u253c\u2500\u2500\u2500\u2524");
            Console.WriteLine($"2 \u2502   \u2502   \u2502   \u2502");
            Console.WriteLine("  \u251c\u2500\u2500\u2500\u253c\u2500\u2500\u2500\u253c\u2500\u2500\u2500\u2524");
            Console.WriteLine($"3 \u2502   \u2502   \u2502   \u2502");
            Console.WriteLine("  \u2514\u2500\u2500\u2500\u2534\u2500\u2500\u2500\u2534\u2500\u2500\u2500\u2518");
        }

        public void DrawMarks(Board board)
        {
            string[,] marks = new string[board.Size, board.Size];
            for (int i = 0; i < board.Size; ++i)
            {
                for (int j = 0; j < board.Size; ++j)
                {
                    marks[i, j] = board.Fields[i, j].ToString();
                    Console.SetCursorPosition((j + 1) * 4 , (int)((i + 1.5) * 2));
                    Console.Write(marks[i, j]);
                }
            }
            Console.SetCursorPosition(0, 9);
        }

        public void DrawTip()
        {
            Console.WriteLine("Use arrows to move cursor, press Enter to enter mark");
        }

        public (int Row, int Column) ChooseFieldToMark(Board board)
        {
            (int left, int top) currentPosition = (4, 3);
            Update(board);
            while (true)
            {
                DrawMarks(board);
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
                        if (currentPosition.top > 3)
                            currentPosition.top -= 2;
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentPosition.top < 7)
                            currentPosition.top += 2;
                        break;
                    case ConsoleKey.Enter:
                        return ((currentPosition.top - 3) / 2, (currentPosition.left / 4) - 1);
                    default:
                        break;

                }
            }
        }
    }
}
