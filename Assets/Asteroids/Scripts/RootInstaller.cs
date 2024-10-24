using Asteroids.Bullets;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    public class RootInstaller : LifetimeScope
    {
        [SerializeField] private BulletDataCollection bulletDataCollection = null;
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterInstance(bulletDataCollection);
            builder.Register<IBulletFactory, BulletFactory>(Lifetime.Singleton);
        }
    }   
}
