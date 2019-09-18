using System;
using System.Linq;

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

        public (int row, int column) ChooseFieldToMark(Board board)
        {
            Field field;
            field = IsThereWayToWin(board);
            if (field != null)
            {
                return field.Coords;
            }
            else
            {
                field = IsThereWayToLose(board);
                if (field != null)
                {
                    return field.Coords;
                }
                else
                {
                    // temp

                    Random random = new Random();
                    int row = random.Next(board.Size);
                    int column = random.Next(board.Size);
                    return (row, column);
                }
            }
        }

        private Field IsThereWayToWin(Board board)
        {
            // look for xx- 
            Field field =
                 board.Fields.Cast<Field>().FirstOrDefault(f =>
                     f.Mark == null &&
                     f.GetNeighbours(board).Any(n =>
                        n.Mark == this.Mark &&
                        n.GetNeighbours(board).Any(nn =>
                            nn.Mark == n.Mark &&
                             (f.Coords.Row - n.Coords.Row) == (n.Coords.Row - nn.Coords.Row) &&
                             (f.Coords.Column - n.Coords.Column) == (n.Coords.Column - nn.Coords.Column))));
            if (field is null)
            {
                // look for x-x
                field =
                    board.Fields.Cast<Field>().FirstOrDefault(f =>
                     f.Mark == null &&
                     f.GetNeighbours(board).Any(n1 =>
                        n1.Mark == this.Mark &&
                        f.GetNeighbours(board).Any(n2 =>
                            n2.Mark == n1.Mark &&
                             (f.Coords.Row - n1.Coords.Row) == (n2.Coords.Row - f.Coords.Row) &&
                             (f.Coords.Column - n1.Coords.Column) == (n2.Coords.Column - f.Coords.Column))));
            }

            return field;
        }

        private Field IsThereWayToLose(Board board)
        {
            // look for oo- 
            Field field =
                 board.Fields.Cast<Field>().FirstOrDefault(f =>
                     f.Mark == null &&
                     f.GetNeighbours(board).Any(n =>
                        n.Mark != null &&
                        n.Mark != this.Mark &&
                        n.GetNeighbours(board).Any(nn =>
                            nn.Mark == n.Mark &&
                             (f.Coords.Row - n.Coords.Row) == (n.Coords.Row - nn.Coords.Row) &&
                             (f.Coords.Column - n.Coords.Column) == (n.Coords.Column - nn.Coords.Column))));
            if (field is null)
            {
                // look for o-o
                field =
                    board.Fields.Cast<Field>().FirstOrDefault(f =>
                     f.Mark == null &&
                     f.GetNeighbours(board).Any(n1 =>
                        n1.Mark != null &&
                        n1.Mark != this.Mark &&
                        f.GetNeighbours(board).Any(n2 =>
                            n2.Mark == n1.Mark &&
                             (f.Coords.Row - n1.Coords.Row) == (n2.Coords.Row - f.Coords.Row) &&
                             (f.Coords.Column - n1.Coords.Column) == (n2.Coords.Column - f.Coords.Column))));
            }

            return field;
        }
    }
}
