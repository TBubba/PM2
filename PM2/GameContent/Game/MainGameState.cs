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
            _mouse.AddOnPressed(Mouse.Button.Left, new MouseButtonBinding((x, y) =>
            {
                _game.CreatePancake(new Vector2f(x, y));
            }));
        }

        //
        public override void LoadContent()
        {
            // Initialize game
            _game.Initialize(_content, _graphics);

            // Define content paths
            const string panPath = @"GameContent\Game\Pans\StandardPan.png";

            // Request content
            _content.RequestTexture(this, panPath);

            // Define positioning
            float halfWidth = (float)(_graphics.RenderWidth / 2u);
            float halfHeight = (float)(_graphics.RenderHeight / 2u);

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
