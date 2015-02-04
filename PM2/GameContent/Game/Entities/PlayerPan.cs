using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.Graphics.Drawables.Shapes;
using SFML.Window;
using BubbasEngine.Engine.Content;
using BubbasEngine.Engine.Graphics;
using SFML.Graphics;
using BubbasEngine.Engine.Physics.Common;
using BubbasEngine.Engine.Physics.Dynamics;
using BubbasEngine.Engine.Physics.Factories;
using BubbasEngine.Engine.Physics.Collision.Shapes;
using PM2.GameContent.Game.Drawables;

namespace PM2.GameContent.Game.Entities
{
    internal class PlayerPan : BaseEntity
    {
        // Private
        private BCircleShape _shape;
        private DrawableHitBox _hitbox;

        private Vector2 _target;
        private bool _targetX;
        private bool _targetY;

        // Constructor(s)
        internal PlayerPan()
            : base()
        {
        }
        internal PlayerPan(BodyData data)
            : base(data)
        {
        }

        // Content & Graphics
        internal override void GetContent(ContentManager content)
        {
            // Create pancake circle shape
            _shape = new BCircleShape(45f, 32);
            _shape.Position = GetWorld().Layer.GetView().Size / 2f;
            _shape.Origin = new Vector2f(_shape.Radius, _shape.Radius);

            // Create pancake circle shape
            _hitbox = new DrawableHitBox();
            _hitbox.Shape.FillColor = new Color(Color.Blue) { A = 125 };
            _hitbox.Shape.Depth = -100;
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
            // Add drawables
            layer.Renderables.Remove(_shape);
        }

        //
        internal override void OnCreated()
        {
        }
        internal override void OnStep()
        {
            // Calculate distance
            Vector2 dist = _target - GetBody().Position;
            Vector2 amount = dist;
            amount.Normalize();

            // 
            if (_targetX)
            {
                if (Math.Abs(dist.X) < Math.Abs(amount.X))
                {
                    GetBody().LinearVelocity = new Vector2(0f, GetBody().LinearVelocity.Y);
                    _targetX = false;
                }
            }

            //
            if (_targetY)
            {   
                if (Math.Abs(dist.Y) < Math.Abs(amount.Y))
                {
                    GetBody().LinearVelocity = new Vector2(GetBody().LinearVelocity.X, 0f);
                    _targetY = false;
                }
            }
        }
        internal override void OnAnimate(float delta)
        {
            Vector2f wsize = new Vector2f(GetWorld().WorldSize.X, GetWorld().WorldSize.Y);
            Vector2f pos = new Vector2f(GetBody().Position.X, GetBody().Position.Y);
            View view = GetWorld().Layer.GetView();

            // Position
            _shape.Position = pos * wsize;

            // Scale pancake
            _shape.Scale = new Vector2f(_shape.Scale.X,
                Math.Max(Math.Abs((pos.Y - view.Size.Y / 2f) / view.Size.Y * 0.45f), 0.03f));

            // Color
            float d = pos.Y / view.Size.Y;
            _shape.FillColor = new Color((byte)(d * 256 / 2 + 256 / 2), (byte)(d * 256 / 2 + 256 / 2), (byte)(d * 256 / 2 + 256 / 2));

            // Depth
            _shape.Depth = (int)pos.Y;

            // Hitbox
            _hitbox.SetShape(GetBody());
        }
        internal override void OnRemoved()
        {
        }

        //
        internal override Body CreateBody(PhysicsWorld world, BodyData data)
        {
            Body body = BodyFactory.CreateRectangle(world, 9f, 0.2f, 1f, data.Position);
            body.IsStatic = false;
            body.IgnoreGravity = true;
            body.IsKinematic = true;
            body.FixedRotation = true;

            return body;
        }

        //
        internal void SetTargetPosition(Vector2 target)
        {
            // Set Target
            _target = target;

            //
            _targetX = true;
            _targetY = true;

            // Calculate distance
            Vector2 dist = _target - GetBody().Position;
            Vector2 amount = dist;
            amount.Normalize();

            GetBody().LinearVelocity = amount;
        }

        //
        public override string ToString()
        {
            return "PLES FUCKING ADD ME";
        }
    }
}
