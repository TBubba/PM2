using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;

namespace BubbasEngine.Engine.Content
{
    public class ContentManagerArgs
    {
        // Content Path
        public string ContentPath;
        public bool RelativePath;

        public bool SafeContentLoading;
        public string SafeTexturePath;
        public string SafeFontPath;
        public string SafeShaderPath;

        // Constructor(s)
        public ContentManagerArgs()
        {
            ContentPath = @"content\";
            RelativePath = true;
            SafeContentLoading = false;
            SafeTexturePath = @"missing\texture.png";
            SafeFontPath = @"missing\font.otf";
            SafeShaderPath = @"missing\texture.glsl";
        }
        public ContentManagerArgs(ContentManagerArgs args)
        {
            ContentPath = args.ContentPath;
            RelativePath = args.RelativePath;
            SafeContentLoading = args.SafeContentLoading;
            SafeTexturePath = args.SafeTexturePath;
            SafeFontPath = args.SafeFontPath;
            SafeShaderPath = args.SafeShaderPath;
        }
    }
}
