using UnityEngine;
using VContainer.Unity;

namespace Asteroids
{
    public class ObjectRotator : ITickable
    {
        private readonly Rigidbody2D _rigidbody2D;
        public ObjectRotator(Rigidbody2D rigidbody2D)
        {
            _rigidbody2D = rigidbody2D;
        }

        public void Tick()
        {
            Vector2 velocity = _rigidbody2D.velocity;
            if(velocity.magnitude < 1.0f) return;
            Vector2 normalizedVelocity = velocity.normalized;
            float angle = Mathf.Atan2(normalizedVelocity.y, normalizedVelocity.x) * Mathf.Rad2Deg - 90.0f;
            _rigidbody2D.rotation = angle;
        }
    }
}