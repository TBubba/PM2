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

        private Color _topColor;
        private Color _botColor;

        private bool _isUpsideDown;

        //
        internal Vector2f Position
        { get { return _shape.Position; } }
        internal float Radius
        { get { return _shape.Radius; } set { _shape.Radius = value; UpdateOrigin(); } }

        internal Color TopColor
        { get { return _topColor; } set { _topColor = value; } }
        internal Color BotColor
        { get { return _botColor; } set { _botColor = value; } }

        internal bool IsUpsideDown
        { get { return _isUpsideDown; } set { _isUpsideDown = value; } }

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

            // Is Upside Down (?)
            bool flipped = false; // If it is flipped by rotation
            bool aboveCenter = false; // If the bottom is visable due to the camera angle

            if (rotation >= (float)Math.PI * 0.5f
             && rotation <= (float)Math.PI * 1.5f)
                flipped = true;

            if (position.Y <= 0.5f)
                aboveCenter = true;

            _isUpsideDown = flipped;
            if (aboveCenter)
                _isUpsideDown = !flipped;

            // Color
            Color curColor;

            if (_isUpsideDown)
                curColor = _botColor;
            else
                curColor = _topColor;

            const float blendDist = 0.02f;
            if (Math.Abs(position.Y - 0.5f) <= blendDist)
            {
                float force = (position.Y - 0.5f + blendDist / 2f) / blendDist;

                if (flipped)
                    curColor = Color.Blend(_botColor, _topColor, force);
                else
                    curColor = Color.Blend(_topColor, _botColor, force);
            }

            _shape.FillColor = curColor;

            // Scale
            float s1 = Math.Abs((position.Y / 1f) - 0.5f); // Scale relative to view
            float s2 = (float)Math.Abs(Math.Cos(rotation)); // Scale relative to rotation
            float scale = Math.Min(s1, s2);
            _shape.Scale = new Vector2f(_shape.Scale.X,
                Math.Max(scale * 0.45f, 0.03f));

            // Rotation
            _shape.Rotation = rotation * (360f / ((float)Math.PI * 2f));

            // Depth
            _shape.Depth = (int)(Math.Abs(position.Y - 0.5f) * Settings.DepthMultiplier);
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
