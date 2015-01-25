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
using PM2.GameContent.Game.Drawables;

namespace PM2.GameContent.Game.Entities
{
    internal class Pancake : BaseEntity
    {
        // Private
        private BCircleShape _shape;
        private DrawableHitBox _hitbox;
        
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
            _shape.Position = GetWorld().Layer.View.Size / 2f;
            _shape.Origin = new Vector2f(_shape.Radius, _shape.Radius);
            _shape.FillColor = Color.Yellow;

            // Create pancake circle shape
            _hitbox = new DrawableHitBox();
            _hitbox.Shape.FillColor = new Color(Color.Red) { A = 125 };
        }
        internal override void RemoveContent(ContentManager content)
        {

        }
        internal override void AddDrawables(GraphicsLayer layer)
        {
            // Add drawables
            layer.Renderables.Add(_shape);
            layer.Renderables.Add(_hitbox.Shape);
        }
        internal override void RemoveDrawables(GraphicsLayer layer)
        {
            // Remove drawables
            layer.Renderables.Remove(_shape);
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
            Vector2f pos = new Vector2f(GetBody().Position.X, GetBody().Position.Y);

            // Position
            _shape.Position = pos;

            // Scale pancake
            _shape.Scale = new Vector2f(_shape.Scale.X,
                Math.Max(Math.Abs((pos.Y - GetWorld().Layer.View.Size.Y / 2f) / GetWorld().Layer.View.Size.Y * 0.45f), 0.03f));

            // Color
            float d = pos.Y / GetWorld().Layer.View.Size.Y;
            _shape.FillColor = new Color((byte)(d * 256 / 2 + 256 / 2), (byte)(d * 256 / 2 + 256 / 2), 0);

            // Hitbox
            _hitbox.SetShape(GetBody());
        }
        internal override void OnRemoved()
        {
        }

        //
        internal override Body CreateBody(PhysicsWorld world, BodyData data)
        {
            Body body = BodyFactory.CreateRectangle(world, 90f, 2f, 10f, data.Position);
            body.IsStatic = false;
            body.IsKinematic = false;
            body.FixedRotation = true;

            return body;
        }
    }
}
