using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOClassLibrary
{
    public sealed class Game
    {
        public Game(GameParams gameParams)
        {
            Player1 = gameParams.Player1;
            Player2 = gameParams.Player2;
            Board = new Board(gameParams.BoardSize);
            CurrentMode = gameParams.GameMode;
        }

        public enum MarkType { X, O }
        public enum Mode { UserVsComputer, ComputerVsComputer, UserVsUser }

        public Player Player1 { get; }
        public Player Player2 { get; }
        public Board Board { get; }
        public Mode CurrentMode { get; }
    }
}
