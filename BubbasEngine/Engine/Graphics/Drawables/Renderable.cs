using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;

namespace BubbasEngine.Engine.Graphics.Drawables
{
    public abstract class Renderable
    {
        // Animate
        internal abstract void Animate(float delta);

        // Draw
        internal abstract void Draw(RenderTarget target);
    }
}
