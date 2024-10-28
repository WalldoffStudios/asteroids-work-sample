using System;
using Asteroids.Borders;
using Asteroids.Managers;
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

            builder.Register<AsteroidDestructionHandler>(Lifetime.Singleton)
                .As<IHandleAsteroidDestroyed>()
                .As<IAsteroidDestructionSubscription>();

            builder.Register<IAsteroidPool>(resolver =>
            {
                return new AsteroidPool(
                    asteroidPrefab,
                    transform,
                    resolver,
                    initialPoolSize
                );
            }, Lifetime.Singleton);

            builder.Register<IAsteroidFactory, AsteroidFactory>(Lifetime.Singleton);

            builder.Register<ScreenBorderPositionProvider>(Lifetime.Singleton)
                .As<IGetScreenBorderPosition>();

            builder.Register<ScreenMoveDirectionProvider>(Lifetime.Singleton)
                .As<IGetScreenMoveDirection>();

            builder.Register<AsteroidSpawner>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<IAsteroidFactory>());

            builder.Register<IntervalAsteroidSpawner>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<AsteroidSpawner>())
                .WithParameter(resolver => resolver.Resolve<IGetScreenBorderPosition>())
                .WithParameter(resolver => resolver.Resolve<IGetScreenMoveDirection>())
                .WithParameter(delayBetweenSpawns)
                .WithParameter(resolver => resolver.Resolve<ILevelStateSubscription>())
                .As<ITickable>()
                .As<ILevelStateListener>()
                .As<IDisposable>();

            builder.Register<AsteroidSplitter>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<AsteroidSpawner>())
                .WithParameter(resolver => resolver.Resolve<IAsteroidDestructionSubscription>())
                .WithParameter(resolver => resolver.Resolve<ILevelStateSubscription>())
                .As<IStartable>()
                .As<IAsteroidDestructionListener>()
                .As<ILevelStateListener>()
                .As<IDisposable>();
        }
    }   
}
