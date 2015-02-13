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
using BubbasEngine.Engine;
using PM2.GameContent.Game.Drawables;

namespace PM2.GameContent.Game.Entities
{
    internal class Pancake : BaseEntity
    {
        // Private
        private DrawablePlate _shape;
        
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
            _shape = new DrawablePlate(45f);
            _shape.Color = Color.Yellow;

            // Create pancake circle shape
            _hitbox.FillColor = new Color(Color.Red) { A = 125 };
            _hitbox.Depth = -100;
        }
        internal override void RemoveContent(ContentManager content)
        {

        }
        internal override void AddDrawables(GraphicsLayer gameLayer, GraphicsLayer hitboxLayer)
        {
            // Add drawables
            gameLayer.Renderables.Add(_shape);

            //
            base.AddDrawables(gameLayer, hitboxLayer);
        }
        internal override void RemoveDrawables(GraphicsLayer gameLayer, GraphicsLayer hitboxLayer)
        {
            // Remove drawables
            gameLayer.Renderables.Remove(_shape);

            //
            base.RemoveDrawables(gameLayer, hitboxLayer);
        }

        //
        internal override void OnCreated()
        {
        }
        internal override void OnStep()
        {
            float mar = _shape.Radius * 2f;

            if (GetBody().Position.Y > GetWorld().WorldSize.Y + mar ||
                //GetBody().Position.Y < -mar ||
                GetBody().Position.X > GetWorld().WorldSize.X + mar ||
                GetBody().Position.X < -mar)
                GetWorld().Entities.Remove(this);
        }
        internal override void OnAnimate(float delta)
        {
            Vector2f pos = new Vector2f(GetBody().Position.X, GetBody().Position.Y) / new Vector2f(GetWorld().WorldSize.X, GetWorld().WorldSize.Y);
            float rot = GetBody().Rotation % ((float)Math.PI * 2f);
            if (rot < 0f)
                rot += (float)Math.PI * 2f;

            View view = GetWorld().MainLayer.GetView();

            //
            _shape.UpdateOrientation(pos, rot, view.Size);

            // Color
            float d = pos.Y / 1f;
            _shape.Color = new Color((byte)(d * 256 / 2 + 256 / 2), (byte)(d * 256 / 2 + 256 / 2), 0);

            // Hitbox
            _hitbox.SetShape(GetBody(), new Vector2(view.Size.X / GetWorld().WorldSize.X, view.Size.Y / GetWorld().WorldSize.Y));
        }
        internal override void OnRemoved()
        {
        }

        //
        internal override Body CreateBody(PhysicsWorld world, BodyData data)
        {
            Body body = BodyFactory.CreateRectangle(world, 7f, 0.1f, 1f, data.Position);
            body.IsStatic = false;
            body.IsKinematic = false;
            //body.FixedRotation = true;

            body.LinearDamping = 0.4f;
            body.AngularDamping = 0.1f;
            body.Friction = 0.2f;

            return body;
        }
    }
}
