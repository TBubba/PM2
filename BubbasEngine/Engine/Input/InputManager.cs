using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;
using BubbasEngine.Engine.Input.Devices.Mouses;
using BubbasEngine.Engine.Input.Devices;
using BubbasEngine.Engine.Windows;
using BubbasEngine.Engine.Input.Devices.Keyboards;

namespace BubbasEngine.Engine.Input
{
    public class InputManager
    {
        // Private
        private KeyboardDevice _keyboard;
        private MouseDevice _mouse;
        private Stack<InputDevice> _devices;

        private InputSettings _settings;
        private GameWindow _window;

        private Action _beginFrame;

        // Internal
        internal InputSettings Settings
        { get { return _settings; } }

        // Public devices (Not sure why, really)
        public MouseDevice Mouse
        { get { return _mouse; } }
        public KeyboardDevice Keyboard
        { get { return _keyboard; } }
        
        // Constructor(s)
        internal InputManager(InputSettingsArgs args)
        {
            // Create standard devices
            _keyboard = new KeyboardDevice();
            _mouse = new MouseDevice();

            // Create room for additional devices
            _devices = new Stack<InputDevice>();

            // Settings
            _settings = new InputSettings(args);
        }

        // Handle Window
        internal void SetWindow(GameWindow window)
        {
            // Remove previous window
            if (_window != null)
                RemoveWindow();

            // 
            _window = window;

            // 
            _keyboard.ApplyWindow(_window);
            _mouse.ApplyWindow(_window);
        }
        internal void RemoveWindow()
        {
            // 
            _keyboard.RemoveWindow(_window);
            _mouse.RemoveWindow(_window);

            //
            _window = null;
        }

        // Handle Devices
        public bool AddDevice(InputDevice device)
        {
            // Adding the same device multiple times is not allowed
            if (ContainsDevice(device))
                return false;

            // Add device on next update
            _beginFrame += delegate
                {
                    if (!ContainsDevice(device))
                        _devices.Push(device);
                };
            return true;
        }
        public bool RemoveDevice(InputDevice device)
        {
            // Can't remove devices that is not in the container
            if (!ContainsDevice(device))
                return false;

            // Remove device on next update
            _beginFrame += delegate
                {
                    if (ContainsDevice(device))
                    {
                        device.RemoveWindow(_window);
                        Remove(device);
                    }
                };
            return true;
        }

        // Device Container
        private bool ContainsDevice(InputDevice device)
        {
            Stack<InputDevice> stack = new Stack<InputDevice>();
            bool success = false;

            // Look through all devices
            int length = _devices.Count;
            for (int i = 0; i < length; i++)
            {
                // Get device ref and move it to the other stack
                InputDevice d = _devices.Pop();
                stack.Push(d);

                // If it is what you're looking for stop looking
                if (d == device)
                {
                    success = true;
                    break;
                }
            }

            // Move them back to the main stack
            length = stack.Count;
            for (int i = 0; i < length; i++)
                stack.Push(stack.Pop());

            // Return whether or not it was found
            return success;
        }
        private bool Remove(InputDevice device)
        {
            Stack<InputDevice> stack = new Stack<InputDevice>();
            bool success = false;

            // Look through all devices
            int length = _devices.Count;
            for (int i = 0; i < length; i++)
            {
                // Get device
                InputDevice d = _devices.Pop();

                // If it is what you're looking for stop looking
                if (d == device)
                {
                    success = true;
                    break;
                }

                // Move device to other stack (Will be added to the main stack afterwards)
                stack.Push(d);
            }

            // Move them back to the main stack
            length = stack.Count;
            for (int i = 0; i < length; i++)
                stack.Push(stack.Pop());

            // Return whether or not it was found
            return success;
        }
        private void UpdateDevices()
        {
            //
            bool any = (_settings.FocusedInputOnly) ? _window.Focused : true;
            bool gamepad = (any)  ? ((_settings.FocusedGamePadInputOnly) ? _window.Focused : true) : false;
            bool keyboard = (any) ? ((_settings.FocusedKeyoardInputOnly) ? _window.Focused : true) : false;
            bool mouse = (any)    ? ((_settings.FocusedMouseInputOnly)   ? _window.Focused : true) : false;
            
            //
            _keyboard.Update(keyboard);
            _mouse.Update(mouse);

            //
            Stack<InputDevice> stack = new Stack<InputDevice>();

            // Look through all devices
            int length = _devices.Count;
            for (int i = 0; i < length; i++)
            {
                // Get device ref and move it to the other stack
                InputDevice d = _devices.Pop();
                stack.Push(d);

                // Update device
                switch (d.GetDeviceType())
                {
                    case InputDeviceType.GamePad: // GamePad
                        d.Update(gamepad);
                        break;
                    case InputDeviceType.Keyboard: // Keyboard
                        d.Update(keyboard);
                        break;
                    case InputDeviceType.Mouse: // Mouse
                        d.Update(mouse);
                        break;
                    case InputDeviceType.Other: // Other
                        d.Update(any);
                        break;
                }
            }

            // Move them back to the main stack
            length = stack.Count;
            for (int i = 0; i < length; i++)
                stack.Push(stack.Pop());
        }

        // BeginFrame
        internal void BeginFrame()
        {
            // Apply one-time-actions
            if (_beginFrame != null)
            {
                _beginFrame();
                _beginFrame = null;
            }

            //
            _keyboard.BeginFrame();
            _mouse.BeginFrame();


        }

        // Update
        internal void Update()
        {
            // Update devices
            UpdateDevices();
        }
    }
}
