using UnityEngine;

namespace Asteroids
{
    public interface ICameraBoundsProvider
    {
        float OrthographicSize { get; }
        float AspectRatio { get; }
    }
    
    public class CameraFacade : ICameraBoundsProvider
    {
        private readonly Camera _camera;

        public CameraFacade(Camera camera)
        {
            _camera = camera;
        }

        public float OrthographicSize => _camera.orthographicSize;
        public float AspectRatio => _camera.aspect;
    }   
}

