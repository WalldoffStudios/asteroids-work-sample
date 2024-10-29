namespace Asteroids.Obstacles
{
    public interface IAsteroidDestructionSubscription
    {
        void RegisterDestructionListener(IAsteroidDestructionListener destructionListener);
        void UnregisterDestructionListener(IAsteroidDestructionListener destructionListener);
    }
}