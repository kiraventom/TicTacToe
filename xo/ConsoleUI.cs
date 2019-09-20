namespace TicTacToe
{
    using System;
    using TicTacToeClassLibrary;

    /// <summary>
    /// Class containing methods to work w/ console.
    /// </summary>
    public static class ConsoleUI
    {
        static ConsoleUI()
        {
            Random = new Random();
        }

        /// <summary>
        /// Gets Random instance.
        /// </summary>
        public static Random Random { get; }

        /// <summary>
        /// Asks user should game restart or quit.
        /// </summary>
        /// <returns> Returns True if user want to quit, False is user wants to restart. </returns>
        public static bool ShouldGameQuit()
        {
            while (true)
            {
                var cki = System.Console.ReadKey();
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

        /// <summary>
        /// Asks user which game mode he prefers.
        /// </summary>
        /// <returns> Returns Game.Mode element according to user choice. </returns>
        public static Game.Mode ChooseGameMode()
        {
            Console.Clear();
            Console.WriteLine(xo.Properties.Resources.ChooseGameMode);
            Console.WriteLine(xo.Properties.Resources.GameModes);
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

        /// <summary>
        /// Asks user which mark he chooses.
        /// </summary>
        /// <returns> Returns Game.MarkType element accoring to user choice. </returns>
        public static Game.MarkType ChooseMark()
        {
            Console.Clear();
            Console.WriteLine(xo.Properties.Resources.ChooseMark);
            Console.WriteLine(xo.Properties.Resources.Marks);
            while (true)
            {
                var cki = Console.ReadKey();
                switch (cki.Key)
                {
                    case ConsoleKey.X:
                        return Game.MarkType.X;
                    case ConsoleKey.O:
                        return Game.MarkType.O;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Clears the console and redraws tip, board and marks on board.
        /// </summary>
        /// <param name="board"> Board object, which is needed to set marks properly. </param>
        public static void Update(Board board)
        {
            Console.Clear();
            DrawTip();
            DrawBoard();
            DrawMarks(board);
        }

        /// <summary>
        /// Draws 3x3 board with numbered rows and columns and empty fields.
        /// </summary>
        public static void DrawBoard()
        {
#pragma warning disable CA1303 // Do not pass literals as localized parameters
            Console.WriteLine("    1   2   3");
            Console.WriteLine("  \u250c\u2500\u2500\u2500\u252c\u2500\u2500\u2500\u252c\u2500\u2500\u2500\u2510");
            Console.WriteLine($"1 \u2502   \u2502   \u2502   \u2502");
            Console.WriteLine("  \u251c\u2500\u2500\u2500\u253c\u2500\u2500\u2500\u253c\u2500\u2500\u2500\u2524");
            Console.WriteLine($"2 \u2502   \u2502   \u2502   \u2502");
            Console.WriteLine("  \u251c\u2500\u2500\u2500\u253c\u2500\u2500\u2500\u253c\u2500\u2500\u2500\u2524");
            Console.WriteLine($"3 \u2502   \u2502   \u2502   \u2502");
            Console.WriteLine("  \u2514\u2500\u2500\u2500\u2534\u2500\u2500\u2500\u2534\u2500\u2500\u2500\u2518");
#pragma warning restore CA1303 // Do not pass literals as localized parameters
        }

        /// <summary>
        /// Takes fields info from Board object and draws marks in a 3x3 board.
        /// </summary>
        /// <param name="board"> Board object, which is needed to set marks properly. </param>
        public static void DrawMarks(Board board)
        {
            if (board is null)
            {
                throw new ArgumentNullException(nameof(board));
            }

            string[,] marks = new string[board.Size, board.Size];

            for (int i = 0; i < board.Size; ++i)
            {
                for (int j = 0; j < board.Size; ++j)
                {
                    marks[i, j] = board.GetFields()[i, j].ToString();
                    System.Console.SetCursorPosition((j + 1) * 4, (int)((i + 1.5) * 2));
                    System.Console.Write(marks[i, j]);
                }
            }

            Console.SetCursorPosition(0, 9);
        }

        /// <summary>
        /// Draws a tip about controls.
        /// </summary>
        public static void DrawTip() => System.Console.WriteLine(xo.Properties.Resources.Tip);

        /// <summary>
        /// Asks user to choose the field he want to mark.
        /// </summary>
        /// <param name="board"> Board object, which is needed to set marks properly. </param>
        /// <returns> Returns a value tuple containing coords of chosen field. </returns>
        public static (int Row, int Column) ChooseFieldToMark(Board board)
        {
            (int left, int top) = (4, 3);
            Update(board);
            while (true)
            {
                DrawMarks(board);
                Console.SetCursorPosition(left, top);
                var cki = Console.ReadKey();
                switch (cki.Key)
                {
                    case ConsoleKey.RightArrow:
                        if (left < 12)
                        {
                            left += 4;
                        }

                        break;
                    case ConsoleKey.LeftArrow:
                        if (left > 4)
                        {
                            left -= 4;
                        }

                        break;
                    case ConsoleKey.UpArrow:
                        if (top > 3)
                        {
                            top -= 2;
                        }

                        break;
                    case ConsoleKey.DownArrow:
                        if (top < 7)
                        {
                            top += 2;
                        }

                        break;
                    case ConsoleKey.Enter:
                        return ((top - 3) / 2, (left / 4) - 1);
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Universal method to get coords from player.
        /// </summary>
        /// <param name="player"> Player meant to get coords from. </param>
        /// <param name="board"> Board object, which is needed to set marks properly. </param>
        /// <returns> Returns a value tuple containing coords of chosen field. </returns>
        public static (int row, int column) GetCoords(Player player, Board board)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            if (player is User)
            {
                return ChooseFieldToMark(board);
            }
            else
            {
                return (player as Computer).ChooseFieldToMark(board);
            }
        }

        /// <summary>
        /// Performs a new turn, updates turns counter and last set mark.
        /// </summary>
        /// <param name="game"> Game object, needed to pass players and board objects on. </param>
        /// <param name="turnsCounter"> Turns counter, needed to set to 0 before game started. </param>
        /// <param name="lastSetMark"> Last set mark, needed to set to MarkType.O before game started. </param>
        public static void NewTurn(Game game, ref int turnsCounter, ref Game.MarkType lastSetMark)
        {
            if (game is null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            if (lastSetMark == game.Player1.Mark)
            {
                while (true)
                {
                    var coords = ConsoleUI.GetCoords(game.Player2, game.Board);
                    if (game.Player2.TryMakeTurn(game.Board, coords))
                    {
                        break;
                    }
                }

                lastSetMark = game.Player2.Mark;
            }
            else
            {
                while (true)
                {
                    var coords = ConsoleUI.GetCoords(game.Player1, game.Board);
                    if (game.Player1.TryMakeTurn(game.Board, coords))
                    {
                        break;
                    }
                }

                lastSetMark = game.Player1.Mark;
            }

            ++turnsCounter;
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="game"> Game object, needed to pass players and board objects on. </param>
        /// <returns> Returns True if user wants to quit, False if user wants to restart. </returns>
        public static bool Start(Game game)
        {
            if (game is null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            bool gameFinished = false;
            Game.MarkType lastSetMark = Game.MarkType.O;
            int turnsCounter = 0;

            while (true)
            {
                ConsoleUI.NewTurn(game, ref turnsCounter, ref lastSetMark);
                Update(game.Board);

                if (game.Board.IsThereWin())
                {
                    gameFinished = true;
                    Console.WriteLine($"{lastSetMark.ToString()} Won!");
                }
                else if (turnsCounter == 9)
                {
                    gameFinished = true;
                    Console.WriteLine(xo.Properties.Resources.Tie);
                }

                if (gameFinished)
                {
                    Console.WriteLine(xo.Properties.Resources.AfterGameOptions);
                    return ShouldGameQuit();
                }
            }
        }

        /*
        public int EnterBoardSize()
        {
            Console.Clear();
            Console.WriteLine("Choose board size, more than 1, less than 10:");
            while (true)
            {
                var cki = Console.ReadKey();
                if (int.TryParse(cki.KeyChar.ToString(), out int size))
                    return size;
                else
                    continue;
            }
        }
        */
    }
}
