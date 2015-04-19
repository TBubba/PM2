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
        internal Vector2 StartPosition;
        internal float StartRotation;
        internal Vector2 StartVelocity;

        internal BaseEntity Entity; // What entity this body belongs to

        // Constructor(s)
        internal BodyData()
        {
            StartPosition = new Vector2();
            StartRotation = 0f;
            StartVelocity = new Vector2();
            Entity = null;
        }
        internal BodyData(BodyData data)
        {
            StartPosition = data.StartPosition;
            StartRotation = data.StartRotation;
            StartVelocity = data.StartVelocity;
            Entity = data.Entity;
        }
    }
}
