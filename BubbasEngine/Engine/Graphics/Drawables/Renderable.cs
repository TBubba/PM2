using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace BubbasEngine.Engine.Graphics.Drawables
{
    public abstract class Renderable
    {
        // Depth
        internal abstract int GetDepth();

        // Draw
        internal abstract void Draw(RenderTarget target);
    }
}
