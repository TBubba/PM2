using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.GameWorlds;
using BubbasEngine.Engine.Content;
using BubbasEngine.Engine.Physics.Common;
using PM2.GameContent.Game.Entities;

namespace PM2.GameContent.Game
{
    internal class PanGame
    {
        // Private
        private PanWorld _world;
        private bool _running;

        private Random _random;

        // Constructor(s)
        internal PanGame(PanGameArgs args)
        {
            //
            _world = new PanWorld();
            _running = true;

            //
            _random = new Random();
        }

        // Run
        internal void Run()
        {
            _running = true;
        }
        internal void Pause()
        {
            _running = false;
        }

        // Create
        internal void CreatePancake(Vector2 position)
        {
            _world.Entities.Add(new Pancake(position));
        }
        internal void CreatePlayer()
        {

        }

        internal void SpawnPancake()
        {

        }

        // Content
        internal void LoadContent(ContentManager content)
        {

        }
        internal void UnloadContent(ContentManager content)
        {

        }

        // Game Loop
        internal void BeginFrame()
        {
            if (_running)
                _world.BeginFrame();
        }
        internal void Step()
        {
            if (_running)
                _world.Step();
        }
        internal void Animate(float delta)
        {
            if (_running)
                _world.Animate(delta);
        }
    }
}
