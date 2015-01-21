using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;
using BubbasEngine.Engine.Windows;

namespace BubbasEngine.Engine.Input.Devices.GamePads
{
    class GamePadDevice : InputDevice
    {

        //
        internal override InputDeviceType GetDeviceType()
        {
            return InputDeviceType.GamePad;
        }

        //
        internal override void BeginFrame()
        {

        }

        //
        internal override void Update(bool focus)
        {

        }

        //
        internal override void ApplyWindow(GameWindow window)
        {

        }
        internal override void RemoveWindow(GameWindow window)
        {

        }
    }
}
