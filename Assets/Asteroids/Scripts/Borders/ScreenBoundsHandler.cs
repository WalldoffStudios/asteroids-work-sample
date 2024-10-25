using UnityEngine;

namespace Asteroids.Borders
{
    public class ScreenBoundsHandler
    {
        private readonly IScreenBoundsProvider _boundsProvider;

        public ScreenBoundsHandler(IScreenBoundsProvider boundsProvider)
        {
            _boundsProvider = boundsProvider;
        }

        public Vector2 WrapPosition(Vector2 position)
        {
            float x = position.x;
            float y = position.y;
            float left = _boundsProvider.Left;
            float right = _boundsProvider.Right;
            float top = _boundsProvider.Top;
            float bottom = _boundsProvider.Bottom;

            if (x > right)
                x = left;
            else if (x < left)
                x = right;

            if (y > top)
                y = bottom;
            else if (y < bottom)
                y = top;

            return new Vector2(x, y);
        }

        public bool IsOutsideScreen(Vector2 position)
        {
            float x = position.x;
            float y = position.y;
            float left = _boundsProvider.Left;
            float right = _boundsProvider.Right;
            float top = _boundsProvider.Top;
            float bottom = _boundsProvider.Bottom;

            return x < left || x > right || y < bottom || y > top;
        }
    }
}