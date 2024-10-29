using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Managers
{
    public interface IGameStateSubscription
    {
        void RegisterStateListener(IGameStateListener stateListener);
        void UnregisterStateListener(IGameStateListener stateListener);
    }
    
    public class GameStateChangeNotifier : IGameStateListener, IGameStateSubscription
    {
        private readonly HashSet<IGameStateListener> _stateListeners = new HashSet<IGameStateListener>();
        
        public void OnGameStateChanged(GameState newState)
        {
            Debug.Log($"Game state handler reacted to state change");
            foreach (IGameStateListener listener in _stateListeners)
            {
                if(listener != null) listener.OnGameStateChanged(newState);
            }
        }

        public void RegisterStateListener(IGameStateListener stateListener)
        {
            _stateListeners.Add(stateListener);
        }

        public void UnregisterStateListener(IGameStateListener stateListener)
        {
            _stateListeners.Remove(stateListener);
        }
    }
}