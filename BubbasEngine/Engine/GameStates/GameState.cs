using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.Content;
using BubbasEngine.Engine.Windows;
using BubbasEngine.Engine.Input;
using BubbasEngine.Engine.Graphics;
using BubbasEngine.Engine.Timing;

namespace BubbasEngine.Engine.GameStates
{
    public abstract class GameState
    {
        // Private
        internal protected GameEngine _engine
        { get; private set; }

        // Protected
        protected ContentManager _content
        { get { return _engine.Content; } }
        protected InputManager _input
        { get { return _engine.Input; } }
        protected GameStateManager _states
        { get { return _engine.States; } }
        protected GameWindow _window
        { get { return _engine.Window; } }
        protected GraphicsRenderer _graphics
        { get { return _engine.Graphics; } }
        protected TimeManager _time
        { get { return _engine.Time; } }

        // Constructor(s)
        public GameState()
        {
        }

        // Internal
        internal void Setup(GameEngine engine)
        {
            _engine = engine;
        }

        // Functions
        public abstract void LoadContent();
        public abstract void BeginFrame();
        public abstract void Step();
        public abstract void Animate(float delta);
        public abstract void UnloadContent();
    }
}
