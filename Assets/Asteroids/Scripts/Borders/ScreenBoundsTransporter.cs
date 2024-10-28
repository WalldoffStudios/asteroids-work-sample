using System;
using System.Collections.Generic;
using Asteroids.Managers;
using VContainer.Unity;
using UnityEngine;

namespace Asteroids.Borders
{
    public interface IScreenBoundsTransporter
    {
        void RegisterTransform(Transform wrapperTransform);
        void UnregisterTransform(Transform wrapperTransform);
    }
    
    public class ScreenBoundsTransporter : IScreenBoundsTransporter, IFixedTickable, ILevelStateListener, IDisposable
    {
        private readonly ScreenBoundsHandler _boundsHandler;
        private readonly HashSet<Transform> _transforms;
        private readonly HashSet<Transform> _transformsToAdd;
        private readonly HashSet<Transform> _transformsToRemove;
        private readonly ILevelStateSubscription _stateSubscription;
        private LevelGameState _currentState;

        public ScreenBoundsTransporter(ScreenBoundsHandler boundsHandler, ILevelStateSubscription stateSubscription)
        {
            Debug.Log("Setup screen bounds transporter");
            _boundsHandler = boundsHandler;
            _transforms = new HashSet<Transform>();
            _transformsToAdd = new HashSet<Transform>();
            _transformsToRemove = new HashSet<Transform>();
            _stateSubscription = stateSubscription;
            _stateSubscription.RegisterStateListener(this);
        }
        
        public void OnLevelStateChanged(LevelGameState newState)
        {
            _currentState = newState;
        }

        public void RegisterTransform(Transform wrapperTransform)
        {
            _transformsToAdd.Add(wrapperTransform);
        }
        
        public void UnregisterTransform(Transform wrapperTransform)
        {
            _transformsToRemove.Add(wrapperTransform);
        }

        public void FixedTick()
        {
            if(_currentState != LevelGameState.Playing) return;
            foreach (var transform in _transforms)
            {
                if (transform == null || !transform.gameObject.activeInHierarchy)
                {
                    continue;
                }

                Vector2 newPosition = _boundsHandler.WrapPosition(transform.position);
                transform.position = newPosition;
            }

            _transforms.UnionWith(_transformsToAdd);
            _transformsToAdd.Clear();
            
            _transforms.ExceptWith(_transformsToRemove);
            _transformsToRemove.Clear();
        }

        public void Dispose()
        {
            _transforms.Clear();
            _transformsToAdd.Clear();
            _transformsToRemove.Clear();
            _stateSubscription.UnregisterStateListener(this);
        }
    }
}
