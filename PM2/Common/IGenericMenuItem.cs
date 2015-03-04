using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PM2.Common
{
    internal interface IGenericMenuItem
    {
        /// <summary>
        /// Occurs when the item is selected
        /// </summary>
        void OnSelect();
        /// <summary>
        /// Occurs when the item is deselected
        /// </summary>
        void OnDeselect();

        /// <summary>
        /// Occurs when the item is pushed
        /// </summary>
        void OnPush();
    }
}
