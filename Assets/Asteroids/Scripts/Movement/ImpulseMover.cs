using UnityEngine;

namespace Asteroids.Movement
{
    public class ImpulseMover : ISetMovementDirection
    {
        private Rigidbody2D _rigidbody2D;
        public ImpulseMover(Rigidbody2D rigidbody2D)
        {
            _rigidbody2D = rigidbody2D;
        }
        public void SetDirection(Vector2 direction)
        {
            _rigidbody2D.AddForce(direction, ForceMode2D.Impulse);
        }
    }   
}
