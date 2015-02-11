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

namespace PM2.GameContent.Game.Entities
{
    internal class PlayerPan : BaseEntity
    {
        // Private
        private BCircleShape _shape;

        private int _stepsToTarget;

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
            _shape.Position = GetWorld().MainLayer.GetView().Size / 2f;
            _shape.Origin = new Vector2f(_shape.Radius, _shape.Radius);

            // Create pancake circle shape
            _hitbox.FillColor = new Color(Color.Blue) { A = 125 };
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
            // Target reached
            if (_stepsToTarget == 1)
            {
                // Stop
                GetBody().LinearVelocity = new Vector2(0f, 0f);
                _stepsToTarget = 0;
            }

            // Step timer (target)
            if (_stepsToTarget > 0)
                _stepsToTarget--;
        }
        internal override void OnAnimate(float delta)
        {
            Vector2f pos = new Vector2f(GetBody().Position.X, GetBody().Position.Y) / new Vector2f(GetWorld().WorldSize.X, GetWorld().WorldSize.Y);
            View view = GetWorld().MainLayer.GetView();

            // Position
            _shape.Position = pos * view.Size;

            // Scale pancake
            _shape.Scale = new Vector2f(_shape.Scale.X,
                Math.Max(Math.Abs((pos.Y - 1f / 2f) / 1f * 0.45f), 0.03f));

            // Color
            float d = pos.Y / 1f;
            _shape.FillColor = new Color((byte)(d * 256 / 2 + 256 / 2), (byte)(d * 256 / 2 + 256 / 2), 0);

            // Depth
            _shape.Depth = (int)pos.Y * 10;

            // Hitbox
            _hitbox.SetShape(GetBody(), new Vector2(view.Size.X / GetWorld().WorldSize.X, view.Size.Y / GetWorld().WorldSize.Y));
        }
        internal override void OnRemoved()
        {
        }

        //
        internal override Body CreateBody(PhysicsWorld world, BodyData data)
        {
            //
            const float width = 7f;
            const float height = 0.2f;
            const float botHeight = 0.2f;
            const float wallWidth = 0.2f;

            //
            Body body = new Body(world, data.Position, data.Rotation);

            Vertices botVerts = PolygonTools.CreateRectangle(width / 2, botHeight / 2);
            PolygonShape botShape = new PolygonShape(botVerts, 1f);
            body.CreateFixture(botShape);

            //Vertices wallVerts = PolygonTools.CreateRectangle(wallWidth / 2, height, new Vector2(-width / 2f, -height), 0f);
            //PolygonShape wallShape = new PolygonShape(wallVerts, 1f);
            //body.CreateFixture(wallShape);

            //Vertices wall2Verts = PolygonTools.CreateRectangle(wallWidth / 2, height, new Vector2((width / 2f) - wallWidth, -height), 0f);
            //PolygonShape wall2Shape = new PolygonShape(wall2Verts, 1f);
            //body.CreateFixture(wall2Shape);

            //
            body.IsStatic = false;
            body.IgnoreGravity = true;
            body.IsKinematic = true;
            body.FixedRotation = true;

            body.SleepingAllowed = false;

            return body;
        }

        //
        internal void SetTargetPosition(Vector2 target)
        {
            // Move towards target
            Vector2 dist = (target - GetBody().Position) / GetWorld().StepTime;
            GetBody().LinearVelocity = dist;

            // Set "Step timer"
            _stepsToTarget = 2;
        }
    }
}
