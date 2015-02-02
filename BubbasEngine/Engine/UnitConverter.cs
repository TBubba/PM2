using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;
using BubbasEngine.Engine.Physics.Common;

namespace BubbasEngine.Engine
{
    public static class UnitConverter
    {
        //
        public static Vector2 ScreenToPhysics(Vector2f screenPos, Vector2i screenSize, Vector2f worldSize)
        {
            Vector2f v = screenPos * worldSize / new Vector2f(screenSize);
            return new Vector2(v.X, v.Y);
        }
    }
}
