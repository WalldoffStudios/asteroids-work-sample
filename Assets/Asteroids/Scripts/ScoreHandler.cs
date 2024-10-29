using System;
using Asteroids.Managers;
using Asteroids.Utilities;

namespace Asteroids.Score
{
    public interface IAddToScore
    {
        void AddToScore(int amount);
    }
    
    public class ScoreHandler : IAddToScore, ILevelStateListener, IDisposable
    {
        private readonly ILevelManagerNotifier _levelManagerNotifier;
        private readonly IScoreThresholdProvider _scoreThresholdProvider;
        private readonly ILevelStateSubscription _stateSubscription;

        private LevelGameState _currentState;
        private int _currentScore;

        public ScoreHandler(
            IScoreThresholdProvider scoreThresholdProvider, 
            ILevelManagerNotifier levelManagerNotifier, 
            ILevelStateSubscription stateSubscription)
        {
            _scoreThresholdProvider = scoreThresholdProvider;
            _levelManagerNotifier = levelManagerNotifier;
            _stateSubscription = stateSubscription;
            _stateSubscription.RegisterStateListener(this);
        }
        
        public void OnLevelStateChanged(LevelGameState newState)
        {
            _currentState = newState;
            if (_currentState == LevelGameState.Initializing)
            {
                _currentScore = 0;
            }

            if (_currentState == LevelGameState.LevelComplete)
            {
                //TODO: save current wave or high score here
            }
        }
        
        public void AddToScore(int amount)
        { 
            _currentScore += amount;
            if (_currentScore >= _scoreThresholdProvider.GetScoreToReach())
            {
                _levelManagerNotifier.OnScoreReached();
            }
        }

        public void Dispose()
        {
            _stateSubscription.UnregisterStateListener(this);
        }
    }   
}
