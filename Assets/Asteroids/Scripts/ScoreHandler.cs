using Asteroids.Managers;
using Asteroids.Utilities;
using UnityEngine;
using VContainer.Unity;

namespace Asteroids.Score
{
    public interface IUpdateScore
    {
        void UpdateScore(int amount);
    }
    
    public class ScoreHandler : IStartable, IUpdateScore
    {
        private const string ScoreKey = "PlayerScore";
        private const string HealthKey = "PlayerHealth";
        private readonly ISaveManager _saveManager;
        private int _currentScore;

        public ScoreHandler(ISaveManager saveManager)
        {
            _saveManager = saveManager;
        }
        
        public void Start()
        {
            _currentScore = _saveManager.GetData(ScoreKey, 0);
            Debug.LogWarning($"Loaded player score with a value of {_currentScore}");
        }
        
        public void UpdateScore(int amount)
        { 
            _currentScore += amount;
            _saveManager.SetData(ScoreKey, _currentScore);
        }
    }   
}
