namespace XOClassLibrary
{
    public sealed class GameParams
    {
        public GameParams(int boardSize, Game.MarkType mark)
        {
            BoardSize = boardSize;
            UserMark = mark;
            ComputerMark = UserMark == Game.MarkType.X ? Game.MarkType.O : Game.MarkType.X;
        }

        public int BoardSize { get; }
        public Game.MarkType UserMark { get; }
        public Game.MarkType ComputerMark { get; }
    }
}
