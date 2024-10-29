namespace Asteroids
{
    public class GameManager
    {
        private readonly IGameStateListener _gameStateListener;
        private GameState _currentState;
        
        public GameManager(IGameStateListener gameStateListener)
        {
            _gameStateListener = gameStateListener;
        }
    }
}