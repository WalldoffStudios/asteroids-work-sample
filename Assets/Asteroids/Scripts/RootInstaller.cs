using Asteroids.Scripts.Managers;
using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    public class RootInstaller : LifetimeScope
    {
        // [SerializeField] private BulletDataCollection bulletDataCollection = null;
        // [SerializeField] private int initialPoolSize = 20;
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            // // Register BulletDataCollection
            // builder.RegisterInstance(bulletDataCollection);
            //
            // // Register BulletPool
            // builder.Register<IBulletPool>(resolver =>
            // {
            //     return new BulletPool(
            //         bulletDataCollection,
            //         parentTransform: this.transform,
            //         initialPoolSize: initialPoolSize
            //     );
            // }, Lifetime.Singleton);
            //
            // // Register BulletFactory
            // builder.Register<IBulletFactory, BulletFactory>(Lifetime.Singleton);

            builder.Register<GameStateChangeNotifier>(Lifetime.Singleton)
                .As<IGameStateListener>()
                .As<IGameStateSubscription>();

            builder.Register<GameManager>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<IGameStateListener>());
        }
    }   
}
