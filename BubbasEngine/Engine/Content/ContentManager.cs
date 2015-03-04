using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.Content.ContentTypes;
using BubbasEngine.Engine.GameStates;
using SFML.Graphics;

namespace BubbasEngine.Engine.Content
{
    public class ContentManager
    {
        // Content
        private string _contentPath;
        private ContentContainer<RefTexture> _textures; //private Dictionary<string, RefTexture> _textures;
        private Dictionary<string, RefFont> _fonts;
        private Dictionary<string, Dictionary<string, RefShader>> _shaders;

        // Safe Loading
        private bool _safeLoading;
        private RefTexture _safeTexture;
        private RefFont _safeFont;
        private RefShader _safeShader;

        // Engine content paths
        internal const string EnginePixelPath = @"pixel";

        // Constructor(s)
        internal ContentManager(ContentManagerArgs args)
        {
            // Content containers
            _textures = new ContentContainer<RefTexture>(); //_textures = new Dictionary<string, RefTexture>();
            _fonts = new Dictionary<string, RefFont>();
            _shaders = new Dictionary<string, Dictionary<string, RefShader>>();

            // Path
            if (args.RelativePath)
                _contentPath += AppDomain.CurrentDomain.BaseDirectory;
            _contentPath += args.ContentPath;

            // Safe
            _safeLoading = args.SafeContentLoading;
            if (_safeLoading)
            {
                _safeTexture = LoadTexture(_contentPath + args.SafeTexturePath);
                _safeFont = LoadFont(_contentPath + args.SafeFontPath);
                _safeShader = LoadShader(_contentPath + args.SafeShaderPath, _contentPath + args.SafeShaderPath);
            }

            // Engine content (content that comes with the engine)
            RefTexture pixelTex = new RefTexture(_contentPath + EnginePixelPath, 1, 1);
            //pixelTex.Update(new byte[] {0});
            _textures.Add(EnginePixelPath, pixelTex);
        }

        // Load
        private RefTexture LoadTexture(string path)
        {
            // Load
            RefTexture texture = new RefTexture(_contentPath + path);
            texture.Smooth = true;
                
            // Return
            return texture;
        }
        private RefTexture SafeLoadTexture(string path)
        {
            // Safe load
            RefTexture texture = null;
            try { texture = LoadTexture(path); }
            catch
            {
                texture = _safeTexture;
                GameConsole.WriteLine(string.Format("{0}: Texture failed to load (Path {1})", this.GetType().Name, path), GameConsole.MessageType.Error); // Debug
            }
                
            // Return
            return texture;
        }

        private RefFont LoadFont(string path)
        {
            // Load
            RefFont font = new RefFont(_contentPath + path);

            // Return
            return font;
        }
        private RefFont SafeLoadFont(string path)
        {
            // Safe load
            RefFont font = null;
            try { font = LoadFont(path); }
            catch
            {
                font = _safeFont;
                GameConsole.WriteLine(string.Format("{0}: Font failed to load (Path {1})", this.GetType().Name, path), GameConsole.MessageType.Error); // Debug
            }

            // Return
            return font;
        }

        private RefShader LoadShader(string vertPath, string fragPath)
        {
            // Load
            RefShader shader = new RefShader(_contentPath + vertPath, _contentPath + fragPath);

            // Return
            return shader;
        }
        private RefShader SafeLoadShader(string vertPath, string fragPath)
        {
            // Safe load
            RefShader shader = null;
            try { shader = LoadShader(vertPath, fragPath); }
            catch
            {
                shader = _safeShader;
                GameConsole.WriteLine(string.Format("{0}: Shader failed to load (Paths: Vert {1}; Frag {2})", this.GetType().Name, vertPath, fragPath), GameConsole.MessageType.Error); // Debug
            }

            // Return
            return shader;
        }

        // Request
        public void RequestTexture(string path, GameState state = null)
        {
            // Load content not loaded
            if (!_textures.ContainsKey(path))
            {
                // Load
                RefTexture texture;
                if (_safeLoading)
                    texture = SafeLoadTexture(path);
                else
                    texture = LoadTexture(path);

                // Add
                _textures[path] = texture;
                GameConsole.WriteLine(string.Format("{0}: Texture Loaded (Path {1})", this.GetType().Name, path)); // Debug
            }

            // Add subscriber
            _textures[path].Subscribe(state);
            GameConsole.WriteLine(string.Format("{0}: State subscribed to texture (Path {1})", this.GetType().Name, path)); // Debug
        }

        public void RequestFont(string path, GameState state = null)
        {
            // Load content not loaded
            if (!_fonts.ContainsKey(path))
            {
                // Load
                RefFont font;
                if (_safeLoading)
                    font = SafeLoadFont(path);
                else
                    font = LoadFont(path);

                // Add
                _fonts[path] = font;
                GameConsole.WriteLine(string.Format("{0}: Font Loaded (Path {1})", this.GetType().Name, path)); // Debug
            }

            // Add subscriber
            _fonts[path].Subscribe(state);
            GameConsole.WriteLine(string.Format("{0}: State subscribed to font (Path {1})", this.GetType().Name, path)); // Debug
        }

        public void RequestShader(string vertPath, GameState state = null)
        {
            RequestShader(vertPath, vertPath, state);
        }
        public void RequestShader(string vertPath, string fragPath, GameState state = null)
        {
            // If the VertPath is not loaded
            if (!_shaders.ContainsKey(vertPath))
                _shaders.Add(vertPath, new Dictionary<string, RefShader>());
            
            // If the FragPath is not loaded (aka the Shader)
            if (!_shaders[vertPath].ContainsKey(fragPath))
            {
                // Load
                RefShader shader;
                if (_safeLoading)
                    shader = SafeLoadShader(fragPath, vertPath);
                else
                    shader = LoadShader(vertPath, fragPath);

                // Add
                _shaders[vertPath][fragPath] = shader;

                // Compile
                shader.LoadShader();

                GameConsole.WriteLine(string.Format("{0}: Shader Loaded (Path {1})", this.GetType().Name, vertPath)); // Debug
            }

            // Add subscriber
            _shaders[vertPath][fragPath].Subscribe(state);
            GameConsole.WriteLine(string.Format("{0}: State subscribed to shader (Path {1})", this.GetType().Name, vertPath)); // Debug
        }

        // Dequest (worst name ever?)
        public void DequestTexture(string path, GameState state = null)
        {
            // Not found
            if (!_textures.ContainsKey(path))
            {
                GameConsole.WriteLine(string.Format("{0}: State tried to unsubscribe from non-existing texture (State {1}, Path {2})", this.GetType().Name, state.GetType().Name, path), GameConsole.MessageType.Warning); // Debug
                return;
            }

            // Remove subscriber
            RefTexture texture = _textures[path];
            texture.Unsubscribe(state);
            GameConsole.WriteLine(string.Format("{0}: State unsubscribed to texture (State {1}, Path {2})", this.GetType().Name, state.GetType().Name, path)); // Debug

            // Remove if no state subscribes
            if (texture.GetSubscriberCount() == 0)
            {
                texture.Dispose();
                _textures.Remove(path);
                GameConsole.WriteLine(string.Format("{0}: Texture removed due to no subscribers (Path {1})", this.GetType().Name, path)); // Debug
            }
        }

        public void DequestFont(string path, GameState state = null)
        {
            // Not found
            if (!_fonts.ContainsKey(path))
            {
                GameConsole.WriteLine(string.Format("{0}: State tried to unsubscribe from non-existing font (State {1}, Path {2})", this.GetType().Name, state.GetType().Name, path), GameConsole.MessageType.Warning); // Debug
                return;
            }

            // Remove subscriber
            RefFont font = _fonts[path];
            font.Unsubscribe(state);
            GameConsole.WriteLine(string.Format("{0}: State unsubscribed to font (State {1}, Path {2})", this.GetType().Name, state.GetType().Name, path)); // Debug

            // Remove if no state subscribes
            if (font.GetSubscriberCount() == 0)
            {
                font.Dispose();
                _fonts.Remove(path);
                GameConsole.WriteLine(string.Format("{0}: Font removed due to no subscribers (Path {1})", this.GetType().Name, path)); // Debug
            }
        }

        public void DequestShader(string vertPath, GameState state = null)
        {
            DequestShader(vertPath, vertPath, state);
        }
        public void DequestShader(string vertPath, string fragPath, GameState state = null)
        {
            // If the VertPath is not found
            if (!_shaders.ContainsKey(vertPath))
            {
                GameConsole.WriteLine(string.Format("{0}: State tried to unsubscribe from non-existing shader (State {1}, Paths: Vert {2}; Frag {3})", this.GetType().Name, state.GetType().Name, vertPath, fragPath), GameConsole.MessageType.Warning); // Debug
                return;
            }

            // If the FragPath is not found
            if (!_shaders[vertPath].ContainsKey(fragPath))
            {
                GameConsole.WriteLine(string.Format("{0}: State tried to unsubscribe from non-existing font (State {1}, Paths: Vert {2}; Frag {3})", this.GetType().Name, state.GetType().Name, vertPath, fragPath), GameConsole.MessageType.Warning); // Debug
                return;
            }

            // Remove subscriber
            RefShader shader = _shaders[vertPath][fragPath];
            shader.Unsubscribe(state);
            GameConsole.WriteLine(string.Format("{0}: State unsubscribed to shader (State {1}, Paths: Vert {2}; Frag {3})", this.GetType().Name, state.GetType().Name, vertPath, fragPath)); // Debug

            // Remove if no state subscribes
            //if (shader.GetSubscriberCount() == 0)
            //{
            //    shader.Dispose();
            //    _shaders[vertPath].Remove(fragPath);
            //    GameConsole.WriteLine(string.Format("{0}: Shader removed due to no subscribers (Paths: Vert {1}; Frag {2})", this.GetType().Name, vertPath, fragPath)); // Debug
            //
            //    if (_shaders[vertPath].Count == 0)
            //    {
            //        _shaders.Remove(vertPath);
            //        //GameConsole.WriteLine(string.Format("{0}: Shader vertex category removed due to no fragment shaders having any subscribers (Paths: Vert {1}; Frag {2})", this.GetType().Name, vertPath, fragPath)); // Debug
            //    }
            //}
        }

        public void DequestAll()
        {

        }
        public void DequestAllFromState(GameState state = null)
        {
            int length = _textures.Count;
            for (int i = 0; i < length; i++)
            {
                // TODO
            }
        }

        // Get
        public Texture GetTexture(string path)
        {
            return _textures[path];
        }

        public Font GetFont(string path)
        {
            return _fonts[path];
        }

        public Shader GetShader(string vertPath)
        {
            return _shaders[vertPath][vertPath].CreateShader();
        }
        public Shader GetShader(string vertPath, string fragPath)
        {
            return _shaders[vertPath][fragPath].CreateShader();
        }
    }
}
