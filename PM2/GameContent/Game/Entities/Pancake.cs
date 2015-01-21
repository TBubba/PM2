using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.GameWorlds;
using BubbasEngine.Engine.GameWorlds.GameInterfaces;
using BubbasEngine.Engine.Physics.Common;
using BubbasEngine.Engine.Graphics.Drawables;
using BubbasEngine.Engine.Content;
using BubbasEngine.Engine.Graphics;
using BubbasEngine.Engine.Graphics.Drawables.Shapes;

namespace PM2.GameContent.Game.Entities
{
    internal class Pancake : GameObject, IGameCreated, IGameStep, IGameAnimate, IGameRemoved
    {
        // Private
        private BCircleShape _shape;
        
        // Constructor(s)
        internal Pancake(Vector2 pos)
        {
            _shape = new BCircleShape(35f);
        }

        // Content & Graphics
        internal void GetContent(ContentManager content)
        {
            // Create sprite
        }
        internal void AddDrawables(GraphicsRenderer graphics)
        {
            // Add drawables
            graphics.AddDrawable(_shape, 0);
        }

        //
        public void Created()
        {
        }
        public void Step()
        {
        }
        public void Animate(float delta)
        {
            // Size pancake
        }
        public void Removed()
        {
        }
    }
}
