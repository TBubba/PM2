using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using BubbasEngine.Engine.Graphics.Drawables;
using SFML.Window;

namespace BubbasEngine.Engine.Graphics
{
    public class GraphicsRenderer
    {
        // Private
        private GraphicsLayerContainer _layers;

        private Action<RenderTarget, float> _render;
        private Action<RenderTarget> _renderToTarget;

        private RenderTarget _target; // Window
        private RenderTexture _proxy; // Render

        private IntRect _flipScale; // Flip (Kind of a hacky way to get around negative scaling)
        private Vector2f _scale; // Scale between targets
        private RectangleShape _renderFix; // Used to avoid a clearing bug

        // Public
        public GraphicsLayerContainer Layers
        { get { return _layers; } }

        public uint RenderWidth
        { get { return _proxy.Size.X; } }
        public uint RenderHeight
        { get { return _proxy.Size.Y; } }

        public Vector2f Scale
        { get { return _scale; } }

        // Constructor(s)
        internal GraphicsRenderer()
        {
            // Layer container
            _layers = new GraphicsLayerContainer();

            //
            _renderFix = new RectangleShape(new Vector2f(0f, 0f)) { FillColor = new Color(0, 0, 0, 0) };
        }

        // Settigns for rendering
        internal void SetRenderSize(uint width, uint height) // (Calling this too often may result in a 'appcrash' - probably due to excessive memory use)
        {
            _proxy = new RenderTexture(width, height);
            _flipScale = new IntRect(0, (int)_proxy.Size.Y, (int)_proxy.Size.X, -(int)_proxy.Size.Y);
            UpdateScale();
        }
        internal void SetRenderTarget(RenderTarget target)
        {
            _target = target;
            UpdateScale();
        }

        private void UpdateScale()
        {
            float tWidth = (_target == null) ? 1f : (float)_target.Size.X;
            float tHeight = (_target == null) ? 1f : (float)_target.Size.Y;
            float pWidth = (_proxy == null) ? 1f : (float)_proxy.Size.X;
            float pHeight = (_proxy == null) ? 1f : (float)_proxy.Size.Y;

            _scale = new SFML.Window.Vector2f(tWidth / pWidth, tHeight / pHeight);
        }

        // Render (Render the current batch as usual)
        internal void Render()
        {
            // Clear proxy
            _proxy.Clear();

            // Render to proxy
            _layers.Render(_proxy);

            // Render proxy to target
            using (Sprite sprite = new Sprite(_proxy.Texture) { Scale = _scale, TextureRect = _flipScale })
            {
                _target.Draw(sprite);
                _target.Draw(_renderFix); // Forces SFML to clear (bug work-around)
            }
        }

        // RenderToTarget (Renders current batch to parameter target)
        internal void RenderToTarget(RenderTarget target)
        {
            //
            if (_renderToTarget != null)
                _renderToTarget(target);
        }

        // Public methods
        public void SetRenderSize(int width, int height) // Rename this bcus it has the same name as the internal function (whitch has a different purpose)
        {
            SetRenderSize((uint)width, (uint)height);
        }
    }
}
