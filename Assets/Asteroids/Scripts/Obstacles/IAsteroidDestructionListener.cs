using UnityEngine;

namespace Asteroids.Obstacles
{
    public interface IAsteroidDestructionListener
    {
        void OnAsteroidDestroyed(Vector2 position, int level);
    }
}