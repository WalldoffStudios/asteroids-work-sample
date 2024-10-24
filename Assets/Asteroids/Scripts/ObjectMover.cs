using UnityEngine;
using VContainer.Unity;

namespace Asteroids
{
    public interface ISetMovementDirection
    {
        void SetDirection(Vector2 direction);
    }
    public class ObjectMover : ISetMovementDirection, IFixedTickable
    {
        private readonly Rigidbody2D _rigidbody2D;
        private Vector2 _moveDirection;
        public ObjectMover(Rigidbody2D rigidbody2D)
        {
            _rigidbody2D = rigidbody2D;
        }
        
        public void SetDirection(Vector2 direction)
        {
            _moveDirection = direction;
        }

        public void FixedTick()
        {
            _rigidbody2D.AddForce(_moveDirection, ForceMode2D.Force);
        }
    }   
}
