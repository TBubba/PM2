using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;

namespace BubbasEngine.Engine.Input.Devices.Keyboards
{
    public delegate void KeyboardInputDele();

    public class KeyboardBinding
    {
        // Private
        private KeyboardInputDele _method;
        private Keyboard.Key[] _modifiers;

        // Constructor(s)
        public KeyboardBinding(KeyboardInputDele method)
        {
            _method = method;
        }
        public KeyboardBinding(KeyboardInputDele method, Keyboard.Key[] modifiers)
        {
            _method = method;
            _modifiers = modifiers;
        }

        // Modifiers
        internal Keyboard.Key[] GetModifiers()
        {
            return _modifiers;
        }

        // Method
        internal void CallMethod()
        {
            _method();
        }
    }
}
