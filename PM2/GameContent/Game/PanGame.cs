using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.GameWorlds;
using BubbasEngine.Engine.Content;
using PM2.GameContent.Game.Entities;
using SFML.Window;
using BubbasEngine.Engine.Graphics;
using BubbasEngine.Engine.Input;
using System.Collections.ObjectModel;
using BubbasEngine.Engine.Physics.Common;

namespace PM2.GameContent.Game
{
    internal class PanGame
    {
        // Private
        private PanWorld _world;
        private PlayerPan[] _players;

        private bool _running;
        private PanGameArgs _args;

        private Random _random;

        // Internal
        internal ReadOnlyCollection<PlayerPan> Players;
        internal int PlayerCount
        { get { return _players.Length; } }

        internal bool Running
        { get { return _running; } }

        // Constructor(s)
        internal PanGame(PanGameArgs args)
        {
            //
            _args = args;

            //
            _world = new PanWorld(args.StepTime);
            _world.PhysicsWorld.Gravity = new Vector2(0f, 0.001f);

            _running = true;

            //
            _random = new Random();

            // Create player container
            _players = new PlayerPan[4];
            Players = Array.AsReadOnly<PlayerPan>(_players);
        }

        // Initialize
        internal void Initialize(ContentManager content, GraphicsLayer layer)
        {
            // Initialize world
            _world.Initialize(content, layer);

            //
            int length = _args.Players;
            for (int i = 0; i < length; i++)
            {
                PlayerPan player = new PlayerPan();

                _players[i] = player;
                _world.Entities.Add(player);
            }
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
            Pancake p = new Pancake(new BodyData() { Position = position });

            _world.Entities.Add(p);
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
