namespace Asteroids
{
    public interface IGameStateListener
    {
        void OnGameStateChanged(GameState newState);
    }
}
