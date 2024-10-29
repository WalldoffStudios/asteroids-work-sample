using UnityEngine;

namespace Asteroids.Borders
{
    public interface IGetScreenMoveDirection
    {
        Vector2 MoveDirection(BorderEdges spawnedEdge);
    }
}