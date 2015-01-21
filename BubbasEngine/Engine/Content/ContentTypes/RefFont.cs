using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using BubbasEngine.Engine.GameStates;

namespace BubbasEngine.Engine.Content.ContentTypes
{
    internal class RefFont : Font, IContent
    {
        // Private
        private string _filename;
        private List<GameState> _subs;

        // Constructor(s)
        internal RefFont(string filename)
            : base(filename)
        {
            _filename = filename;
            _subs = new List<GameState>();
        }

        // Filename
        public string GetFilename()
        {
            return _filename;
        }

        // Subscribe
        public bool Subscribe(GameState state)
        {
            // Existing subscribers can't subscribe multiple times
            if (_subs.Contains(state))
            {
                GameConsole.WriteLine(string.Format("{0}: State tried to subsribe multiple times to the same content (State {1})", this.GetType().Name, state.GetType().Name), GameConsole.MessageType.Warning); // Debug
                return false;
            }

            // Add subscriber
            _subs.Add(state);
            return true;
        }
        public bool Unsubscribe(GameState state)
        {
            bool s = _subs.Remove(state);

            if (!s)
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to remove a subscriber that isn't subscribing (State {1})", this.GetType().Name, state.GetType().Name), GameConsole.MessageType.Error); // Debug
                return false;
            }
            return true;
        }

        public int GetSubscriberCount()
        {
            return _subs.Count;
        }
    }
}
