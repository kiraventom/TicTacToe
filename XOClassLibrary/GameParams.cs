using System;

namespace XOClassLibrary
{
    public sealed class GameParams
    {
        public GameParams(int boardSize, Game.Mode gameMode, Game.MarkType? userMark)
        {
            BoardSize = boardSize;
            switch (gameMode)
            {
                case Game.Mode.UserVsComputer:
                    if (userMark is null)
                        throw new ArgumentNullException("User mark must be defined if game mode is User vs Computer");
                    Player1 = new User(userMark.Value);
                    Player2 = new Computer(userMark.Value == Game.MarkType.X ? Game.MarkType.O : Game.MarkType.X);
                    break;
                case Game.Mode.ComputerVsComputer:
                    Player1 = new Computer(Game.MarkType.X);
                    Player2 = new Computer(Game.MarkType.O);
                    break;
                case Game.Mode.UserVsUser:
                    Player1 = new User(Game.MarkType.X);
                    Player2 = new User(Game.MarkType.O);
                    break;
            }
            //UserMark = mark;
            //ComputerMark = UserMark == Game.MarkType.X ? Game.MarkType.O : Game.MarkType.X;
        }

        public int BoardSize { get; }
        public Game.Mode GameMode { get; }
        public Player Player1 { get; }
        public Player Player2 { get; }
    }
}
