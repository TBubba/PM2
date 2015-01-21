using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.GameStates;
using SFML.Graphics;
using System.IO;

namespace BubbasEngine.Engine.Content.ContentTypes
{
    internal class RefShader : IContent
    {
        // Private
        private string _filenameVert;
        private string _filenameFrag;
        private List<GameState> _subs;

        private byte[] _vert;
        private byte[] _frag;

        // Constructor(s)
        internal RefShader(string filename)
        {
            _filenameVert = filename;
            _filenameFrag = filename;
            _subs = new List<GameState>();
        }
        internal RefShader(string filenameVert, string filenameFrag)
        {
            _filenameVert = filenameVert;
            _filenameFrag = filenameFrag;
            _subs = new List<GameState>();
        }

        // Filename
        public string GetFilename()
        {
            return _filenameVert;
        }

        // Shader
        internal void LoadShader()
        {
            _vert = LoadFile(_filenameVert);
            _frag = LoadFile(_filenameFrag);
        }
        private byte[] LoadFile(string path)
        {
            // Open stream
            FileStream stream = new FileStream(path, FileMode.Open);
            byte[] buffer = new byte[(int)stream.Length];

            // Load
            stream.Read(buffer, 0, buffer.Length);

            // Close
            stream.Close();

            return buffer;
        }

        internal Shader CreateShader()
        {
            // Open stream
            MemoryStream v = new MemoryStream(_vert);
            MemoryStream f = new MemoryStream(_frag);

            // Load
            Shader shader = new Shader(v, f);

            // Close
            v.Close();
            f.Close();

            return shader;
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
