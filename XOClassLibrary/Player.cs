using System;
using System.Linq;

namespace TicTacToeClassLibrary
{
    /// <summary>
    /// Abstract class that represents player.
    /// </summary>
    public abstract class Player
    {
        /// <summary>
        /// Creates new player and assigns it a mark.
        /// </summary>
        /// <param name="mark"> Mark that should be assigned to this player. </param>
        public Player(Game.MarkType mark)
        {
            Mark = mark;
        }

        /// <summary>
        /// Tries to make a turn on certain coords.
        /// </summary>
        /// <param name="board"> Current board. </param>
        /// <param name="coords"> Coords of a field player tries to mark. </param>
        /// <returns> Marks the field and returns True if it was marked succesfully; False if it was already marked. </returns>
        public bool TryMakeTurn(Board board, (int row, int column) coords)
        {
            if (board is null)
            {
                throw new ArgumentNullException(nameof(board));
            }

            return board.GetFields()[coords.row, coords.column].TrySetMark(this.Mark);
        }

        /// <summary>
        /// Mark assigned to this player.
        /// </summary>
        public Game.MarkType Mark { get; }
    }

    /// <summary>
    /// Class for human player.
    /// </summary>
    public sealed class User : Player
    {
        /// <summary>
        /// Creates new human player instance and assigns it certain mark.
        /// </summary>
        /// <param name="mark"> Mark that should be assigned to this player. </param>
        public User(Game.MarkType mark) : base(mark) { }
    }

    /// <summary>
    /// Class for computer player.
    /// </summary>
    public sealed class Computer : Player
    {
        /// <summary>
        /// Creates new computer player instance and assigns it certain mark.
        /// </summary>
        /// <param name="mark"> Mark that should be assigned to this player. </param>
        public Computer(Game.MarkType mark) : base(mark) { }

        /// <summary>
        /// Decides what field should be marked by computer.
        /// </summary>
        /// <param name="board"> Current board. </param>
        /// <returns> Returns coords of field that should be marked. </returns>
        public (int row, int column) ChooseFieldToMark(Board board)
        {
            if (board is null)
            {
                throw new ArgumentNullException(nameof(board));
            }

            Field field;
            field = IsThereWayToWin(board);
            if (field is null)
            {
                field = IsThereWayToLose(board);
                if (field is null)
                {
                    field = IsThereWayToWinInTwoTurns(board);
                    if (field is null)
                    {
                        // temp
                        Random random = new Random();
                        int row = random.Next(board.Size);
                        int column = random.Next(board.Size);
                        field = new Field(row, column);
                    }
                }
            }

            return field.Coords;
        }

        /// <summary>
        /// Looks for possibilty to win this turn.
        /// </summary>
        /// <param name="board"> Current board. </param>
        /// <returns> Returns field needed to be marked to win this turn; if there is no such one, returns null. </returns>
        private Field IsThereWayToWin(Board board)
        {
            // look for xx- 
            Field desired =
                 board.GetFields().Cast<Field>().FirstOrDefault(field =>
                     field.Mark == null &&
                     field.GetAdjacent(board).Any(adjacent =>
                        adjacent.Mark == this.Mark &&
                        adjacent.GetAdjacent(board).Any(adjacentOfAdjacent =>
                            adjacentOfAdjacent.Mark == adjacent.Mark &&
                            Field.GetRatio(field, adjacent) == Field.GetRatio(adjacent, adjacentOfAdjacent))));

            if (desired is null)
            {
                // look for x-x
                desired =
                    board.GetFields().Cast<Field>().FirstOrDefault(field =>
                     field.Mark == null &&
                     field.GetAdjacent(board).Any(adjacent1 =>
                        adjacent1.Mark == this.Mark &&
                        field.GetAdjacent(board).Any(adjacent2 =>
                            adjacent2.Mark == adjacent1.Mark &&
                            Field.GetRatio(field, adjacent1) == Field.GetRatio(adjacent2, field))));
            }

            return desired;
        }

        /// <summary>
        /// Looks for possibilty to lose next turn.
        /// </summary>
        /// <param name="board"> Current board. </param>
        /// <returns> Returns field needed to be marked not to lose next turn; if there is no such one, returns null. </returns>
        private Field IsThereWayToLose(Board board)
        {
            // look for oo- 
            Field desired =
                 board.GetFields().Cast<Field>().FirstOrDefault(field =>
                     field.Mark == null &&
                     field.GetAdjacent(board).Any(adjacent =>
                        adjacent.Mark != null &&
                        adjacent.Mark != this.Mark &&
                        adjacent.GetAdjacent(board).Any(adjacentOfAdjacent =>
                            adjacentOfAdjacent.Mark == adjacent.Mark &&
                            Field.GetRatio(field, adjacent) == Field.GetRatio(adjacent, adjacentOfAdjacent))));
            if (desired is null)
            {
                // look for o-o
                desired =
                    board.GetFields().Cast<Field>().FirstOrDefault(field =>
                     field.Mark == null &&
                     field.GetAdjacent(board).Any(adjacent1 =>
                        adjacent1.Mark != null &&
                        adjacent1.Mark != this.Mark &&
                        field.GetAdjacent(board).Any(adjacent2 =>
                            adjacent2.Mark == adjacent1.Mark &&
                            Field.GetRatio(field, adjacent1) == Field.GetRatio(adjacent2, field))));
            }

            return desired;
        }

        /// <summary>
        /// Looks for possibilty to win next turn.
        /// </summary>
        /// <param name="board"> Current board. </param>
        /// <returns> Returns field needed to be marked to win next turn; if there is no such one, returns null. </returns>
        private Field IsThereWayToWinInTwoTurns(Board board)
        {
            // look for x-- 
            Field desired =
                 board.GetFields().Cast<Field>().FirstOrDefault(field =>
                     field.Mark == null &&
                     field.GetAdjacent(board).Any(adjacent1 =>
                        adjacent1.Mark == this.Mark &&
                        field.GetAdjacent(board).Any(adjacent2 =>
                            adjacent2.Mark == null &&
                            Field.GetRatio(field, adjacent1) == Field.GetRatio(adjacent2, field))));
            if (desired is null)
            {
                // look for -x-
                desired =
                    board.GetFields().Cast<Field>().FirstOrDefault(field =>
                     field.Mark == null &&
                     field.GetAdjacent(board).Any(adjacent =>
                        adjacent.Mark == this.Mark &&
                        adjacent.GetAdjacent(board).Any(adjacentOfAdjacent =>
                            adjacentOfAdjacent.Mark == null &&
                             Field.GetRatio(field, adjacent) == Field.GetRatio(adjacent, adjacentOfAdjacent))));
            }

            return desired;
        }
    }
}
