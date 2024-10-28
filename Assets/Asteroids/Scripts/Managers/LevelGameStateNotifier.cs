using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Scripts.Managers
{
    public interface ILevelStateSubscription
    {
        void RegisterStateListener(ILevelStateListener stateListener);
        void UnregisterStateListener(ILevelStateListener stateListener);
    }
    
    public class LevelGameStateNotifier : ILevelStateListener, ILevelStateSubscription, IDisposable
    {
        private readonly HashSet<ILevelStateListener> _stateListeners = new HashSet<ILevelStateListener>();
        
        public LevelGameStateNotifier()
        {
            Debug.Log("LevelGameStateNotifier instance created");
        }

        public void OnLevelStateChanged(LevelGameState newState)
        {
            foreach (ILevelStateListener listener in _stateListeners)
            {
                if(listener != null) listener.OnLevelStateChanged(newState);
            }
        }

        public void RegisterStateListener(ILevelStateListener stateListener)
        {
            _stateListeners.Add(stateListener);
        }

        public void UnregisterStateListener(ILevelStateListener stateListener)
        {
            _stateListeners.Remove(stateListener);
        }

        public void Dispose()
        {
            Debug.Log("Level game state notifier was disposed of");
        }
    }
}