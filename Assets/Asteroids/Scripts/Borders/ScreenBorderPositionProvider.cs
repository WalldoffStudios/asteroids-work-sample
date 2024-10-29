using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Borders
{
    public class ScreenBorderPositionProvider : IGetScreenBorderPosition
    {
        private readonly IScreenBoundsProvider _screenBounds;

        public ScreenBorderPositionProvider(IScreenBoundsProvider screenBounds)
        {
            _screenBounds = screenBounds;
        }
        
        public (Vector2 position, BorderEdges edge) BorderPositionWithEdge()
        {
            BorderEdges edge = (BorderEdges)Random.Range(0, Enum.GetValues(typeof(BorderEdges)).Length);
            Vector2 position = edge switch
            {
                BorderEdges.Left => new Vector2(_screenBounds.Left, Random.Range(_screenBounds.Bottom, _screenBounds.Top)),
                BorderEdges.Right => new Vector2(_screenBounds.Right, Random.Range(_screenBounds.Bottom, _screenBounds.Top)),
                BorderEdges.Top => new Vector2(Random.Range(_screenBounds.Left, _screenBounds.Right), _screenBounds.Top),
                BorderEdges.Bottom => new Vector2(Random.Range(_screenBounds.Left, _screenBounds.Right), _screenBounds.Bottom),
                _ => throw new ArgumentOutOfRangeException(nameof(edge), edge, null)
            };

            return (position, edge);
        }
    }
}