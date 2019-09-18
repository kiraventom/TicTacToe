using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOClassLibrary
{
    public sealed class Game
    {
        public Game(GameParams gameParams)
        {
            User = new Player(gameParams.UserMark);
            Computer = new Player(gameParams.ComputerMark);
            Board = new Board(gameParams.BoardSize);
            CurrentState = GameState.Waiting;
        }

        //public void Start()
        //{
        //    CurrentState = GameState.Playing;
        //    while (true)
        //    {

        //        break;
        //    }
        //    CurrentState = GameState.Waiting;
        //}

        private enum GameState { Waiting, Playing }
        public enum MarkType { X = 0, O }

        public Player User { get; }
        public Player Computer { get; }
        public Board Board { get; }
        private GameState CurrentState { get; set; }
    }
}
