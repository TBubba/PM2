using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.GameWorlds;
using BubbasEngine.Engine.GameWorlds.GameInterfaces;
using BubbasEngine.Engine.Content;
using BubbasEngine.Engine.Graphics;
using SFML.Window;
using BubbasEngine.Engine.Physics.Common;
using BubbasEngine.Engine.Physics.Dynamics;
using BubbasEngine.Engine.Physics.Factories;
using BubbasEngine.Engine.Graphics.Drawables.Shapes;

namespace PM2.GameContent.Game.Entities
{
    internal class BaseEntity : GameObject, IGameCreated, IGamePhysics, IGameStep, IGameAnimate, IGameRemoved
    {
        // Private
        private Body _body;
        private BodyData _bodyData;

        protected BBodyShape _hitbox
        { get; private set; }

        private ContentManager _content;

        // Internal
        internal BBodyShape Hitbox
        { get { return _hitbox; } }

        // Constructor(s)
        internal BaseEntity()
            : this(new BodyData())
        {
        }
        internal BaseEntity(BodyData data)
        {
            _bodyData = data;
            _hitbox = new BBodyShape();
        }

        // World
        internal PanWorld GetWorld()
        {
            return (PanWorld)_world;
        }

        // Content & Graphics
        internal virtual void GetContent(ContentManager content)
        {
            // Keep reference
            _content = content;

            // Request content (and create drawables)
        }
        internal virtual void RemoveContent(ContentManager content)
        {
            // Dequest content (and remove drawables?)
        }

        internal virtual void AddDrawables(GraphicsLayer gameLayer, GraphicsLayer hitboxLayer)
        {
            // Add drawables to renderer
            hitboxLayer.Renderables.Add(_hitbox);
        }
        internal virtual void RemoveDrawables(GraphicsLayer gameLayer, GraphicsLayer hitboxLayer)
        {
            // Remove drawables from renderer
            hitboxLayer.Renderables.Remove(_hitbox);
        }

        //
        public void Created()
        {
            //
            OnCreated();
        }
        public Body AddBody(PhysicsWorld world)
        {
            // Create body
            _body = CreateBody(world, _bodyData);
            
            // Apply start-velocity
            if (_bodyData.StartVelocity != Vector2.Zero)
                _body.ApplyLinearImpulse(_bodyData.StartVelocity);

            // Return body
            return _body;
        }
        public void Step()
        {
            //
            OnStep();
        }
        public void Animate(float delta)
        {
            //
            OnAnimate(delta);
        }
        public void Removed()
        {
            //
            OnRemoved();
        }

        //
        public Body GetBody()
        {
            return _body;
        }
        internal virtual Body CreateBody(PhysicsWorld world, BodyData data)
        {
            return new Body(world, data.StartPosition, data.StartRotation, data);
        }

        //
        internal virtual void OnCreated()
        {
        }
        internal virtual void OnStep()
        {
        }
        internal virtual void OnAnimate(float delta)
        {
        }
        internal virtual void OnRemoved()
        {
        }

        // ToString
        public override string ToString()
        {
            Vector2 position = Vector2.NaN;
            Vector2 velocity = Vector2.NaN;

            int rotation = 0;
            float angular = 0f;

            if (_body != null)
            {
                position = _body.Position;
                velocity = _body.LinearVelocity;

                rotation = (int)(_body.Rotation / (Math.PI * 2f) * 360f) % 359;
                if (Math.Sign(rotation) == -1) rotation += 360;

                angular = _body.AngularVelocity;
            }

            return "[" + GetType().Name + "]" +
                   string.Format("{0}", " Pos(" + string.Format("{0,9:0.000};{1,9:0.000}", position.X, position.Y) + ")") +
                   string.Format("{0}", " Vel(" + string.Format("{0,8:0.000};{1,8:0.000}", velocity.X, velocity.Y) + ")") +
                   " Rot(" + string.Format("{0:000}", rotation) + ")" +
                    " Ang(" + string.Format("{0,5:00.0}", angular) + ")";
        }
    }
}
