using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.GameWorlds;
using SFML.Window;

namespace PM2.GameContent.Game
{
    internal class PanWorld : GameWorld
    {
        // Private
        private WorldCamera _camera;

        // Internal
        internal WorldCamera Camera
        { get { return _camera; } }

        // Constructor(s)
        internal PanWorld()
        {
            _camera = new WorldCamera();
        }
    }
}
