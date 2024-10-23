using UnityEngine;

namespace Asteroids
{
    public class PlayerMovement : IMoveInputCollector
    {
        public void MovementInput(Vector2 direction)
        {
            Debug.Log($"Move input direction was {direction}");
        }
    }   
}
