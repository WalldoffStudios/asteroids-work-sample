using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Managers
{
    public class LevelGameStateNotifier : ILevelStateListener, ILevelStateSubscription
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
    }
}