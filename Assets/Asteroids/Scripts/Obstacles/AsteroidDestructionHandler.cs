using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Obstacles
{
    public interface IHandleAsteroidDestroyed
    {
        void AsteroidDestroyed(Vector2 position, int level);
    }
    
    public interface IAsteroidDestructionListener
    {
        void OnAsteroidDestroyed(Vector2 position, int level);
    }
    
    public interface IAsteroidDestructionSubscription
    {
        void RegisterDestructionListener(IAsteroidDestructionListener destructionListener);
        void UnregisterDestructionListener(IAsteroidDestructionListener destructionListener);
    }
    
    public class AsteroidDestructionHandler : IHandleAsteroidDestroyed, IAsteroidDestructionSubscription
    {
        private readonly HashSet<IAsteroidDestructionListener> _destructionListeners =
            new HashSet<IAsteroidDestructionListener>();

        public void RegisterDestructionListener(IAsteroidDestructionListener destructionListener)
        {
            _destructionListeners.Add(destructionListener);
            Debug.LogWarning("Registered the first listener");
        }

        public void UnregisterDestructionListener(IAsteroidDestructionListener destructionListener)
        {
            _destructionListeners.Remove(destructionListener);
        }

        public void AsteroidDestroyed(Vector2 position, int level)
        {
            Debug.LogWarning("Asteroid was destroyed, this was called in the destruction handler");
            foreach (var listener in _destructionListeners)
            {
                listener.OnAsteroidDestroyed(position, level);
            }
        }
    }
}