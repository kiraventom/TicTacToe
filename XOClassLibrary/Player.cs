using System;

namespace XOClassLibrary
{
    public abstract class Player
    {
        public Player(Game.MarkType mark)
        {
            Mark = mark;
        }

        public abstract bool TryMakeTurn(Board board, (int row, int column) coords);

        public Game.MarkType Mark { get; }
    }

    public sealed class User : Player
    {
        public User(Game.MarkType mark) : base(mark) { }

        public override bool TryMakeTurn(Board board, (int row, int column) coords)
        {
            while (true)
            {
                bool result = board.Fields[coords.row, coords.column].TrySetMark(this.Mark);
                return result;
            }
        }
    }

    public sealed class Computer : Player
    {
        public Computer(Game.MarkType mark) : base(mark) { }

        public override bool TryMakeTurn(Board board, (int row, int column) coords)
        {
            bool result = board.Fields[coords.row, coords.column].TrySetMark(this.Mark);
            return result;
        }
    }
}
