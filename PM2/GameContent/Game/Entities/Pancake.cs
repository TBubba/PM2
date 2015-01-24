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
using SFML.Graphics;
using SFML.Window;

namespace PM2.GameContent.Game.Entities
{
    internal class Pancake : BaseEntity
    {
        // Private
        private BCircleShape _shape;
        
        // Constructor(s)
        internal Pancake(Vector2f position)
            : base (position)
        {
        }

        // Content & Graphics
        internal override void GetContent(ContentManager content)
        {
            // Create pancake circle shape
            _shape = new BCircleShape(45f, 32);
            _shape.Position = GetWorld().Camera.Size / 2f;
            _shape.Origin = new Vector2f(_shape.Radius, _shape.Radius);
            _shape.FillColor = Color.Yellow;
        }
        internal override void RemoveContent(ContentManager content)
        {

        }
        internal override void AddDrawables(GraphicsRenderer graphics)
        {
            // Add drawables
            graphics.AddDrawable(_shape, 0);
        }
        internal override void RemoveDrawables(GraphicsRenderer graphics)
        {
            // Add drawables
            graphics.RemoveDrawable(_shape, 0);
        }

        //
        internal override void OnCreated()
        {
        }
        internal override void OnStep()
        {
        }
        internal override void OnAnimate(float delta)
        {
            //
            _shape.Position = new Vector2f(_position.X, _position.Y) + GetWorld().Camera.RelativePosition();

            // Scale pancake
            _shape.Scale = new Vector2f(_shape.Scale.X,
                Math.Max(Math.Abs((_position.Y - GetWorld().Camera.Size.Y / 2f) / GetWorld().Camera.Size.Y * 0.45f), 0.02f));
        }
        internal override void OnRemoved()
        {
        }
    }
}
