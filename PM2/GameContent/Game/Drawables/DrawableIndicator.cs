using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.Graphics.Drawables;
using BubbasEngine.Engine.Graphics.Drawables.Shapes;
using SFML.Graphics;
using SFML.Window;

namespace PM2.GameContent.Game.Drawables
{
    internal class DrawableIndicator : IRenderable
    {
        // Private
        private BSprite _arrow;
        private BText _number;

        private int _depth;
        private bool _visable;

        // Internal
        internal int Depth
        { get { return _depth; } set { _depth = value; } }

        internal Texture Texture
        { get { return _arrow.Texture; } set { _arrow.Texture = value; } }
        internal Font Font
        { get { return _number.Font; } set { _number.Font = value; } }

        // Constructor(s)
        internal DrawableIndicator()
        {
            _arrow = new BSprite();
            _arrow.Origin = new Vector2f(0.5f, 0.5f);

            _number = new BText();
            _number.Origin = new Vector2f(0.5f, 0.5f);
        }

        //
        internal void UpdateOrientation(Vector2f position, Vector2f viewSize)
        {
            // Visable
            if (position.Y >= 0f)
            {
                _visable = false;
                return;
            }

            _visable = true;

            // Position
            Vector2f viewPos = position * viewSize;
            _arrow.Position = new Vector2f(viewPos.X, _arrow.Position.Y);
            _number.Position = new Vector2f(viewPos.X, _number.Position.Y);

            // Number
            float dist = Math.Abs(position.Y) * 10f;
            _number.Text = string.Format("{0:000.}", dist);

            // Depth
            _depth = (int)(-dist * 10f);
        }

        // Depth
        public int GetDepth()
        {
            return _depth;
        }

        // Draw
        public void Draw(RenderTarget target)
        {
            // Don't draw if not visable
            if (!_visable)
                return;

            // Draw
            _arrow.Draw(target);
            _number.Draw(target);
        }
    }
}
