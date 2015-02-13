using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.Graphics.Drawables;
using BubbasEngine.Engine.Graphics.Drawables.Shapes;
using SFML.Graphics;

namespace PM2.GameContent.Game.Drawables
{
    internal class DrawableIndicator : IRenderable
    {
        // Private
        private BSprite _arrow;
        private BText _number;

        private int _depth;

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
            _number = new BText();
        }

        // Depth
        public int GetDepth()
        {
            return _depth;
        }

        // Draw
        public void Draw(RenderTarget target)
        {
            // Draw
            _arrow.Draw(target);
            _number.Draw(target);
        }
    }
}
