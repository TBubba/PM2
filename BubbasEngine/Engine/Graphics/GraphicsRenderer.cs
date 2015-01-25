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
        private Dictionary<int, GraphicsLayer> _layers;

        private Action<RenderTarget, float> _render;
        private Action<RenderTarget> _renderToTarget;

        private RenderTarget _target; // Window
        private RenderTexture _proxy; // Render

        private IntRect _flipScale; // Flip (Kind of a hacky way to get around negative scaling)
        private Vector2f _scale; // Scale between targets
        private RectangleShape _renderFix; // Used to avoid a clearing bug

        // Public
        public uint RenderWidth
        { get { return _proxy.Size.X; } }
        public uint RenderHeight
        { get { return _proxy.Size.Y; } }

        public Vector2f Scale
        { get { return _scale; } }

        // Constructor(s)
        internal GraphicsRenderer()
        {
            _layers = new Dictionary<int, GraphicsLayer>();
            _renderFix = new RectangleShape(new Vector2f(0f, 0f)) { FillColor = new Color(0, 0, 0, 0) };
        }
      　
        // Drawables
        public bool AddDrawable(Renderable drawable, int layer)
        {
            // Throw exception if parameter drawable is null
            if (drawable == null)
                throw new Exception("drawable must not be null");

            // Add the layer if it is missing
            if (!_layers.ContainsKey(layer))
            {
                // Add layer
                GraphicsLayer l = new GraphicsLayer();
                _layers.Add(layer, l);
                
                // Sort layers (Adds the new layer into the mix)
                SortRender();

                GameConsole.WriteLine(string.Format("{0}: Layer added (Layer {1})", GetType().Name, layer)); // Debug
            }

            // Add drawable to layer
            bool s = _layers[layer].AddDrawable(drawable);

            // Debug
            if (s)
                GameConsole.WriteLine(string.Format("{0}: Added drawable to layer (Drawable {1}, Layer {2})", GetType().Name, drawable.GetType().Name, layer)); // Debug

            // Return Success / Fail
            return s;
        }
        public bool RemoveDrawable(Renderable drawable, int layer)
        {
            // Add the layer if it is missing
            if (!_layers.ContainsKey(layer))
            {
                // Fail
                GameConsole.WriteLine(string.Format("{0}: Tried to remove drawable from non-existing layer (Layer {1})", GetType().Name, layer), GameConsole.MessageType.Error); // Debug
                return false;
            }

            // Remove drawable to layer
            bool s = _layers[layer].RemoveDrawable(drawable);

            // Debug
            if (s)
                GameConsole.WriteLine(string.Format("{0}: Removed drawable from layer (Drawable {1}, Layer {2})", GetType().Name, drawable.GetType().Name, layer)); // Debug

            // Remove layer if empty
            if (_layers[layer].GetDrawableCount() == 0)
            {
                // Remove layer from methods
                GraphicsLayer l = _layers[layer];
                _render -= l.Render;
                _renderToTarget -= l.Render;

                // Remove Layer
                _layers.Remove(layer);

                GameConsole.WriteLine(string.Format("{0}: Layer removed (Layer {1})", GetType().Name, layer)); // Debug
            }

            // Success
            return true;
        }

        // Sort (Sort rendering layers)
        internal void SortRender()
        {
            // Clear methods
            _render = null;
            _renderToTarget = null;

            // Get layer order
            List<int> lay = _layers.Keys.ToList();
            lay.Sort();

            // Order methods
            int length = lay.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                _render += _layers[lay[i]].Render;
                _renderToTarget += _layers[lay[i]].Render;
            }
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
        internal void Render(float delta)
        {
            // Clear proxy
            _proxy.Clear();

            // Render to proxy
            if (_render != null)
                _render(_proxy, delta);

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
