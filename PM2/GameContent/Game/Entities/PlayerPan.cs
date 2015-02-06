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
            _hitbox.FillColor = new Color(Color.Blue) { A = 125 };
            _hitbox.Depth = -100;
        }
        internal override void RemoveContent(ContentManager content)
        {

        }
        internal override void AddDrawables(GraphicsLayer layer)
        {
            // Add drawables
            layer.Renderables.Add(_shape);
            layer.Renderables.Add(_hitbox);
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
            Vector2f pos = new Vector2f(GetBody().Position.X, GetBody().Position.Y) / new Vector2f(GetWorld().WorldSize.X, GetWorld().WorldSize.Y);
            View view = GetWorld().Layer.GetView();

            // Position
            _shape.Position = pos * view.Size;

            // Scale pancake
            _shape.Scale = new Vector2f(_shape.Scale.X,
                Math.Max(Math.Abs((pos.Y - 1f / 2f) / 1f * 0.45f), 0.03f));

            // Color
            float d = pos.Y / 1f;
            _shape.FillColor = new Color((byte)(d * 256 / 2 + 256 / 2), (byte)(d * 256 / 2 + 256 / 2), 0);

            // Depth
            _shape.Depth = (int)pos.Y;

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
            const float width = 11f;
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

            return body;
        }

        //
        internal void SetTargetPosition(Vector2 target)
        {
            // Set Target
            _target = target;

            // Calculate distance
            //Vector2 dist = _target - GetBody().Position;
            //Vector2 amount = dist;
            //amount.Normalize();
            //amount *= 0.1f;

            //GetBody().LinearVelocity = amount;

            Vector2 dist = _target - GetBody().Position / GetWorld().StepTime;

            //
            if (amount.X != 0f)
                _targetX = true;
            if (amount.Y != 0f)
                _targetY = true;
        }
    }
}
