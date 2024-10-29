using UnityEngine;

namespace Asteroids.Obstacles
{
    public interface IAsteroidFactory
    {
        void CreateAsteroid(int level, Vector2 position, Vector2 direction, float speed);
    }
}