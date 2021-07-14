using System;

namespace GameCatalogue.Exceptions
{
    public class GameNotRegisteredException : Exception
    {
        public GameNotRegisteredException()
            : base("This game is not registered")
        { }
    }
}
