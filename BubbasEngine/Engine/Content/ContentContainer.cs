using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbasEngine.Engine.Content
{
    public class EntryAddedEventArgs : EventArgs
    {
        public string Key;
        public object Entry;

        public EntryAddedEventArgs()
        {
        }
        public EntryAddedEventArgs(string key, object entry)
        {
            Key = key;
            Entry = entry;
        }
    }
    public class EntryRemovedEventArgs : EventArgs
    {
        public string Key;
        public object Entry;

        public EntryRemovedEventArgs()
        {
        }
        public EntryRemovedEventArgs(string key, object entry)
        {
            Key = key;
            Entry = entry;
        }
    }
    public class EntryChangedEventArgs : EventArgs
    {
        public string Key;
        public object Entry;
        public object OldEntry;

        public EntryChangedEventArgs()
        {
        }
        public EntryChangedEventArgs(string key, object entry, object oldEntry)
        {
            Key = key;
            Entry = entry;
            OldEntry = oldEntry;
        }
    }

    public class ContentContainer<T>
    {
        // Private
        private Dictionary<string, T> _entries;

        // Public
        public int Count
        { get { return _entries.Count; } }

        // Events
        public event EventHandler<EntryAddedEventArgs> OnEntryAdded;
        public event EventHandler<EntryRemovedEventArgs> OnEntryRemoved;
        public event EventHandler<EntryChangedEventArgs> OnEntryChanged;

        // Container
        public T this[string key]
        {
            get
            {
                if (!_entries.ContainsKey(key))
                    throw new Exception(string.Format("content not found (\"{0}\" : {1})", key, typeof(T).Name));
                return _entries[key];
            }
            internal set
            {
                if (_entries.ContainsKey(key))
                    Set(key, value);
                else
                    Add(key, value);
            }
        }

        // Constructor(s)
        internal ContentContainer()
        {
            // Create private container
            _entries = new Dictionary<string,T>();
        }

        // Handle Entries
        public bool Add(string key, T entry)
        {
            // Abort if parameter is null
            if (entry == null)
            {
                throw new Exception("entry must not be null");
                //GameConsole.WriteLine(string.Format("{0}: Tried to add a non-existing {1} (obj = null)", GetType().Name, typeof(T).Name), GameConsole.MessageType.Error); // Debug
                //return false;
            }

            // Abort if key is already contained
            if (ContainsKey(key))
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to add an {1} that is already contained (Name {2})", GetType().Name, typeof(T).Name, entry.GetType().Name), GameConsole.MessageType.Error); // Debug
                return false;
            }

            // Add entry to container
            _entries.Add(key, entry);

            //
            //_world.OnEntityAdded(entry);

            // Call event
            if (OnEntryAdded != null)
                OnEntryAdded(this, new EntryAddedEventArgs(key, entry));

            // Success
            return true;
        }

        public bool Set(string key, T entry)
        {
            // Abort if parameter is null
            if (entry == null)
            {
                throw new Exception("entry must not be null");
                //GameConsole.WriteLine(string.Format("{0}: Tried to add a non-existing {1} (obj = null)", GetType().Name, typeof(T).Name), GameConsole.MessageType.Error); // Debug
                //return false;
            }

            // Abort if key is not contained
            if (!ContainsKey(key))
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to add an {1} that is already contained (Name {2})", GetType().Name, typeof(T).Name, entry.GetType().Name), GameConsole.MessageType.Error); // Debug
                return false;
            }

            //
            object oldEntry = _entries[key];

            // Set the entry to the new one
            _entries[key] = entry;

            // Call event
            if (OnEntryChanged != null)
                OnEntryChanged(this, new EntryChangedEventArgs(key, entry, oldEntry));

            // Success
            return true;
        }

        public bool Remove(string key)
        {
            // Abort if parameter is null
            if (key == null)
                throw new Exception("key must not be null");

            // Abort if entry is not found
            if (!ContainsKey(key))
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to remove a {1} that is not in the container (Name {2})", GetType().Name, typeof(T).Name, key.GetType().Name), GameConsole.MessageType.Error); // Debug
                return false;
            }

            //
            object entry = _entries[key];

            // Remove entry from container
            _entries.Remove(key);

            // 
            //_world.OnEntityRemoved(entry);

            // Call event
            if (OnEntryRemoved != null)
                OnEntryRemoved(this, new EntryRemovedEventArgs(key, entry));

            // Success
            return true;
        }

        public int RemoveAll() // TO DO
        {
            // Abort if the container is empty
            if (_entries.Count == 0)
            {
                GameConsole.WriteLine(string.Format("{0}: Tried to remove all {1}s while none were contained", GetType().Name, typeof(T).Name), GameConsole.MessageType.Error); // Debug
                return 0;
            }

            // Queue all entites for removal
            Action rem = new Action(delegate { });
            int length = _entries.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                // Keep entry and index
                int index = i;
                //T entry = _entries[index];

                // Create action that removes selected entry
                rem += delegate
                {
                    // Remove entry from container
                    //_entries.RemoveAt(index);

                    // Tell world that this entry was removed
                    //_world.OnEntityRemoved(entry);
                };
            }

            // Remove all entities
            rem();
            GameConsole.WriteLine(string.Format("{0}: Removed all {1}s (Count {2})", GetType().Name, typeof(T).Name, length)); // Debug

            // Return amount of entities removed
            return length;
        }

        // Contains
        internal bool ContainsKey(string key)
        {
            return _entries.ContainsKey(key);
        }
        internal bool ContainsEntry(T entry)
        {
            return _entries.ContainsValue(entry);
        }
    }
}
