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
                .As<ITickable>();

            builder.Register<ScoreHandler>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<ISaveManager>())
                .As<IStartable>()
                .As<IUpdateScore>();
        }
    }
}