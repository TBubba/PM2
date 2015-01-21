﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace BubbasEngine.Engine.Graphics.Drawables.Shapes
{
    public class BConvexShape : BShape
    {
        // Public
        public uint PointCount
        { get { return GetShape().GetPointCount(); } set { GetShape().SetPointCount(value); } }

        // Constructor(s)
        public BConvexShape(BConvexShape copy)
        {
            _shape = new ConvexShape(copy.GetShape());
            _state = RenderStates.Default;
        }
        public BConvexShape(uint PointCount)
        {
            _shape = new ConvexShape(PointCount);
            _state = RenderStates.Default;
        }

        //
        private ConvexShape GetShape()
        {
            return (ConvexShape)_shape;
        }

        //
        public Vector2f GetPoint(uint index)
        {
            return GetShape().GetPoint(index);
        }
        public void SetPoint(uint index, Vector2f point)
        {
            GetShape().SetPoint(index, point);
        }
    }
}
