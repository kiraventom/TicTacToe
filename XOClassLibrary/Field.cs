using System;
using System.Collections.Generic;
using System.Linq;

namespace XOClassLibrary
{
    public sealed class Field
    {
        public Field(int row, int column)
        {
            Row = row;
            Column = column;
            Mark = null;
        }

        public bool TrySetMark(Game.MarkType mark)
        {
            if (Mark != null)
                return false;
            else
            {
                Mark = mark;
                return true;
            }
        }

        public IEnumerable<Field> GetNeighbours(Board board)
        {
            return (board.Fields.Cast<Field>().Where(field => 
                field.Coords != this.Coords &&
                Math.Abs(field.Row - this.Row) <= 1 && 
                Math.Abs(field.Column - this.Column) <= 1));
        }

        public override string ToString()
        {
            switch (this.Mark)
            {
                case null:
                    return " ";
                case Game.MarkType.X:
                    return "X";
                case Game.MarkType.O:
                    return "O";
                default:
                    return "error";
            }
        }

        private int Row { get; }
        private int Column { get; }
        public (int Row, int Column) Coords => (Row, Column);
        public Game.MarkType? Mark { get; private set; }

    }
}
