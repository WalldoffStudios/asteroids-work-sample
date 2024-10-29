using System;
using Asteroids.Managers;
using Asteroids.Utilities;
using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    public class RootInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            // Register SaveManager as a singleton
            builder.Register<SaveManager>(Lifetime.Singleton)
                .As<ISaveManager>()
                .As<IInitializable>()
                .As<IDisposable>();
            
            builder.Register<GameStateChangeNotifier>(Lifetime.Singleton)
                .As<IGameStateListener>()
                .As<IGameStateSubscription>();

            builder.Register<GameManager>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<IGameStateListener>());

            TimerUtilities timerUtilities = gameObject.AddComponent<TimerUtilities>();
            builder.RegisterComponent(timerUtilities).As<ITimerAction>();
        }
    }   
}
