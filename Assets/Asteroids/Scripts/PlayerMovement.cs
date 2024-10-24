using UnityEngine;
using VContainer.Unity;

namespace Asteroids
{
    public interface IApplyMovement
    {
        void ApplyMovement(Vector2 movement);
    }
    public class PlayerMovement : IApplyMovement, IFixedTickable
    {
        private readonly Rigidbody2D _rigidbody2D;
        private Vector2 _moveDirection;
        public PlayerMovement(Rigidbody2D rigidbody2D)
        {
            _rigidbody2D = rigidbody2D;
        }
        
        public void ApplyMovement(Vector2 movement)
        {
            _moveDirection = movement;
        }

        public void FixedTick()
        {
            _rigidbody2D.AddForce(_moveDirection, ForceMode2D.Force);   
        }
    }   
}
