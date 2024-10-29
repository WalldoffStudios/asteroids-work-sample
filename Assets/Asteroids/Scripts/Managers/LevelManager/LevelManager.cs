using System;
using Asteroids.Utilities;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids.Managers
{
    public class LevelManager : IStartable, ITickable, IScoreThresholdProvider, ILevelManagerNotifier
    {
        private const string LevelKey = "LevelKey";

        private readonly ISaveManager _saveManager;
        private readonly ILevelStateListener _stateListener;
        private LevelGameState _currentState;
        private int _currentLevel;
        private int _scoreToReach;

        public LevelManager(ILevelStateListener stateListener, ISaveManager saveManager)
        {
            _stateListener = stateListener;
            _saveManager = saveManager;
        }

        public void Start()
        {
            _saveManager.SetData(LevelKey, 1);
            TransitionToState(LevelGameState.Initializing);
            BuildLevel();
            TransitionToState(LevelGameState.Playing);
        }

        public int GetScoreToReach() => _scoreToReach;

        public void OnScoreReached()
        {
            Debug.Log($"Reached score of {_scoreToReach}, transitioning to completed state");
            TransitionToState(LevelGameState.LevelComplete);
            _saveManager.SetData(LevelKey, _currentLevel + 1);
            _saveManager.Save(); //Save instantly since this is crucial data
            TransitionToState(LevelGameState.Initializing);
            BuildLevel();
            TransitionToState(LevelGameState.Playing);
        }

        public void ResetLevel()
        {
            TransitionToState(LevelGameState.Initializing);
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
            _currentLevel = _saveManager.GetData(LevelKey, 1);
            _scoreToReach = _currentLevel * 10;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                int totalStates = Enum.GetValues(typeof(LevelGameState)).Length;
                int nextState = ((int)_currentState + 1) % totalStates;
                TransitionToState((LevelGameState)nextState);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetLevel();
            }
        }
    }
}