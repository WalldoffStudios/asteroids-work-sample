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
            _currentScore += amount;
        }
    }   
}
