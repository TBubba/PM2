using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.Graphics.Drawables.Shapes;
using SFML.Window;
using BubbasEngine.Engine.Physics.Collision.Shapes;
using BubbasEngine.Engine.Physics.Dynamics;
using BubbasEngine.Engine.Physics.Common;

namespace PM2.GameContent.Game.Drawables
{
    internal class DrawableHitBox
    {
        // Private
        private BConvexShape _shape;

        // Internal
        internal BConvexShape Shape
        { get { return _shape; } }

        // Constrcutor(s)
        internal DrawableHitBox()
        {
            _shape = new BConvexShape(0u);
        }

        //
        internal void SetShape(Body body)
        {
            //
            PolygonShape shape = (PolygonShape)body.FixtureList[0].Shape;
            
            //
            _shape.SetPointCount((uint)shape.Vertices.Count);

            //
            BubbasEngine.Engine.Physics.Common.Transform xf;
            body.GetTransform(out xf);
            for (int i = 0; i < shape.Vertices.Count; i++)
            {
                //
                Vector2 vector = MathUtils.Mul(ref xf, shape.Vertices[i]);
                _shape.SetPoint((uint)i, new Vector2f(vector.X, vector.Y));
            }
        }
        internal void SetShape(Body body, float scale)
        {
            //
            PolygonShape shape = (PolygonShape)body.FixtureList[0].Shape;

            //
            _shape.SetPointCount((uint)shape.Vertices.Count);

            //
            BubbasEngine.Engine.Physics.Common.Transform xf;
            body.GetTransform(out xf);
            for (int i = 0; i < shape.Vertices.Count; i++)
            {
                //
                Vector2 vector = MathUtils.Mul(ref xf, shape.Vertices[i]) * scale;
                _shape.SetPoint((uint)i, new Vector2f(vector.X, vector.Y));
            }
        }
        internal void SetShape(Body body, Vector2 scale)
        {
            //
            PolygonShape shape = (PolygonShape)body.FixtureList[0].Shape;

            //
            _shape.SetPointCount((uint)shape.Vertices.Count);

            //
            BubbasEngine.Engine.Physics.Common.Transform xf;
            body.GetTransform(out xf);
            for (int i = 0; i < shape.Vertices.Count; i++)
            {
                //
                Vector2 vector = MathUtils.Mul(ref xf, shape.Vertices[i]) * scale;
                _shape.SetPoint((uint)i, new Vector2f(vector.X, vector.Y));
            }
        }

        internal void SetShape(Vector2f[] vertecies)
        {
            //
            int length = vertecies.Length;
            for (int i = 0; i < length; i++)
            {
                _shape.SetPoint((uint)i, vertecies[i]);
            }
        }
    }
}
