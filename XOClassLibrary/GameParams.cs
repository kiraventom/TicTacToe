namespace XOClassLibrary
{
    public sealed class GameParams
    {
        public GameParams(int boardSize, Game.MarkType mark)
        {
            BoardSize = boardSize;
            PlayerMark = mark;
            ComputerMark = PlayerMark == Game.MarkType.X ? Game.MarkType.O : Game.MarkType.X;
        }

        public int BoardSize { get; }
        public Game.MarkType PlayerMark { get; }
        public Game.MarkType ComputerMark { get; }
    }
}
