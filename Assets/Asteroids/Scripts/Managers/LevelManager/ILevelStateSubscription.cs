namespace Asteroids.Managers
{
    public interface ILevelStateSubscription
    {
        void RegisterStateListener(ILevelStateListener stateListener);
        void UnregisterStateListener(ILevelStateListener stateListener);
    }
}