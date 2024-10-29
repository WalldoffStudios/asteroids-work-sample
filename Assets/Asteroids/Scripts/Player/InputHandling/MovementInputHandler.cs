using System;
using Asteroids.Managers;
using Asteroids.Movement;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids.Player.InputHandling
{
    public class MovementInputHandler : ITickable, ILevelStateListener, IDisposable
    {
        private readonly ISetMovement _setMovement;
        private readonly ILevelStateSubscription _stateSubscription;
        private readonly float _movementSpeed;

        private Vector2 _movementInput;
        private LevelGameState _currentState;
        
        public MovementInputHandler(ISetMovement setMovement, ILevelStateSubscription stateSubscription, float movementSpeed)
        {
            _setMovement = setMovement;
            _stateSubscription = stateSubscription;
            _movementSpeed = movementSpeed;
            _stateSubscription.RegisterStateListener(this);
        }

        public void Tick()
        {
            if(_currentState != LevelGameState.Playing) return;
            _movementInput.x = Input.GetAxisRaw("Horizontal");
            _movementInput.y = Input.GetAxisRaw("Vertical");
            _setMovement.SetMovement(_movementInput.normalized * _movementSpeed);
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
