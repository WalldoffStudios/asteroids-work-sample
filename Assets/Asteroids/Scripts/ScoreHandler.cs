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
        private const string ScoreKey = "PlayerScore";
        private const string HealthKey = "PlayerHealth";
        
        private readonly ISaveManager _saveManager;
        private readonly ILevelManagerNotifier _levelManagerNotifier;
        private readonly IScoreThresholdProvider _scoreThresholdProvider;
        private readonly ILevelStateSubscription _stateSubscription;

        private LevelGameState _currentState;
        private int _currentScore;

        public ScoreHandler(
            ISaveManager saveManager, 
            IScoreThresholdProvider scoreThresholdProvider, 
            ILevelManagerNotifier levelManagerNotifier, 
            ILevelStateSubscription stateSubscription)
        {
            _saveManager = saveManager;
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

            if (_currentState == LevelGameState.GameOver)
            {
                //TODO: save current wave or high score here
            }
        }
        
        public void AddToScore(int amount)
        { 
            _currentScore += amount;
            _saveManager.SetData(ScoreKey, _currentScore);
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
