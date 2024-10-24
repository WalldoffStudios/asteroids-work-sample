using UnityEngine;
using VContainer.Unity;

namespace Asteroids.Player.InputHandling
{
    public class MovementInputHandler : ITickable
    {
        private readonly IAddMovementCalculation _movementCalculation;
        private readonly float _movementSpeed;

        private Vector2 _movementInput;
        
        public MovementInputHandler(IAddMovementCalculation movementCalculation, float movementSpeed)
        {
            _movementCalculation = movementCalculation;
            _movementSpeed = movementSpeed;
        }

        public void Tick()
        {
            _movementInput.x = Input.GetAxisRaw("Horizontal");
            _movementInput.y = Input.GetAxisRaw("Vertical");
            _movementCalculation.AddCalculation(_movementInput.normalized * _movementSpeed);
        }
    }   
}
