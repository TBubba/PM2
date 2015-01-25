using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PM2.GameContent.Game
{
    internal class PanGameArgs
    {
        // Internal
        internal int GameMode;
        internal int Players;
        internal int Room;

        internal float StepTime;

        // Constructor(s)
        internal PanGameArgs()
        {
            GameMode = 0;
            Players = 1;
            Room = 0;

            StepTime = 1f / 60f;
        }
        internal PanGameArgs(PanGameArgs args)
        {
            // Copy parameter args
            GameMode = args.GameMode;
            Players = args.Players;
            Room = args.Room;

            StepTime = args.StepTime;
        }
    }
}
