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
using PM2.Common;
using BubbasEngine.Engine.Input.Devices.Mouses;

namespace PM2.GameContent.MainMenu
{
    internal class MainMenuGameState : GameState
    {
        //
        private KeyboardBindingCollection _keys;
        private MouseBindingCollection _mouse;

        private bool _pushCurrent;
        private bool _moveUp;
        private bool _moveDown;

        private GenericMenu _menu;

        private float _time;

        // Constructor(s)
        internal MainMenuGameState()
        {
            // Create menu with items (buttons)
            _menu = new GenericMenu(new MainMenuButton[] {
                new MainMenuButton("Singleplayer", delegate
                    {
                        BubbasEngine.Engine.GameConsole.WriteLine("You clicked on Singleplayer");
                        _states.RemoveState(this);
                        _states.AddState(new PM2.GameContent.Game.MainGameState());
                    }),
                new MainMenuButton("Multiplayer", delegate
                    {
                        /* Multiplayer */
                        BubbasEngine.Engine.GameConsole.WriteLine("You clicked on Multiplayer");
                    }),
                new MainMenuButton("Settings", delegate
                    {
                        /* Settings */
                        BubbasEngine.Engine.GameConsole.WriteLine("You clicked on Settings");
                    }),
                new MainMenuButton("Exit", delegate
                    {
                        BubbasEngine.Engine.GameConsole.WriteLine("You clicked on Exit");
                        _engine.End();
                    })
                });

            // Set up keybindings
            _keys = new KeyboardBindingCollection();
            _keys.AddOnPressed(Keyboard.Key.Space,
                new KeyboardBinding(new KeyboardInputDele(delegate
                {
                    _pushCurrent = true;
                })));

            _keys.AddOnPressed(Keyboard.Key.Up,
                new KeyboardBinding(new KeyboardInputDele(delegate
                {
                    _moveUp = true;
                })));
            _keys.AddOnPressed(Keyboard.Key.W,
                new KeyboardBinding(new KeyboardInputDele(delegate
                {
                    _moveUp = true;
                })));

            _keys.AddOnPressed(Keyboard.Key.Down,
                new KeyboardBinding(new KeyboardInputDele(delegate
                {
                    _moveDown = true;
                })));
            _keys.AddOnPressed(Keyboard.Key.S,
                new KeyboardBinding(new KeyboardInputDele(delegate
                {
                    _moveDown = true;
                })));

            // Set up mouse-interaction
            _mouse = new MouseBindingCollection();
            _mouse.AddOnPressed(Mouse.Button.Left, new MouseButtonBinding((x, y) =>
            {
                int rx = (int)((float)x / _graphics.Scale.X);
                int ry = (int)((float)y / _graphics.Scale.Y);

                int length = _menu.Length;
                for (int i = 0; i < length; i++)
                {
                    MainMenuButton button = (MainMenuButton)_menu.GetItem(i);
                    if (button.Contains(rx, ry))
                    {
                        _menu.Select(i);
                        _menu.PushSelected();
                        break;
                    }
                }
            }));
            _mouse.AddOnMoved(new MouseMoveBinding((x, y) =>
            {
                int length = _menu.Length;
                for (int i = 0; i < length; i++)
                {
                    MainMenuButton button = (MainMenuButton)_menu.GetItem(i);
                    if (button.Contains(x, y))
                    {
                        _menu.Select(i);
                        break;
                    }
                }
            }));
        }

        //
        public override void Initialize()
        {

        }

        public override void LoadContent()
        {
            // Define content paths
            const string outlineShaderPath = @"Common\Shaders\Outline";
            const string buttonTexturePath = @"GameContent\Menu\Button.png";
            const string buttonFontPath = @"Common\Fonts\Anklada.ttf";
            const string titleTexturePath = @"GameContent\Menu\Title.png";

            // Requst content
            _content.RequestShader(outlineShaderPath + ".vert", outlineShaderPath + ".frag", this);
            _content.RequestTexture(buttonTexturePath, this);
            _content.RequestFont(buttonFontPath, this);
            _content.RequestTexture(titleTexturePath, this);

            // Load content
            Texture buttonTexture = _content.GetTexture(buttonTexturePath);
            Font buttonFont = _content.GetFont(buttonFontPath);

            Texture titleTexture = _content.GetTexture(titleTexturePath);

            // Create drawables for the menu items
            int length = _menu.Length;
            for (int i = 0; i < length; i++)
            {
                // Get resources
                Shader shader = _content.GetShader(outlineShaderPath + ".vert", outlineShaderPath + ".frag");

                // Create
                MainMenuButton button = ((MainMenuButton)_menu.GetItem(i));

                Vector2f position = new Vector2f(
                    (float)(_graphics.RenderWidth / 2u),
                    (float)((_graphics.RenderHeight / (length + 3)) * (i + 3)));

                button.SetDrawable(position, buttonTexture, shader, buttonFont);

                // Add drawable
                button.AddDrawables(_layer);
            }

            // Select menu item
            _menu.Select(0);

            // Apply Keybindings
            _keys.Apply(_input.Keyboard);
            _mouse.Apply(_input.Mouse);
        }

        public override void BeginFrame()
        {
        }

        public override void Step()
        {
        }

        public override void Animate(float delta)
        {
        }

        public override void UnloadContent()

        {
            // Unload content
            //content.DEQUSET(this, @"intro\logo.png");

            // Remove graphics
            //_layer.Renderables.Remove(_logo);

            //
            int length = _menu.Length;
            for (int i = 0; i < length; i++)
            {
                ((MainMenuButton)_menu.GetItem(i)).RemoveDrawables(_layer);
            }

            // Remove Keybindings
            _keys.Remove(_input.Keyboard);
            _mouse.Remove(_input.Mouse);
        }
    }
}
