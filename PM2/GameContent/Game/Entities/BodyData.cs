using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.Physics.Common;

namespace PM2.GameContent.Game.Entities
{
    internal class BodyData
    {
        // Internal
        internal Vector2 Position;
        internal float Rotation;
        internal Vector2 Velocity;

        // Constructor(s)
        internal BodyData()
        {
            Position = new Vector2();
            Rotation = 0f;
            Velocity = new Vector2();
        }
        internal BodyData(BodyData data)
        {
            Position = data.Position;
            Rotation = data.Rotation;
            Velocity = data.Velocity;
        }
    }
}
