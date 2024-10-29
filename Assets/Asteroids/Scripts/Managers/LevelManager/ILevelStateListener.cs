namespace Asteroids.Managers
{
    public interface ILevelStateListener
    {
        void OnLevelStateChanged(LevelGameState newState);
    }
}