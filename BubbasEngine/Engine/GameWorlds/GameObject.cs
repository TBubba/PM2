using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbasEngine.Engine.GameWorlds
{
    public class GameObject
    {
        // Private & Protected
        protected GameWorld _world
        { get; private set; }

        // Constructor(s)
        public GameObject()
        {
        }

        // World
        internal void AddToWorld(GameWorld world)
        {
            if (_world != null)
            {
                throw new Exception("added gameobject to another world without removing it from the first");
            }

            _world = world;
        }
        internal void RemoveFromWorld()
        {
            _world = null;
        }
    }
}
