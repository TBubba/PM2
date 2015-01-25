using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.GameWorlds;
using SFML.Window;
using PM2.GameContent.Game.Entities;
using BubbasEngine.Engine.Graphics;
using BubbasEngine.Engine.Content;
using BubbasEngine.Engine.Input;

namespace PM2.GameContent.Game
{
    internal class PanWorld : GameWorld
    {
        // Private
        private WorldCamera _camera;

        private ContentManager _content;
        private GraphicsRenderer _graphics;

        // Internal
        internal WorldCamera Camera
        { get { return _camera; } }

        // Constructor(s)
        internal PanWorld(float stepTime)
            : base(stepTime)
        {
            _camera = new WorldCamera();
        }

        //
        internal void Initialize(ContentManager content, GraphicsRenderer graphics)
        {
            // Keep references
            _content = content;
            _graphics = graphics;

            // EntityContainer Events
            Entities.OnEntityAdded += OnEntityAdded;
            Entities.OnEntityRemoved += OnEntityRemoved;

            // Camera
            _camera.Size = new Vector2f(graphics.RenderWidth, graphics.RenderHeight);
        }

        //
        private void OnEntityAdded(GameObject obj)
        {
            BaseEntity ent = (BaseEntity)obj;
            ent.GetContent(_content);
            ent.AddDrawables(_graphics);
        }
        private void OnEntityRemoved(GameObject obj)
        {
            BaseEntity ent = (BaseEntity)obj;
            ent.RemoveContent(_content);
            ent.RemoveDrawables(_graphics);
        }
    }
}
