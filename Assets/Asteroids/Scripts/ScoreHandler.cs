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
        private int _currentScore;
        
        public void Start()
        {
            _currentScore = 0;
        }
        
        public void UpdateScore(int amount)
        { 
            Debug.Log($"Added score, previous score was: {_currentScore}, new score is: {_currentScore + amount}");
            _currentScore += amount;
        }
    }   
}
