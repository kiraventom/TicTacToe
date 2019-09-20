using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeClassLibrary
{
    /// <summary>
    /// Class that represents game instance.
    /// </summary>
    public sealed class Game
    {
        /// <summary>
        /// Creates new game instance with specified parameters.
        /// </summary>
        /// <param name="gameParams"> Game parameters; cannot be null. </param>
        public Game(GameParams gameParams)
        {
            if (gameParams is null)
            {
                throw new ArgumentNullException(nameof(gameParams));
            }

            Player1 = gameParams.Player1;
            Player2 = gameParams.Player2;
            Board = new Board(gameParams.BoardSize);
            CurrentMode = gameParams.GameMode;
        }

        /// <summary>
        /// Type of mark.
        /// </summary>
        public enum MarkType { X, O }

        /// <summary>
        /// Game mode.
        /// </summary>
        public enum Mode { UserVsComputer, ComputerVsComputer, UserVsUser }

        /// <summary>
        /// The first player.
        /// </summary>
        public Player Player1 { get; }

        /// <summary>
        /// The second player.
        /// </summary>
        public Player Player2 { get; }

        /// <summary>
        /// Board associated to this game instance.
        /// </summary>
        public Board Board { get; }
        
        /// <summary>
        /// Mode of this game.
        /// </summary>
        public Mode CurrentMode { get; }
    }
}
