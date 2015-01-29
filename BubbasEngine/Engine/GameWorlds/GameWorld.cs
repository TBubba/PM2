using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BubbasEngine.Engine.Physics.Dynamics;
using BubbasEngine.Engine.GameWorlds.GameInterfaces;
using System.Collections.ObjectModel;

namespace BubbasEngine.Engine.GameWorlds
{
    public class GameWorld
    {
        // Private
        private EntityContainer _entities;

        private Action _objectBeginFrame;
        private Action _objectStep;
        private Action<float> _objectAnimate;

        private Action _beginFrame;

        private PhysicsWorld _physicsWorld;
        private float _stepTime;

        // Public
        public EntityContainer Entities
        { get { return _entities; } }
        public PhysicsWorld PhysicsWorld
        { get { return _physicsWorld; } }

        public float StepTime
        { get { return _stepTime; } set { _stepTime = value; } }

        // Constructor(s)
        public GameWorld(float stepTime)
        {
            // Create containe
            _entities = new EntityContainer(this);
            
            //
            _physicsWorld = new PhysicsWorld(new Physics.Common.Vector2());
            _stepTime = stepTime;
        }

        // Game Loop
        public void BeginFrame()
        {
            //
            if (_beginFrame != null)
            {
                _beginFrame();
                _beginFrame = null;
            }

            // Call BeginFrame
            CallBeginFrame();
        }
        public void Step()
        {
            // Call Step
            CallStep();

            // Physics step
            _physicsWorld.Step(_stepTime);
        }
        public void Animate(float delta)
        {
            // Call Animate
            CallAnimate(delta);
        }

        // OnEntity...
        internal void OnEntityAdded(GameObject entity)
        {
            // Add reference to this world to entity
            entity.AddToWorld(this);

            // Add entity calls
            AddEntityCalls(entity);
        }
        internal void OnEntityRemoved(GameObject entity)
        {
            // Remove reference to this world from entity
            entity.RemoveFromWorld();

            // Remove entity calls
            RemoveEntityCalls(entity);
        }

        // Handle Entities
        private void AddEntityCalls(GameObject entity)
        {
            //
            if (entity is IGameBeginFrame) // BeginFrame
                _objectBeginFrame += ((IGameBeginFrame)entity).BeginFrame;

            if (entity is IGameCreated) // Created
                _beginFrame += ((IGameCreated)entity).Created;
            if (entity is IGamePhysics) // GetBody (Physics)
                _beginFrame += delegate { ((IGamePhysics)entity).AddBody(_physicsWorld); };
            if (entity is IGameStep) // Step
                _objectStep += ((IGameStep)entity).Step;
            if (entity is IGameAnimate) // Animate
                _objectAnimate += ((IGameAnimate)entity).Animate;
        }
        private void RemoveEntityCalls(GameObject entity)
        {
            // Call "Removed" when approperiate (if IGameRemoved)
            if (entity is IGameRemoved) // Removed
                _beginFrame += ((IGameRemoved)entity).Removed;

            if (entity is IGameBeginFrame) // BeginFrame
                _objectBeginFrame -= ((IGameBeginFrame)entity).BeginFrame;
            if (entity is IGameStep) // Step
                _objectStep -= ((IGameStep)entity).Step;
            if (entity is IGameAnimate) // Animate
                _objectAnimate -= ((IGameAnimate)entity).Animate;
        }

        // Entity Calls
        private void CallBeginFrame()
        {
            if (_objectBeginFrame != null)
                _objectBeginFrame();
        }
        private void CallStep()
        {
            if (_objectStep != null)
                _objectStep();
        }
        private void CallAnimate(float delta)
        {
            if (_objectAnimate != null)
                _objectAnimate(delta);
        }
    }
}
