using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbasEngine.Engine.Input.Devices.Mouses
{
    public delegate void MouseWheelDele(int x, int y, int delta);

    public class MouseWheelBinding
    {
        // Private
        private MouseWheelDele _method;

        // Constructor(s)
        public MouseWheelBinding(MouseWheelDele method)
        {
            _method = method;
        }

        // Method
        internal void CallMethod(int delta, int x, int y)
        {
            _method(delta, x, y);
        }
    }
}
