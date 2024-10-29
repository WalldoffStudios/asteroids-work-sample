using System;
using Asteroids.Score;
using Asteroids.Utilities;
using VContainer;
using VContainer.Unity;

namespace Asteroids.Managers
{
    public class LevelInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            builder.Register<LevelGameStateNotifier>(Lifetime.Singleton)
                .As<ILevelStateListener>()
                .As<ILevelStateSubscription>();
            
            builder.Register<LevelManager>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<ILevelStateListener>())
                .As<IStartable>()
                .As<ITickable>()
                .As<IScoreThresholdProvider>()
                .As<ILevelManagerNotifier>();
            
            //TODO: must register UI to keep track of score here
            
            builder.Register<ScoreHandler>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<ISaveManager>())
                .WithParameter(resolver => resolver.Resolve<IScoreThresholdProvider>())
                .WithParameter(resolver => resolver.Resolve<ILevelManagerNotifier>())
                .WithParameter(resolver => resolver.Resolve<ILevelStateSubscription>())
                .As<IAddToScore>()
                .As<IDisposable>();
        }
    }
}