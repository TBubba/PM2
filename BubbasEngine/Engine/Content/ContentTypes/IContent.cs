using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.GameStates;

namespace BubbasEngine.Engine.Content.ContentTypes
{
    internal interface IContent
    {
        string GetFilename();

        // Subscribe
        bool Subscribe(GameState state);
        bool Unsubscribe(GameState state);

        int GetSubscriberCount();
    }
}
