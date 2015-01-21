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
        private GameWorld _world;

        // Constructor(s)
        internal PanGame(PanGameArgs args)
        {
            _world = new GameWorld();
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
            _world.BeginFrame();
        }
        internal void Step()
        {
            _world.Step();
        }
        internal void Animate(float delta)
        {
            _world.Animate(delta);
        }
    }
}
