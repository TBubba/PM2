using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;
using BubbasEngine.Engine.Windows;

namespace BubbasEngine.Engine.Input.Devices
{
    internal enum InputDeviceType
    {
        GamePad,
        Mouse,
        Keyboard,
        Other,

        Count
    }

    public abstract class InputDevice
    {
        //
        internal abstract InputDeviceType GetDeviceType();

        // Update
        internal abstract void BeginFrame();

        // Update
        internal abstract void Update(bool focus);

        // Window
        internal abstract void ApplyWindow(GameWindow window);
        internal abstract void RemoveWindow(GameWindow window);
    }
}

