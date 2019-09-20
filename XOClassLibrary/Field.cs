using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToeClassLibrary
{
    /// <summary>
    /// Class that represents the field on the board.
    /// </summary>
    public sealed class Field
    {
        /// <summary>
        /// Creates new field with certain coords.
        /// </summary>
        /// <param name="row"> Zero-based row index. </param>
        /// <param name="column"> Zero-based column index. </param>
        public Field(int row, int column)
        {
            Row = row;
            Column = column;
            Mark = null;
        }

        /// <summary>
        /// Tries to mark field.
        /// </summary>
        /// <param name="mark"> Mark type. </param>
        /// <returns> Marks field and returns true, if field was empty; otherwise returns false. </returns>
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

        /// <summary>
        /// Gets adjacent fields.
        /// </summary>
        /// <param name="board"> Current board. </param>
        /// <returns> IEnumerable of fields, adjacent to this one. </returns>
        public IEnumerable<Field> GetAdjacent(Board board)
        {
            if (board is null)
            {
                throw new ArgumentNullException(nameof(board));
            }

            return (board.GetFields().Cast<Field>().Where(field =>
                field.Coords != this.Coords &&
                Math.Abs(field.Row - this.Row) <= 1 &&
                Math.Abs(field.Column - this.Column) <= 1));
        }

        /// <summary>
        /// Gets ratio of two fields positions.
        /// </summary>
        /// <param name="field1"> The first field. </param>
        /// <param name="field2"> The second field. </param>
        /// <returns> Returns value tuple of rows and columns differences. </returns>
        public static (int rowDiff, int columnDiff) GetRatio (Field field1, Field field2)
        {
            if (field1 is null)
            {
                throw new ArgumentNullException(nameof(field1));
            }

            if (field2 is null)
            {
                throw new ArgumentNullException(nameof(field2));
            }

            return (field1.Coords.Row - field2.Coords.Row, 
                    field1.Coords.Column - field2.Coords.Column);
        }

        /// <summary>
        /// Converts field mark value into sting representation.
        /// </summary>
        /// <returns> Returns "X" if field mark is X; "O" if field mark is O; " " (space) if field is not marked; otherwise returns "error". </returns>
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

        /// <summary>
        /// Zero-based row index of the field.
        /// </summary>
        private int Row { get; }

        /// <summary>
        /// Zero-based column index of the field.
        /// </summary>
        private int Column { get; }
        /// <summary>
        /// Coords of the field.
        /// </summary>
        public (int Row, int Column) Coords => (Row, Column);

        /// <summary>
        /// Mark of the field. Equals null if field is nt marked.
        /// </summary>
        public Game.MarkType? Mark { get; private set; }

    }
}
