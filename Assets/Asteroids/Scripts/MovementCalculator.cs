using UnityEngine;
using VContainer.Unity;

//Todo: This class will get all forces (player input, asteroid impact collision forces) and calculate the total amount of force to apply
namespace Asteroids
{
    public interface IAddMovementCalculation
    {
        void AddCalculation(Vector2 movement);
    }
    public class MovementCalculator : IAddMovementCalculation, ITickable
    {
        private readonly IApplyMovement _applyMovement;
        private Vector2 _totalMovement;

        public MovementCalculator(IApplyMovement applyMovement)
        {
            _applyMovement = applyMovement;
        }
        
        public void AddCalculation(Vector2 movement)
        {
            _totalMovement += movement;
        }

        public void Tick()
        {
            _applyMovement.ApplyMovement(_totalMovement);
            _totalMovement = Vector2.zero;
        }
    }   
}
