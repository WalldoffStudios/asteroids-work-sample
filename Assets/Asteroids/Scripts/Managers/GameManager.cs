namespace Asteroids
{
    public enum GameState
    {
        Boot = 0,
        MainMenu = 1,
        Game = 2
    }

    public interface IGameStateListener
    {
        void OnGameStateChanged(GameState newState);
    }
    
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