﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using BubbasEngine.Engine.Physics.Dynamics;
using BubbasEngine.Engine.Physics.Common;
using SFML.Window;
using BubbasEngine.Engine.Physics.Collision.Shapes;

namespace BubbasEngine.Engine.Graphics.Drawables.Shapes
{
    public class BBodyShape : IRenderable
    {
        // Private
        private List<BConvexShape> _shapes;

        private int _depth;
        private Color _fillColor;

        //
        public int Depth
        { get { return _depth; } set { _depth = value; } }
        public Color FillColor
        { get { return _fillColor; } set { SetFillColor(value); } }

        // Constrcutor(s)
        public BBodyShape()
        {
            _shapes = new List<BConvexShape>();
        }

        //
        public void SetShape(Body body)
        {
            SetShape(body, Vector2.One);
        }
        public void SetShape(Body body, float scale)
        {
            SetShape(body, Vector2.One * scale);
        }
        public void SetShape(Body body, Vector2 scale)
        {
            //
            int fCount = body.FixtureList.Count;

            // Make sure the number of shapes is correct
            if (fCount > _shapes.Count)
            {
                for (int i = _shapes.Count; i < fCount; i++)
                    _shapes.Add(CreateShape());
            }
            else if (fCount < _shapes.Count)
            {
                for (int i = _shapes.Count - 1; i >= fCount; i--)
                    _shapes.RemoveAt(i);
            }

            // Set shapes
            for (int i = 0; i < fCount; i++)
            {
                //
                PolygonShape shape = (PolygonShape)body.FixtureList[i].Shape;
                Vector2 center = shape.Normals.GetCentroid();

                //
                if (_shapes[i].PointCount != (uint)shape.Vertices.Count)
                    _shapes[i].SetPointCount((uint)shape.Vertices.Count);

                //
                BubbasEngine.Engine.Physics.Common.Transform xf;
                body.GetTransform(out xf);
                for (int j = 0; j < shape.Vertices.Count; j++)
                {
                    //
                    Vector2 vector = (MathUtils.Mul(ref xf, shape.Vertices[j]) - center) * scale;
                    _shapes[i].SetPoint((uint)j, new Vector2f(vector.X + center.X, vector.Y + center.Y));
                }
            }
        }

        //
        private void SetFillColor(Color color)
        {
            _fillColor = color;

            int length = _shapes.Count;
            for (int i = 0; i < length; i++)
                _shapes[i].FillColor = color;
        }

        private BConvexShape CreateShape()
        {
            return new BConvexShape(0u) { FillColor = _fillColor };
        }

        //
        public int GetDepth()
        {
            return _depth;
        }
        public void Draw(RenderTarget target)
        {
            // Draw
            int length = _shapes.Count;
            for (int i = 0; i < length; i++)
                _shapes[i].Draw(target);
        }
    }
}