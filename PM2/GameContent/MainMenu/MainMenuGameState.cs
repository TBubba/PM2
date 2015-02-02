using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.GameStates;
using BubbasEngine.Engine.Graphics.Drawables;
using BubbasEngine.Engine.Input.Devices.Keyboards;
using SFML.Window;
using SFML.Graphics;
using BubbasEngine.Engine;

namespace PM2.GameContent.MainMenu
{
    internal class MainMenuGameState : GameState
    {
        private BSprite _logo;

        private float _time;

        private KeyboardBindingCollection _keys;

        //
        internal MainMenuGameState()
        {
            //
            _keys = new KeyboardBindingCollection();
            _keys.AddOnPressed(Keyboard.Key.Space,
                new KeyboardBinding(new KeyboardInputDele(delegate
                    {

                    })));
        }

        //
        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            // Define content paths
            const string logoPath = @"GameContent\Intro\Logo.png";

            // Request content
            _content.RequestTexture(this, logoPath);

            // Define positioning
            float halfWidth = (float)(_graphics.RenderWidth / 2u);
            float halfHeight = (float)(_graphics.RenderHeight / 2u);

            // Load content
            Texture logoTexture = _content.GetTexture(logoPath);

            // Set up graphics
            _logo = new BSprite(logoTexture);
            _logo.Position = new Vector2f(halfWidth, halfHeight);
            _logo.Origin = new Vector2f(logoTexture.Size / 2u);
            _layer.Renderables.Add(_logo);

            // Apply Keybindings
            _keys.Apply(_input.Keyboard);
        }

        public override void BeginFrame()
        {
            if (_time > 0.5f)
            {
                // Go to main game
                _states.RemoveState(this);
                _states.AddState(new Game.MainGameState());
            }
        }

        public override void Step()
        {
        }

        public override void Animate(float delta)
        {
            // Update time                                           
            _time += delta / 1000f;
        }

        public override void UnloadContent()

        {
            // Unload content
            //content.DEQUSET(this, @"intro\logo.png");

            // Remove graphics
            _layer.Renderables.Remove(_logo);

            // Remove Keybindings
            _keys.Remove(_input.Keyboard);
        }
    }
}
