using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.Graphics.Drawables;
using SFML.Graphics;

namespace BubbasEngine.Engine.Graphics
{
    internal class GraphicsLayer
    {
        // Private
        private Stack<Renderable> _drawables;
        private Action<float> _animate;
        private Action<RenderTarget> _draw;

        // Constructor(s)
        internal GraphicsLayer()
        {
            _drawables = new Stack<Renderable>();
        }

        // Drawable
        internal bool AddDrawable(Renderable drawable)
        {
            // Adding drawables that are already stored in this layer is not allowed
            if (ContainsDrawable(drawable))
            {
                // Error
                GameConsole.WriteLine(string.Format("{0}: Tried to add already stored drawable (Drawable {1})", GetType().Name, drawable.ToString()), GameConsole.MessageType.Warning); // Debug
                return false;
            }

            // Add drawable
            _drawables.Push(drawable);
            _animate += drawable.Animate;
            _draw += drawable.Draw;

            // Succeeded
            return true;
        }
        internal bool RemoveDrawable(Renderable drawable)
        {
            // Removing drawables that are not stored in this layer is impossible (Waste of CPU cycles to try) (Debug)
            if (!ContainsDrawable(drawable))
            {
                // Error
                GameConsole.WriteLine(string.Format("{0}: Tried to remove drawable that is not stored in this layer (Drawable {1})", GetType().Name, drawable.ToString()), GameConsole.MessageType.Error); // Debug
                return false;
            }

            // Remove drawable
            Remove(drawable);
            _animate -= drawable.Animate;
            _draw -= drawable.Draw;

            // Succeeded
            return true;
        }

        // Container
        private bool ContainsDrawable(Renderable drawable)
        {
            Stack<Renderable> stack = new Stack<Renderable>();
            bool success = false;

            // Look through all devices
            int length = _drawables.Count;
            for (int i = 0; i < length; i++)
            {
                // Get device ref and move it to the other stack
                Renderable d = _drawables.Pop();
                stack.Push(d);

                // If it is what you're looking for stop looking
                if (d == drawable)
                {
                    success = true;
                    break;
                }
            }

            // Move them back to the main stack
            length = stack.Count;
            for (int i = 0; i < length; i++)
                _drawables.Push(stack.Pop());

            // Return whether or not it was found
            return success;
        }
        private bool Remove(Renderable drawable)
        {
            Stack<Renderable> stack = new Stack<Renderable>();
            bool success = false;

            // Look through all devices
            int length = _drawables.Count;
            for (int i = 0; i < length; i++)
            {
                // Get device
                Renderable d = _drawables.Pop();

                // If it is what you're looking for stop looking
                if (d == drawable)
                {
                    success = true;
                    break;
                }

                // Move device to other stack (Will be added to the main stack afterwards)
                stack.Push(d);
            }

            // Move them back to the main stack
            length = stack.Count;
            for (int i = 0; i < length; i++)
                _drawables.Push(stack.Pop());

            // Return whether or not it was found
            return success;
        }

        //
        internal int GetDrawableCount()
        {
            return _drawables.Count;
        }

        //
        internal void Animate(float delta)
        {
            if (_animate != null)
                _animate(delta);
        }
        internal void Render(RenderTarget target)
        {
            if (_draw != null)
                _draw(target);
        }
        internal void Render(RenderTarget target, float delta)
        {
            if (_draw != null)
                _draw(target);
            if (_animate != null)
                _animate(delta);
        }
    }
}
