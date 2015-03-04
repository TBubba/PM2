using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PM2.Common
{
    internal class DynamicMenuItem : IGenericMenuItem
    {
        // Events
        internal event EventHandler OnSelected;
        internal event EventHandler OnDeselected;
        internal event EventHandler OnPushed;

        // Constructor(s)
        internal DynamicMenuItem()
        {
        }

        // Event Callers
        public void OnSelect()
        {
            OnSelected(this, new EventArgs());
        }
        public void OnDeselect()
        {
            OnDeselected(this, new EventArgs());
        }
        public void OnPush()
        {
            OnPushed(this, new EventArgs());
        }
    }
}
