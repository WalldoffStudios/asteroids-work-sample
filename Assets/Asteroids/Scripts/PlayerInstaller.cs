using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    public class PlayerInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<PlayerMovement>(Lifetime.Singleton).As<IMoveInputCollector>();
            builder.Register<MovementInputHandler>(Lifetime.Singleton).As<ITickable>();
        }
    }   
}
