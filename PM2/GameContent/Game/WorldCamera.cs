using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;

namespace PM2.GameContent.Game
{
    internal class WorldCamera
    {
        // Private
        private Vector2f _position;
        private Vector2f _origin;
        private Vector2f _size;

        // Internal
        internal Vector2f Position
        { get { return _position; } set { _position = value; } }
        internal Vector2f Origin
        { get { return _origin; } set { _origin = value; } }
        internal Vector2f Size
        { get { return _size; } set { _size = value; } }

        // Constructor(s)
        internal WorldCamera()
        {
        }

        //
        internal void CenterOrigin()
        {
            _origin = _size / 2f;
        }

        //
        internal Vector2f RelativePosition()
        {
            return _position - _origin;
        }
        internal bool IfInside(Vector2f pos)
        {
            if (pos.X < _position.X ||
                pos.X > _position.X + _size.X ||
                pos.Y < _position.Y ||
                pos.Y > _position.Y + _size.Y)
                return true;
            return false;
        }

        //
        internal Vector2f Center()
        {
            return _position + _size / 2f;
        }
        internal float Top()
        {
            return _position.Y - _origin.Y;
        }
        internal float Bottom()
        {
            return _position.Y + _size.Y - _origin.Y;
        }
        internal float Left()
        {
            return _position.X - _origin.X;
        }
        internal float Right()
        {
            return _position.X + _size.X - _origin.X;
        }
    }
}
