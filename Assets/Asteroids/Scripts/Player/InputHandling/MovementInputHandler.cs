using System;
using Asteroids.Managers;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids.Player.InputHandling
{
    public class MovementInputHandler : ITickable, ILevelStateListener, IDisposable
    {
        private readonly IAddMovementCalculation _movementCalculation;
        private readonly ILevelStateSubscription _stateSubscription;
        private readonly float _movementSpeed;

        private Vector2 _movementInput;
        private LevelGameState _currentState;
        
        public MovementInputHandler(IAddMovementCalculation movementCalculation, ILevelStateSubscription stateSubscription, float movementSpeed)
        {
            _movementCalculation = movementCalculation;
            _stateSubscription = stateSubscription;
            _movementSpeed = movementSpeed;
            _stateSubscription.RegisterStateListener(this);
        }

        public void Tick()
        {
            if(_currentState != LevelGameState.Playing) return;
            _movementInput.x = Input.GetAxisRaw("Horizontal");
            _movementInput.y = Input.GetAxisRaw("Vertical");
            _movementCalculation.AddCalculation(_movementInput.normalized * _movementSpeed);
        }

        public void OnLevelStateChanged(LevelGameState newState)
        {
            _currentState = newState;
        }

        public void Dispose()
        {
            _stateSubscription.UnregisterStateListener(this);
        }
    }   
}
