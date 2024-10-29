namespace Asteroids.Obstacles
{
    public interface IAsteroidPool
    {
        Asteroid GetAsteroid();
        void ReleaseAsteroid(Asteroid asteroid);
        void ClearPool();
    }
}