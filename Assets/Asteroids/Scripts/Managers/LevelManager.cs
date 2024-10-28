using System;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids.Managers
{
    public enum LevelGameState
    {
        Initializing = 0,
        Playing = 1,
        Paused = 2,
        GameOver = 3,
        LevelComplete = 4
    }
    public interface ILevelStateListener
    {
        void OnLevelStateChanged(LevelGameState newState);
    }
    
    public class LevelManager : IStartable, ITickable
    {
        private readonly ILevelStateListener _stateListener;
        private LevelGameState _currentState;
        
        public LevelManager(ILevelStateListener stateListener)
        {
            _stateListener = stateListener;
        }
        
        public void Start()
        {
            TransitionToState(LevelGameState.Initializing);
            BuildLevel();
            TransitionToState(LevelGameState.Playing);
        }

        public void ResetLevel()
        {
            TransitionToState(LevelGameState.Initializing);
            CleanupLevel();
            BuildLevel();
            TransitionToState(LevelGameState.Playing);
        }

        private void TransitionToState(LevelGameState newState)
        {
            _currentState = newState;
            Debug.Log($"Level Manager transitioning to state: {_currentState}");
            _stateListener.OnLevelStateChanged(_currentState);
        }

        private void BuildLevel()
        {
            // Implement level-building logic here
            Debug.Log("Building level...");
            // Example: Spawn player, asteroids, UI setup, etc.
        }

        private void CleanupLevel()
        {
            // Implement cleanup logic here
            Debug.Log("Cleaning up level...");
            // Example: Return all objects to pools, reset UI, reset score, etc.
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                int totalStates = Enum.GetValues(typeof(LevelGameState)).Length;
                int nextState = ((int)_currentState + 1) % totalStates;
                TransitionToState((LevelGameState)nextState);
            }
        }
    }
}