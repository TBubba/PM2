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
using BubbasEngine.Engine.Physics.Common;

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
            // Keyboard
            _keys = new KeyboardBindingCollection();

            // Mouse
            _mouse = new MouseBindingCollection();
        }

        //
        public override void Initialize()
        {
            // Initialize game
            _game = new PanGame(new PanGameArgs()
            {
                Players = 1,
                GameMode = 0,
                StepTime = 1f / _time.StepsPerSecond
            });

            _game.Initialize(_content, _graphics.Layers);
        }

        public override void LoadContent()
        {
            // Define content paths
            const string panPath = @"GameContent\Game\Pans\StandardPan.png";

            // Request content
            _content.RequestTexture(panPath, this);

            // Define positioning
            float halfWidth = (float)(_graphics.RenderWidth / 2u);
            float halfHeight = (float)(_graphics.RenderHeight / 2u);

            //
            _keys.AddOnPressed(Keyboard.Key.Escape,
                new KeyboardBinding(new KeyboardInputDele(delegate
                {
                    // Pause or Resume
                    if (_game.Running)
                        _game.Pause();
                    else
                        _game.Run();
                })));
            _keys.AddOnPressed(Keyboard.Key.F3,
                new KeyboardBinding(new KeyboardInputDele(delegate
                {
                    // Hide or Show debug
                    _game.ShowDebug = !_game.ShowDebug;
                })));

            //
            _mouse.AddOnPressed(Mouse.Button.Left, new MouseButtonBinding((x, y) =>
            {
                for (int i = 0; i < 3; i++ )
                    _game.CreatePancake(new Vector2((float)x / (float)_graphics.RenderWidth, (float)y / (float)_graphics.RenderHeight - 0.1f - (float)i * 0.035f));
            }));
            _mouse.AddOnMoved(new MouseMoveBinding((x, y) =>
            {
                _game.MovePlayer(0, new Vector2(x, y) / new Vector2(_graphics.RenderWidth, _graphics.RenderHeight));
            }));

            // Load world content
            _game.LoadContent();

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

            // Unload
            _game.UnloadContent();

            // Remove Keybindings
            _keys.Remove(_input.Keyboard);
        }
    }
}
