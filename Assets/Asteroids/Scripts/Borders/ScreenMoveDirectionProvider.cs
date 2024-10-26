using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Borders
{
    public interface IGetScreenMoveDirection
    {
        Vector2 MoveDirection(BorderEdges spawnedEdge);
    }
    public class ScreenMoveDirectionProvider : IGetScreenMoveDirection
    {
        private readonly IScreenBoundsProvider _screenBounds;

        public ScreenMoveDirectionProvider(IScreenBoundsProvider screenBounds)
        {
            _screenBounds = screenBounds;
        }
        
        public Vector2 MoveDirection(BorderEdges spawnedEdge)
        {
            return spawnedEdge switch
            {
                BorderEdges.Left => new Vector2(1f, Random.Range(-1f, 1f)).normalized,
                BorderEdges.Right => new Vector2(-1f, Random.Range(-1f, 1f)).normalized,
                BorderEdges.Top => new Vector2(Random.Range(-1f, 1f), -1f).normalized,
                BorderEdges.Bottom => new Vector2(Random.Range(-1f, 1f), 1f).normalized,
                _ => throw new ArgumentOutOfRangeException(nameof(spawnedEdge), spawnedEdge, null)
            };
        }
    }
}