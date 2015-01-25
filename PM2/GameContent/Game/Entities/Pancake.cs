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
using BubbasEngine.Engine.Physics.Factories;
using BubbasEngine.Engine.Physics.Dynamics;

namespace PM2.GameContent.Game.Entities
{
    internal class Pancake : BaseEntity
    {
        // Private
        private BCircleShape _shape;
        
        // Constructor(s)
        internal Pancake()
            : base()
        {
        }
        internal Pancake(BodyData data)
            : base(data)
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
            Vector2f pos = new Vector2f(Body.Position.X, Body.Position.Y);

            //
            _shape.Position = pos + GetWorld().Camera.RelativePosition();

            // Scale pancake
            _shape.Scale = new Vector2f(_shape.Scale.X,
                Math.Max(Math.Abs((pos.Y - GetWorld().Camera.Size.Y / 2f) / GetWorld().Camera.Size.Y * 0.45f), 0.02f));
        }
        internal override void OnRemoved()
        {
        }

        //
        internal override Body CreateBody(PhysicsWorld world, BodyData data)
        {
            return BodyFactory.CreateRectangle(world, 10f, 1f, 1f, data.Position);
        }
    }
}
