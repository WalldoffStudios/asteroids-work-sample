using UnityEngine;

namespace Asteroids.Obstacles
{
    public interface IHandleAsteroidDestroyed
    {
        void AsteroidDestroyed(Vector2 position, int level);
    }
}