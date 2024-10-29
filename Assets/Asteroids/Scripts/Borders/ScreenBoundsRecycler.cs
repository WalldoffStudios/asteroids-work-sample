using System;
using System.Collections.Generic;
using Asteroids.Managers;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids.Borders
{
    public class ScreenBoundsRecycler : IScreenBoundsRecycler, ITickable, ILevelStateListener, IDisposable
    {
        private readonly ScreenBoundsHandler _boundsHandler;
        private readonly HashSet<IWrapRecycler> _recyclableTransforms;
        private readonly HashSet<IWrapRecycler> _recyclablesToAdd;
        private readonly HashSet<IWrapRecycler> _recyclablesToRemove;
        private readonly ILevelStateSubscription _stateSubscription;
        private LevelGameState _currentState;

        public ScreenBoundsRecycler(ScreenBoundsHandler boundsHandler, ILevelStateSubscription stateSubscription)
        {
            _boundsHandler = boundsHandler;
            _recyclableTransforms = new HashSet<IWrapRecycler>();
            _recyclablesToAdd = new HashSet<IWrapRecycler>();
            _recyclablesToRemove = new HashSet<IWrapRecycler>();
            _stateSubscription = stateSubscription;
            _stateSubscription.RegisterStateListener(this);
        }
        
        public void OnLevelStateChanged(LevelGameState newState)
        {
            _currentState = newState;
        }
        
        public void RegisterWrapRecycler(IWrapRecycler wrapRecycler)
        {
            _recyclablesToAdd.Add(wrapRecycler);
        }

        public void UnregisterWrapRecycler(IWrapRecycler wrapRecycler)
        {
            if(_currentState == LevelGameState.Playing) _recyclablesToRemove.Add(wrapRecycler);    
        }
        
        public void Tick()
        {
            if(_currentState != LevelGameState.Playing) return;
            
            foreach (IWrapRecycler wrapRecycler in _recyclableTransforms)
            {
                Transform transform = wrapRecycler.RecycleTransform();
                if (transform == null  || !transform.gameObject.activeSelf)
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
            
            _recyclableTransforms.ExceptWith(_recyclablesToRemove);
            _recyclablesToRemove.Clear();
        }
        
        public void Dispose()
        {
            _recyclableTransforms.Clear();
            _recyclablesToAdd.Clear();
            _recyclablesToRemove.Clear();
            _stateSubscription.UnregisterStateListener(this);
        }
    }
}