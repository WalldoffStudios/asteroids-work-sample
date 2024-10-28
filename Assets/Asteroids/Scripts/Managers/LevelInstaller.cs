using System;
using Asteroids.Score;
using VContainer;
using VContainer.Unity;

namespace Asteroids.Scripts.Managers
{
    public class LevelInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            builder.Register<LevelGameStateNotifier>(Lifetime.Singleton)
                .As<ILevelStateListener>()
                .As<ILevelStateSubscription>()
                .As<IDisposable>();

            builder.Register<LevelManager>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<ILevelStateListener>())
                .As<IStartable>()
                .As<ITickable>()
                .As<IDisposable>();

            builder.Register<ScoreHandler>(Lifetime.Singleton)
                .As<IStartable>()
                .As<IUpdateScore>();
        }
    }
}