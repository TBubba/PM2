using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbasEngine.Engine.Input.Devices.Mouses
{
    public delegate void MouseMoveDele(int x, int y);

    public class MouseMoveBinding
    {
        // Private
        private MouseMoveDele _method;

        // Constructor(s)
        public MouseMoveBinding(MouseMoveDele method)
        {
            _method = method;
        }

        // Method
        internal void CallMethod(int x, int y)
        {
            _method(x, y);
        }
    }
}
