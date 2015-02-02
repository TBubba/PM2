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
using BubbasEngine.Engine.Physics.Common;

namespace PM2.GameContent.Game
{
    internal class PanWorld : GameWorld
    {
        // Private
        private ContentManager _content;
        private GraphicsLayer _layer;

        private Vector2 _worldSize;

        // Internal
        internal GraphicsLayer Layer
        { get { return _layer; } }

        internal Vector2 WorldSize
        { get { return _worldSize; } set { _worldSize = value; } }

        // Constructor(s)
        internal PanWorld(float stepTime)
            : base(stepTime)
        {
        }

        //
        internal void Initialize(ContentManager content, GraphicsLayer layer)
        {
            // Keep references
            _content = content;
            _layer = layer;

            // EntityContainer Events
            OnEntityActivated += EntityActivated;
            OnEntityDeactivated += EntityDeactivated;
        }

        //
        private void EntityActivated(GameObject obj)
        {
            BaseEntity ent = (BaseEntity)obj;
            ent.GetContent(_content);
            ent.AddDrawables(_layer);
        }
        private void EntityDeactivated(GameObject obj)
        {
            BaseEntity ent = (BaseEntity)obj;
            ent.RemoveContent(_content);
            ent.RemoveDrawables(_layer);
        }
    }
}
