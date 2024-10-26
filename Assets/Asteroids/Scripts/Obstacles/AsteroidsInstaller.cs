using Asteroids.Borders;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids.Obstacles
{
    public class AsteroidsInstaller : LifetimeScope
    {
        [SerializeField] private Asteroid asteroidPrefab = null;
        [SerializeField] private int initialPoolSize = 10;
        [SerializeField] private float delayBetweenSpawns = 1.0f;
        
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<IAsteroidPool>(resolver =>
            {
                return new AsteroidPool(
                    asteroidPrefab,
                    parentTransform: transform,
                    resolver,
                    initialPoolSize: initialPoolSize
                );
            }, Lifetime.Singleton);

            builder.Register<IAsteroidFactory, AsteroidFactory>(Lifetime.Singleton);

            builder.Register<ScreenBorderPositionProvider>(Lifetime.Singleton)
                .As<IGetScreenBorderPosition>();

            builder.Register<ScreenMoveDirectionProvider>(Lifetime.Singleton)
                .As<IGetScreenMoveDirection>();

            builder.Register<AsteroidSpawner>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<IGetScreenBorderPosition>())
                .WithParameter(resolver => resolver.Resolve<IGetScreenMoveDirection>())
                .WithParameter(resolver => resolver.Resolve<IAsteroidFactory>())
                .WithParameter(delayBetweenSpawns)
                .As<ITickable>();
        }
    }   
}
