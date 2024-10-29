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
            
            builder.RegisterInstance(bulletDataCollection);
            
            builder.Register<IBulletPool>(resolver =>
            {
                return new BulletPool(
                    bulletDataCollection,
                    parentTransform: transform,
                    resolver,
                    initialPoolSize: initialPoolSize
                );
            }, Lifetime.Singleton);
            
            builder.Register<IBulletFactory, BulletFactory>(Lifetime.Singleton);
        }
    }   
}
