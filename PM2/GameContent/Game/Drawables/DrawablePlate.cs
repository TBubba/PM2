using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.Graphics.Drawables.Shapes;
using BubbasEngine.Engine.Graphics.Drawables;
using SFML.Window;
using SFML.Graphics;

namespace PM2.GameContent.Game.Drawables
{
    internal class DrawablePlate : IRenderable
    {
        //
        private BCircleShape _shape;

        //
        internal Color Color
        { get { return _shape.FillColor; } set { _shape.FillColor = value; } }
        internal Vector2f Position
        { get { return _shape.Position; } }
        internal float Radius
        { get { return _shape.Radius; } set { _shape.Radius = value; UpdateOrigin(); } }

        // Constructor(s)
        internal DrawablePlate()
            : this(1f)
        {
        }
        internal DrawablePlate(float radius)
        {
            _shape = new BCircleShape(radius, 25u);
            UpdateOrigin();
        }

        //
        private void UpdateOrigin()
        {
            _shape.Origin = new Vector2f(_shape.Radius, _shape.Radius);
        }
        internal void UpdateOrientation(Vector2f position, float rotation, Vector2f viewSize)
        {
            // Correct rotation (clamp it between 0 and 2pi)
            rotation %= ((float)Math.PI * 2f);
            if (rotation < 0f)
                rotation += (float)Math.PI * 2f;

            // Position
            _shape.Position = position * viewSize;

            // Scale
            float s1 = Math.Abs((position.Y / 1f) - 0.5f); // Scale relative to view
            float s2 = (float)Math.Abs(Math.Cos(rotation)); // Scale relative to rotation
            float scale = Math.Min(s1, s2);
            _shape.Scale = new Vector2f(_shape.Scale.X,
                Math.Max(scale * 0.45f, 0.03f));

            // Rotation
            _shape.Rotation = rotation * (360f / ((float)Math.PI * 2f));

            // Depth
            _shape.Depth = (int)(position.Y * Settings.DepthMultiplier);
        }

        // Depth
        public int GetDepth()
        {
            return _shape.Depth;
        }

        // Draw
        public void Draw(RenderTarget target)
        {
            // Draw
            _shape.Draw(target);
        }
    }
}
