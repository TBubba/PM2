﻿using System;
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
            _shape.Position = GetWorld().Layer.GetView().Size / 2f;
            _shape.Origin = new Vector2f(_shape.Radius, _shape.Radius);
            _shape.FillColor = Color.Yellow;

            // Create pancake circle shape
            _hitbox.FillColor = new Color(Color.Red) { A = 125 };
            _hitbox.Depth = -100;
        }
        internal override void RemoveContent(ContentManager content)
        {

        }
        internal override void AddDrawables(GraphicsLayer layer)
        {
            // Add drawables
            layer.Renderables.Add(_shape);

            //
            base.AddDrawables(layer);
        }
        internal override void RemoveDrawables(GraphicsLayer layer)
        {
            // Remove drawables
            layer.Renderables.Remove(_shape);

            //
            base.RemoveDrawables(layer);
        }

        //
        internal override void OnCreated()
        {
        }
        internal override void OnStep()
        {
            if (GetBody().Position.Y > GetWorld().WorldSize.Y ||
                GetBody().Position.Y < 0f ||
                GetBody().Position.X > GetWorld().WorldSize.X ||
                GetBody().Position.X < 0f)
                GetWorld().Entities.Remove(this);
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

            // Rotation
            _shape.Rotation = GetBody().Rotation * (360f / ((float)Math.PI * 2f));
            
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
            Body body = BodyFactory.CreateRectangle(world, 9f, 0.2f, 1f, data.Position);
            body.IsStatic = false;
            body.IsKinematic = false;
            //body.FixedRotation = true;

            return body;
        }
    }
}
