using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine;
using PM2.GameContent;

namespace PM2
{
    class Program
    {
        static void Main(string[] args)
        {
            // GameConsole
            GameConsole.WindowWidth = 100;
            GameConsole.WindowHeight = 35;

            // Temporary testing values
            const float resScale = 1f;
            const uint winWidth = 1280u;
            const uint winHeight = 720u;

            //
            GameArgs gameArgs = new GameArgs()
            {
                Content = new BubbasEngine.Engine.Content.ContentManagerArgs() // Content
                {
                    ContentPath = @"content\",
                    RelativePath = true
                },
                Debug = new BubbasEngine.Engine.Debugging.DebugArgs() // Debug
                {
                    Activated = true,
                    DebugFontPath = @""
                },
                Input = new BubbasEngine.Engine.Input.InputSettingsArgs() // Input 
                {
                    FocusedInputOnly = true,
                    FocusedGamePadInputOnly = false,
                    FocusedKeyoardInputOnly = false,
                    FocusedMouseInputOnly = false
                },
                Graphics = new BubbasEngine.Engine.Graphics.GraphicsRendererArgs() // Graphics
                {
                    ResolutionWidth = (int)((float)winWidth * resScale),
                    ResolutionHeight = (int)((float)winHeight * resScale)
                },
                Time = new BubbasEngine.Engine.Timing.TimeManagerArgs() // Time
                {
                    StepsPerSecond = 60
                },
                Window = new BubbasEngine.Engine.Windows.GameWindowArgs() // Window
                {
                    CreateWindow = true,
                    WindowWidth = winWidth,
                    WindowHeight = winHeight,
                    WindowTitle = "PannkaksMästarN 2: The Revenge of SmetMästarN",
                    WindowStyle = SFML.Window.Styles.Close
                }
            };

            // Engine
            GameEngine game = new GameEngine(gameArgs);

            // Run Game
            game.Run(new LaunchGameState());
        }
    }
}
