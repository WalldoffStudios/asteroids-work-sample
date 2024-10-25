using Asteroids.Borders;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids.Bullets
{
    public class BulletInstaller : LifetimeScope
    {
        [SerializeField] private BulletDataCollection bulletDataCollection = null;
        [SerializeField] private int initialPoolSize = 20;
        
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
            
            // Register BulletDataCollection
            builder.RegisterInstance(bulletDataCollection);

            // Register ScreenBoundsProvider and ScreenBoundsHandler
            //builder.Register<IScreenBoundsProvider, ScreenBoundsProvider>(Lifetime.Singleton);
            //builder.Register<ScreenBoundsHandler>(Lifetime.Singleton);

            // Register TransformWrapRecycler and its interfaces
            builder.Register<TransformWrapRecycler>(Lifetime.Singleton)
                .As<IRegisterRecyclableTransform>()
                .As<IUnregisterRecyclableTransform>()
                .As<IFixedTickable>();

            // Register BulletPool
            builder.Register<IBulletPool>(resolver =>
            {
                return new BulletPool(
                    bulletDataCollection,
                    parentTransform: this.transform,
                    resolver,
                    initialPoolSize: initialPoolSize
                );
            }, Lifetime.Singleton);

            // Register BulletFactory
            builder.Register<IBulletFactory, BulletFactory>(Lifetime.Singleton);
        }
    }   
}
