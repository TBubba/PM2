﻿using System;
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
        private DrawablePlate _shape;

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
            // Create pan circle shape
            _shape = new DrawablePlate(45f);
            _shape.TopColor = Color.Gray;
            _shape.BotColor = Color.DarkGray;

            // Create pancake circle shape
            _hitbox.FillColor = new Color(Color.Yellow) { A = 125 };
            _hitbox.Depth = -1000;
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
            float rot = GetBody().Rotation % ((float)Math.PI * 2f);
            if (rot < 0f)
                rot += (float)Math.PI * 2f;

            View view = GetWorld().MainLayer.GetView();

            //
            _shape.UpdateOrientation(pos, rot, view.Size);

            // Color
            float d = pos.Y / 1f;
            //_shape.TopColor = new Color((byte)(d * 256 / 2 + 256 / 2), (byte)(d * 256 / 2 + 256 / 2), (byte)(d * 256 / 2 + 256 / 2));

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
            body.Friction = 1f;

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

        //
        public override string ToString()
        {
            //
            int depth = (_shape != null) ? _shape.GetDepth() : 0;

            //
            return base.ToString() +
                string.Format(" Depth({0,3})", depth);
        }
    }
}
