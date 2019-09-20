using System;

namespace TicTacToeClassLibrary
{
    /// <summary>
    /// Class that represents parameters for game.
    /// </summary>
    public sealed class GameParams
    {
        /// <summary>
        /// Creates new set of parameters.
        /// </summary>
        /// <param name="boardSize"> Amount of fields on one side of the square board. </param>
        /// <param name="gameMode"> Preferred game mode. </param>
        /// <param name="userMark"> Mark chosen for user. If game mode is 'User Vs Computer', it must have value. </param>
        public GameParams(int boardSize, Game.Mode gameMode, Game.MarkType? userMark)
        {
            BoardSize = boardSize;
            switch (gameMode)
            {
                case Game.Mode.UserVsComputer:
                    if (userMark is null)
                        throw new ArgumentNullException(nameof(userMark), Properties.Resources.UserMarkNotDefined);
                    Player1 = new User(userMark.Value);
                    Player2 = new Computer(userMark.Value == Game.MarkType.X ? Game.MarkType.O : Game.MarkType.X);
                    break;
                case Game.Mode.ComputerVsComputer:
                    Player1 = new Computer(Game.MarkType.X);
                    Player2 = new Computer(Game.MarkType.O);
                    break;
                case Game.Mode.UserVsUser:
                    Player1 = new User(Game.MarkType.X);
                    Player2 = new User(Game.MarkType.O);
                    break;
            }
        }

        /// <summary>
        /// Amount of fields on one side of the square board.
        /// </summary>
        public int BoardSize { get; }

        /// <summary>
        /// Chosen game mode.
        /// </summary>
        public Game.Mode GameMode { get; }

        /// <summary>
        /// THe first player.
        /// </summary>
        public Player Player1 { get; }

        /// <summary>
        /// The second player.
        /// </summary>
        public Player Player2 { get; }
    }
}
