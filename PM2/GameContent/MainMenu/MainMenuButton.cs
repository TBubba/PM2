using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.Graphics.Drawables;
using SFML.Graphics;
using SFML.Window;
using BubbasEngine.Engine.Graphics;
using PM2.Common;

namespace PM2.GameContent.MainMenu
{
    internal class MainMenuButton : IGenericMenuItem
    {
        // Private
        private BSprite _buttonSprite;
        private BText _buttonText;
        private BVertexArray _buttonOutline;

        private Action _onPushed;
        private IntRect _cursorRect; // Rectangle for checking if the cursor is overlapping the button
        private bool _selected;

        // Constructor(s)
        internal MainMenuButton(string text, Action onPushed)
        {
            //
            _buttonText = new BText();
            _buttonText.Text = text;

            //
            _onPushed = onPushed;
        }

        //
        internal void SetOnPush(Action meth)
        {
            _onPushed = meth;
        }
        internal void SetDrawable(Vector2f position, Texture buttonTexture, Shader outlineShader, Font buttonFont)
        {
            // Button Sprite
            _buttonSprite = new BSprite(buttonTexture);
            _buttonSprite.Position = position;
            _buttonSprite.Origin = new Vector2f(buttonTexture.Size.X / 2f, buttonTexture.Size.Y / 2f);

            // Outline
            _buttonOutline = new BVertexArray(PrimitiveType.TrianglesFan, 4, outlineShader);

            FloatRect frect = _buttonSprite.GetGlobalBounds();

            _buttonOutline[0] = new Vertex(new Vector2f(frect.Left, frect.Top));
            _buttonOutline[1] = new Vertex(new Vector2f(frect.Left + frect.Width, frect.Top));
            _buttonOutline[2] = new Vertex(new Vector2f(frect.Left + frect.Width, frect.Top + frect.Height));
            _buttonOutline[3] = new Vertex(new Vector2f(frect.Left, frect.Top + frect.Height));

            //_buttonOutline[0] = new Vertex(new Vector2f(0f, 0f));
            //_buttonOutline[1] = new Vertex(new Vector2f(0f, 720f));
            //_buttonOutline[2] = new Vertex(new Vector2f(1280f, 720f));
            //_buttonOutline[3] = new Vertex(new Vector2f(1280f, 0f));

            outlineShader.SetParameter("texture", buttonTexture);
            outlineShader.SetParameter("stepSize", new Vector2f(3f / (float)buttonTexture.Size.X, 3f / (float)buttonTexture.Size.Y));
            outlineShader.SetParameter("outlineColor", new Color(50, 50, 200, 255));
            outlineShader.SetParameter("textureAlpha", 0f);

            // Text
            _buttonText.Font = buttonFont;
            _buttonText.Position = position;

            SetText(_buttonText.Text);

            // Depth
            _buttonSprite.Depth = 1;
            _buttonText.Depth = 0;

            // Cursor
            _cursorRect = new IntRect((int)frect.Left, (int)frect.Top, (int)frect.Width, (int)frect.Height);
        }
        internal void SetText(string text)
        {
            FloatRect frect = _buttonText.GetLocalBounds();

            _buttonText.Text = text;
            _buttonText.Origin = new Vector2f(frect.Width / 2f, frect.Height / 2f);
        }

        //
        internal bool Contains(int x, int y)
        {
            if (_cursorRect.Contains(x, y))
                return true;
            return false;
        }

        //
        public void OnSelect()
        {
            //
            _selected = true;
        }
        public void OnDeselect()
        {
            //
            _selected = false;
        }
        public void OnPush()
        {
            //
            _onPushed();
        }

        //
        internal void Animate(float time)
        {
            Color color;

            if (_selected)
                color = new Color(50, 50, 200, (byte)(125d + Math.Max(Math.Sin(time * 0.25f), 0) * 125d));
            else
                color = new Color(50, 50, 200, 0);

            _buttonOutline.Shader.SetParameter("outlineColor", color);
        }

        //
        internal void AddDrawables(GraphicsLayer layer)
        {
            layer.Renderables.Add(_buttonSprite);
            layer.Renderables.Add(_buttonText);

            layer.Renderables.Add(_buttonOutline);
        }
        internal void RemoveDrawables(GraphicsLayer layer)
        {
            layer.Renderables.Remove(_buttonSprite);
            layer.Renderables.Remove(_buttonText);

            layer.Renderables.Remove(_buttonOutline);
        }
    }
}
