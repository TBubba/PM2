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
using BubbasEngine.Engine.Graphics.Drawables;
using SFML.Graphics;

namespace PM2.GameContent.Game
{
    internal class PanGame
    {
        // Private
        private PanWorld _world;
        private PlayerPan[] _players;

        private bool _running;
        private PanGameArgs _args;

        private GraphicsLayer _layer;
        private BText _debugText;

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
            _world.WorldSize = new Vector2(16f, 9f);

            _running = true;

            //
            _world.PhysicsWorld.Gravity = new Vector2(0f, 0.001f);

            //
            _random = new Random();
            
            //
            _debugText = new BText();
            _debugText.Color = Color.White;
            _debugText.CharacterSize = 12u;

            // Create player container
            _players = new PlayerPan[4];
            Players = Array.AsReadOnly<PlayerPan>(_players);
        }

        // Initialize
        internal void Initialize(ContentManager content, GraphicsLayer layer)
        {
            // Keep ref
            _layer = layer;

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
            Pancake p = new Pancake(new BodyData() { Position = position * _world.WorldSize });

            _world.Entities.Add(p);
        }

        internal void SpawnPancake()
        {

        }

        // Player
        internal void MovePlayer(int index, Vector2 position)
        {
            _players[index].SetTargetPosition(position * _world.WorldSize);
        }

        // Content
        internal void LoadContent(ContentManager content)
        {
            //
            const string fontPath = @"Common\Fonts\AcidStructure.ttf";

            //
            content.RequestFont(fontPath);

            //
            _debugText.Font = content.GetFont(fontPath);

            //
            _layer.Renderables.Add(_debugText);
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

            //
            StringBuilder sb = new StringBuilder();

            //sb.AppendFormat("Player(s) {Count: {0}}", _players.Length);

            for (int i = 0; i < _players.Length; i++)
                if (_players[i] != null)
                    sb.AppendFormat("\t{0}: {1}", i, _players[i].ToString());

            _debugText.Text = sb.ToString();
        }
    }
}
