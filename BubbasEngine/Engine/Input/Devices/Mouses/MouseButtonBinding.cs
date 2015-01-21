using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbasEngine.Engine.Input.Devices.Mouses
{
    public delegate void MouseButtonDele(int x, int y);

    public class MouseButtonBinding
    {
        // Private
        private MouseButtonDele _method;

        // Constructor(s)
        public MouseButtonBinding(MouseButtonDele method)
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
