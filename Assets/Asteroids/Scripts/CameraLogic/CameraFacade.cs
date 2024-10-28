using System;
using UnityEngine;

namespace Asteroids
{
    public interface ICameraBoundsProvider
    {
        float OrthographicSize { get; }
        float AspectRatio { get; }
    }

    public interface IScreenToWorldPoint
    {
        Vector2 ScreenToWorldPoint(Vector2 position);
    }
    
    public class CameraFacade : ICameraBoundsProvider, IScreenToWorldPoint
    {
        private readonly Camera _camera;

        public CameraFacade(Camera camera)
        {
            _camera = camera;
        }

        public float OrthographicSize => _camera.orthographicSize;
        public float AspectRatio => _camera.aspect;
        public Vector2 ScreenToWorldPoint(Vector2 position) => _camera.ScreenToWorldPoint(position);
    }   
}

