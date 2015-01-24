using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.GameWorlds;
using BubbasEngine.Engine.GameWorlds.GameInterfaces;
using BubbasEngine.Engine.Content;
using BubbasEngine.Engine.Graphics;
using SFML.Window;

namespace PM2.GameContent.Game.Entities
{
    internal class BaseEntity : GameObject, IGameCreated, IGameStep, IGameAnimate, IGameRemoved
    {
        // Private
        protected Vector2f _position;

        private ContentManager _content;
        private GraphicsRenderer _graphics;

        // Internal
        internal Vector2f Position
        { get { return _position; } }

        // Constructor(s)
        internal BaseEntity(Vector2f position)
        {
            _position = position;
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

        //
        public void Created()
        {
            //
            OnCreated();
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
