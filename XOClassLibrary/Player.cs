using System;

namespace XOClassLibrary
{
    public sealed class Player
    {
        public Player(Game.MarkType mark)
        {
            Mark = mark;
        }

        public Game.MarkType Mark { get; }
    }
}
