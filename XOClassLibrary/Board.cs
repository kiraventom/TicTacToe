using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToeClassLibrary
{
    /// <summary>
    /// Class that represents game board.
    /// </summary>
    public sealed class Board
    {
        /// <summary>
        /// Creates new square board.
        /// </summary>
        /// <param name="size"> Amount of fields on one side, should be more than 2 and less than 10. </param>
        public Board(int size)
        {
            if (size < 3 || size > 10)
                throw new ArgumentOutOfRangeException(nameof(size));
            else
            {
                Size = size;
                _fields = CreateFields(Size);
            }
        }

        /// <summary>
        /// Looks for a row, column or diagonal, containing same marks.
        /// </summary>
        /// <returns> 
        /// Returns True if there is row, column or diagonal, containing same marks, or False, if there is no any of there. 
        /// </returns>
        public bool IsThereWin()
        {
            return
                GetFields().Cast<Field>().Any(f =>
                    f.Mark != null &&
                    f.GetAdjacent(this).Any(n =>
                        n.Mark == f.Mark &&
                        n.GetAdjacent(this).Any(nn =>
                            nn.Mark == n.Mark &&
                            (f.Coords.Row - n.Coords.Row) == (n.Coords.Row - nn.Coords.Row) &&
                            (f.Coords.Column - n.Coords.Column) == (n.Coords.Column - nn.Coords.Column))));
        }

        /// <summary>
        /// Creates a square array of fields.
        /// </summary>
        /// <param name="size"> Size of one side of square array. </param>
        /// <returns> Returns a square array of fields. </returns>
        private Field[,] CreateFields(int size)
        {
            Field[,] fields = new Field[size, size];
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    fields[i, j] = new Field(i, j);
                }
            }
            return fields;
        }

        /// <summary>
        /// Class field containing fields of the board.
        /// </summary>
        private readonly Field[,] _fields;

        /// <summary>
        /// Returns fields of the board.
        /// </summary>
        /// <returns> Returns fields of the board. </returns>
        public Field[,] GetFields() => this._fields;

        /// <summary>
        /// Amount of fields on one side of the board. 
        /// </summary>
        public int Size { get; }
    }
}
