using System;
using System.Collections.Generic;
using System.Linq;

namespace XOClassLibrary
{
    public sealed class Board
    {
        public Board(int size)
        {
            if (size < 2 || size > 10)
                throw new ArgumentOutOfRangeException("board size");
            else
            {
                Size = size;
                Fields = CreateFields(Size);
            }
        }

        // TODO: event?
        public bool IsThereWin()
        {
            return 
                Fields.Cast<Field>().Any(f =>
                    f.Mark != null &&
                    f.GetNeighbours(this).Any(n =>
                        n.Mark == f.Mark &&
                        n.GetNeighbours(this).Any(nn =>
                            nn.Mark == n.Mark &&
                            (f.Coords.Row - n.Coords.Row) == (n.Coords.Row - nn.Coords.Row) &&
                            (f.Coords.Column - n.Coords.Column) == (n.Coords.Column - nn.Coords.Column))));
        }

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

        public Field[,] Fields { get; }
        public int Size { get; }
    }
}
