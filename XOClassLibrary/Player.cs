using System;

namespace XOClassLibrary
{
    public abstract class Player
    {
        public Player(Game.MarkType mark)
        {
            Mark = mark;
        }

        public abstract bool TryMakeTurn(Game game, (int row, int column) coords);

        public Game.MarkType Mark { get; }
    }

    public sealed class User : Player
    {
        public User(Game.MarkType mark) : base(mark) { }

        public override bool TryMakeTurn(Game game, (int row, int column) coords)
        {
            while (true)
            {
                bool result = game.Board.Fields[coords.row, coords.column].TrySetMark(this.Mark);
                return result;
            }
        }
    }

    public sealed class Computer : Player
    {
        public Computer(Game.MarkType mark) : base(mark) { }

        public override bool TryMakeTurn(Game game, (int row, int column) coords)
        {
            bool result = game.Board.Fields[coords.row, coords.column].TrySetMark(this.Mark);
            return result;
        }
    }
}
