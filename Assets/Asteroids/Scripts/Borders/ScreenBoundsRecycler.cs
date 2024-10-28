using System;
using System.Collections.Generic;
using Asteroids.Managers;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids.Borders
{
    public interface IWrapRecycler
    {
        Transform RecycleTransform();
        void ReturnTransformToPool();
    }

    public interface IScreenBoundsRecycler
    {
        void RegisterWrapRecycler(IWrapRecycler wrapRecycler);
        void UnregisterWrapRecycler(IWrapRecycler wrapRecycler);
    }
    
    public class ScreenBoundsRecycler : IScreenBoundsRecycler, IFixedTickable, ILevelStateListener, IDisposable
    {
        private readonly ScreenBoundsHandler _boundsHandler;
        private readonly HashSet<IWrapRecycler> _recyclableTransforms;
        private readonly HashSet<IWrapRecycler> _recyclablesToAdd;
        private readonly HashSet<IWrapRecycler> _recyclableToRemove;
        private readonly ILevelStateSubscription _stateSubscription;
        private LevelGameState _currentState;

        public ScreenBoundsRecycler(ScreenBoundsHandler boundsHandler, ILevelStateSubscription stateSubscription)
        {
            _boundsHandler = boundsHandler;
            _recyclableTransforms = new HashSet<IWrapRecycler>();
            _recyclablesToAdd = new HashSet<IWrapRecycler>();
            _recyclableToRemove = new HashSet<IWrapRecycler>();
            _stateSubscription = stateSubscription;
            _stateSubscription.RegisterStateListener(this);
        }
        
        public void OnLevelStateChanged(LevelGameState newState)
        {
            _currentState = newState;
            if (_currentState != LevelGameState.Playing && _currentState != LevelGameState.Initializing)
            {
                foreach (IWrapRecycler wrapRecycler in _recyclableTransforms)
                {
                    Transform transform = wrapRecycler.RecycleTransform();
                    if (transform == null || !transform.gameObject.activeInHierarchy)
                    {
                        continue;
                    }
                    wrapRecycler.ReturnTransformToPool();
                }
                _recyclableTransforms.Clear();
            }
        }
        
        public void RegisterWrapRecycler(IWrapRecycler wrapRecycler)
        {
            _recyclablesToAdd.Add(wrapRecycler);
        }

        public void UnregisterWrapRecycler(IWrapRecycler wrapRecycler)
        {
            _recyclableToRemove.Add(wrapRecycler);    
        }
        
        public void FixedTick()
        {
            if(_currentState != LevelGameState.Playing) return;
            foreach (IWrapRecycler wrapRecycler in _recyclableTransforms)
            {
                Transform transform = wrapRecycler.RecycleTransform();
                if (transform == null || !transform.gameObject.activeInHierarchy)
                {
                    continue;
                }

                if (_boundsHandler.IsOutsideScreen(transform.position))
                {
                    wrapRecycler.ReturnTransformToPool();
                }
            }
            
            _recyclableTransforms.UnionWith(_recyclablesToAdd);
            _recyclablesToAdd.Clear();
            
            _recyclableTransforms.ExceptWith(_recyclableToRemove);
            _recyclableToRemove.Clear();
        }
        
        public void Dispose()
        {
            _recyclableTransforms.Clear();
            _recyclablesToAdd.Clear();
            _recyclableToRemove.Clear();
            _stateSubscription.UnregisterStateListener(this);
        }
    }
}