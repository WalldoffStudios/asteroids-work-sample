using UnityEngine;

namespace Asteroids.Borders
{
    public interface IGetScreenBorderPosition
    {
        (Vector2 position, BorderEdges edge) BorderPositionWithEdge();
    }
}
