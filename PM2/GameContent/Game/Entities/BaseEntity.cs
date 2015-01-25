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

namespace PM2.GameContent.Game.Entities
{
    internal class BaseEntity : GameObject, IGameCreated, IGamePhysics, IGameStep, IGameAnimate, IGameRemoved
    {
        // Private
        private Body _body;
        private BodyData _bodyData;

        private Vector2 _startPosition;

        private ContentManager _content;
        private GraphicsRenderer _graphics;

        // Internal
        internal Body Body
        { get { return _body; } }

        // Constructor(s)
        internal BaseEntity()
            : this(new BodyData())
        {
        }
        internal BaseEntity(BodyData data)
        {
            _bodyData = data;
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

        internal virtual void AddDrawables(GraphicsRenderer graphics)
        {
            // Keep reference
            _graphics = graphics;

            // Add drawables to renderer
        }
        internal virtual void RemoveDrawables(GraphicsRenderer graphics)
        {
            // Remove drawables from renderer
        }

        // Physics
        internal void MoveTo(Vector2 position)
        {
            if (_body != null)
                _body.Position = position;
        }

        //
        public void Created()
        {
            //
            OnCreated();
        }
        public Body AddBody(PhysicsWorld world)
        {
            _body = CreateBody(world, _bodyData);
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
            RemoveContent(_content);
            RemoveDrawables(_graphics);

            //
            OnRemoved();
        }

        //
        internal virtual Body CreateBody(PhysicsWorld world, BodyData data)
        {
            return new Body(world, data.Position, data.Rotation);
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
    }
}
