using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.GameStates;
using BubbasEngine.Engine.GameWorlds;
using BubbasEngine.Engine.Input.Devices.Mouses;
using BubbasEngine.Engine.Input.Devices.Keyboards;
using BubbasEngine.Engine.Graphics.Drawables;
using SFML.Window;
using BubbasEngine.Engine;
using SFML.Graphics;
using BubbasEngine.Engine.Graphics.Drawables.Shapes;

namespace PM2.GameContent.Game
{
    internal class MainGameState : GameState
    {
        // Private
        private PanGame _game;
        private KeyboardBindingCollection _keys;
        private MouseBindingCollection _mouse;


        // Constructor(s)
        internal MainGameState()
        {
            // Game
            _game = new PanGame(new PanGameArgs(){
                Players = 1,
                GameMode = 0
                });

            // Keyboard
            _keys = new KeyboardBindingCollection();

            // Mouse
            _mouse = new MouseBindingCollection();
        }

        //
        public override void LoadContent()
        {
            // Define content paths
            const string panPath = @"GameContent\Game\Pans\StandardPan.png";

            // Request content
            _content.RequestTexture(this, panPath);

            // Define positioning
            float halfWidth = (float)(_graphics.RenderWidth / 2u);
            float halfHeight = (float)(_graphics.RenderHeight / 2u);

            //
            Texture recsTex = _content.GetTexture(panPath);
            BRectangleShape recs = new BRectangleShape(new Vector2f(recsTex.Size.X, recsTex.Size.Y));
            recs.Position = new Vector2f(halfWidth, halfHeight);
            recs.Origin = recs.Size / 2f;
            //rs.Texture = rsTex;
            _graphics.AddDrawable(recs, 0);

            //
            Texture cirsTex = _content.GetTexture(panPath);
            BCircleShape cirs = new BCircleShape(105f, 250u);
            cirs.Position = new Vector2f(halfWidth, halfHeight);
            cirs.Origin = new Vector2f(cirs.Radius, cirs.Radius);
            //cirs.Texture = cirsTex;
            _graphics.AddDrawable(cirs, 0);

            //
            Texture consTex = _content.GetTexture(panPath);
            BConvexShape cons = new BConvexShape(3u);
            cons.Position = new Vector2f(halfWidth, halfHeight);
            cons.SetPoint(0, new Vector2f(0f, -210f));
            cons.SetPoint(1, new Vector2f(105f, 0f));
            cons.SetPoint(2, new Vector2f(-105f, 0f));
            //cons.Texture = consTex;
            _graphics.AddDrawable(cons, 0);

            // Apply Keybindings
            _keys.Apply(_input.Keyboard);
            _mouse.Apply(_input.Mouse);
        }

        public override void BeginFrame()
        {
            _game.BeginFrame();
        }

        public override void Step()
        {
            _game.Step();
        }

        public override void Animate(float delta)
        {
            _game.Animate(delta);
        }

        public override void UnloadContent()
        {
            // Unload content
            //content.DEQUSET(this, @"intro\logo.png");

            // Remove Keybindings
            _keys.Remove(_input.Keyboard);
        }
    }
}
