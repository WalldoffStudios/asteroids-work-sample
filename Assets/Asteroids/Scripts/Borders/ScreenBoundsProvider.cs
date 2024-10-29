namespace Asteroids.Borders
{
    public class ScreenBoundsProvider : IScreenBoundsProvider
    {
        private readonly ICameraBoundsProvider _cameraBounds;

        public ScreenBoundsProvider(ICameraBoundsProvider cameraBounds)
        {
            _cameraBounds = cameraBounds;
            UpdateBounds();
        }

        public float Left { get; private set; }
        public float Right { get; private set; }
        public float Top { get; private set; }
        public float Bottom { get; private set; }

        public void UpdateBounds()
        {
            float orthographicSize = _cameraBounds.OrthographicSize;
            float aspect = _cameraBounds.AspectRatio;

            Top = orthographicSize;
            Bottom = -orthographicSize;
            Right = aspect * orthographicSize;
            Left = -Right;
        }
    }
}

