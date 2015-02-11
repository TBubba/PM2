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
using BubbasEngine.Engine.Graphics.Drawables;

namespace PM2.GameContent.Game
{
    internal class PanWorld : GameWorld
    {
        // Private
        private ContentManager _content;

        private GraphicsLayer _backLayer;
        private GraphicsLayer _mainLayer;
        private GraphicsLayer _interfaceLayer;
        private GraphicsLayer _debugLayer;

        private Vector2 _worldSize;

        // Internal
        internal GraphicsLayer BackLayer
        { get { return _backLayer; } }
        internal GraphicsLayer MainLayer
        { get { return _mainLayer; } }
        internal GraphicsLayer InterfaceLayer
        { get { return _interfaceLayer; } }
        internal GraphicsLayer DebugLayer
        { get { return _debugLayer; } }

        internal Vector2 WorldSize
        { get { return _worldSize; } set { _worldSize = value; } }

        // Constructor(s)
        internal PanWorld(float stepTime)
            : base(stepTime)
        {
        }

        //
        internal void Initialize(ContentManager content, GraphicsLayerContainer layerContainer)
        {
            // Keep references
            _content = content;

            // Create layers
            _backLayer = layerContainer.Create();
            _mainLayer = layerContainer.Create();
            _interfaceLayer = layerContainer.Create();
            _debugLayer = layerContainer.Create();

            // EntityContainer Events
            OnEntityActivated += EntityActivated;
            OnEntityDeactivated += EntityDeactivated;
        }

        //
        private void EntityActivated(GameObject obj)
        {
            BaseEntity ent = (BaseEntity)obj;
            ent.GetContent(_content);
            ent.AddDrawables(_mainLayer, _debugLayer);
        }
        private void EntityDeactivated(GameObject obj)
        {
            BaseEntity ent = (BaseEntity)obj;
            ent.RemoveContent(_content);
            ent.RemoveDrawables(_mainLayer, _debugLayer);
        }
    }
}
