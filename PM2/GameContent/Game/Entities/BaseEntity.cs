using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.GameWorlds;
using BubbasEngine.Engine.Physics.Common;
using BubbasEngine.Engine.Content;
using BubbasEngine.Engine.Graphics;

namespace PM2.GameContent.Game.Entities
{
    internal class BaseEntity : GameObject
    {
        // Private
        protected Vector2 _position;

        // Internal
        internal Vector2 Position
        { get { return _position; } }

        // World
        internal PanWorld GetWorld()
        {
            return (PanWorld)_world;
        }

        // Content & Graphics
        internal virtual void GetContent(ContentManager content)
        {
        }
        internal virtual void AddDrawables(GraphicsRenderer graphics)
        {
        }
        internal virtual void RemoveDrawables(GraphicsRenderer graphics)
        {
        }
    }
}
