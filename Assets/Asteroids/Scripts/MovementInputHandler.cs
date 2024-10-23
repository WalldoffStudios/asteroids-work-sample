using UnityEngine;
using VContainer.Unity;

namespace Asteroids
{
    public interface IMoveInputCollector
    {
        void MovementInput(Vector2 direction);
    } 
    
    public class MovementInputHandler : ITickable
    {
        private readonly IMoveInputCollector _moveInputCollector;

        private Vector2 _movementInput;
        
        public MovementInputHandler(IMoveInputCollector moveInputCollector)
        {
            _moveInputCollector = moveInputCollector;
        }

        public void Tick()
        {
            _movementInput.x = Input.GetAxisRaw("Horizontal");
            _movementInput.y = Input.GetAxisRaw("Vertical");
            _moveInputCollector.MovementInput(_movementInput);
        }
    }   
}
