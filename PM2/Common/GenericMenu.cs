using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PM2.Common
{
    internal class GenericMenu
    {
        // Vars
        private int _selected;
        private IGenericMenuItem[] _items;

        // Public
        public int Selected
        { get { return _selected; } }
        public IGenericMenuItem SelectedItem
        { get { return _items[_selected]; } }
        public int Length
        { get { return _items.Length; } }

        // Constructor
        public GenericMenu()
        {
        }
        public GenericMenu(IGenericMenuItem[] items)
        {
            int length = items.Length;

            // Create new array
            _items = new IGenericMenuItem[length];

            // Copy to new array
            for (int i = 0; i < length; i++)
                _items[i] = items[i];
        }

        // Select
        public void Select(int index)
        {
            // Deselect the current item
            if (_selected >= 0)
                _items[_selected].OnDeselect();

            // Select the new item
            if (index >= 0 && index < _items.Length)
                _items[index].OnSelect();

            // Set the new selected index
            _selected = index;
        }
        public void SelectNext()
        {
            // Return if there are no items
            if (_items.Length == 0)
                return;

            // Deselect
            _items[_selected].OnDeselect();

            // Loop to the first item
            if (_selected + 1 >= _items.Length)
            {
                // Select
                if (_items.Length > 0)
                    _items[0].OnSelect();

                _selected = 0;
            }
            else // Go to the next item
            {
                // Select
                _items[_selected + 1].OnSelect();

                _selected++;
            }
        }
        public void SelectPrev()
        {
            // Return if there are no items
            if (_items.Length == 0)
                return;

            // Deselect
            _items[_selected].OnDeselect();

            // Loop to the last item
            if (_selected - 1 < 0)
            {
                _items[_items.Length - 1].OnSelect();

                _selected = _items.Length - 1;
            }
            else // Go to the next item
            {
                // Select
                _items[_selected - 1].OnSelect();

                _selected--;
            }
        }

        // Add Items
        public void AddItem(IGenericMenuItem item)
        {
            int length = _items.Length;

            // Create new array
            IGenericMenuItem[] newItems = new IGenericMenuItem[length + 1];

            // Copy old items
            for (int i = 0; i < length; i++)
                newItems[i] = _items[i];

            // Add new item
            newItems[length + 1] = item;

            // Replace old array
            _items = newItems;
        }
        public void AddItemRange(IGenericMenuItem[] items)
        {
            int length = _items.Length;
            int length2 = items.Length;

            // Create new array
            IGenericMenuItem[] newItems = new IGenericMenuItem[length + items.Length];

            // Copy old items
            for (int i = 0; i < length; i++)
                newItems[i] = _items[i];

            // Add new item
            for (int i = length; i < length2; i++)
                newItems[i] = items[i];

            // Replace old array
            _items = newItems;
        }

        // Get Items
        public IGenericMenuItem GetItem(int index)
        {
            return _items[index];
        }
        public IGenericMenuItem[] GetItemRange(int index, int length)
        {
            IGenericMenuItem[] ret = new IGenericMenuItem[length];
            for (int i = 0; i < _items.Length; i++)
                ret[i] = _items[index + i];

            return ret;
        }

        // Push
        public void Push(int index)
        {
            _items[index].OnPush();
        }
        public void PushSelected()
        {
            _items[_selected].OnPush();
        }
    }
}
