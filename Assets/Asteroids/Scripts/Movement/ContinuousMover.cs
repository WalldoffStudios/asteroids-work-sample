using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids.Movement
{
    public class ContinuousMover : ISetMovementDirection, IFixedTickable
    {
        private readonly Rigidbody2D _rigidbody2D;
        private Vector2 _moveDirection;
        public ContinuousMover(Rigidbody2D rigidbody2D)
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
