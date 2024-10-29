using UnityEngine;
using VContainer.Unity;

namespace Asteroids.Movement
{
    public class ContinuousMover : ISetMovement, IFixedTickable
    {
        private readonly Rigidbody2D _rigidbody2D;
        private Vector2 _movement;
        public ContinuousMover(Rigidbody2D rigidbody2D)
        {
            _rigidbody2D = rigidbody2D;
        }
        
        public void SetMovement(Vector2 movement)
        {
            _movement = movement;
        }

        public void FixedTick()
        {
            _rigidbody2D.AddForce(_movement, ForceMode2D.Force);
        }
    }   
}
