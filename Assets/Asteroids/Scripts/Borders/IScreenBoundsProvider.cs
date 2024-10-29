namespace Asteroids.Borders
{
    public interface IScreenBoundsProvider
    {
        float Left { get; }
        float Right { get; }
        float Top { get; }
        float Bottom { get; }
    }
}