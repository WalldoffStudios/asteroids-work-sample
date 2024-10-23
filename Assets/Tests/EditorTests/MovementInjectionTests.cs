using NUnit.Framework;
using VContainer;
using VContainer.Unity;

namespace Asteroids.Tests.EditMode
{
    public class DependencyRegistrationTests
    {
        private IObjectResolver _container;

        [SetUp]
        public void Setup()
        {
            var builder = new ContainerBuilder();
            
            builder.Register<PlayerMovement>(Lifetime.Singleton).As<IMoveInputCollector>();
            builder.Register<MovementInputHandler>(Lifetime.Singleton).As<ITickable>();
            
            _container = builder.Build();
        }

        [Test]
        public void PlayerMovementResolvedAsIMoveInputCollector()
        {
            IMoveInputCollector moveInputCollector = _container.Resolve<IMoveInputCollector>();
    
            Assert.IsNotNull(moveInputCollector, "IMoveInputCollector was not resolved.");
            Assert.IsInstanceOf<PlayerMovement>(moveInputCollector, "Resolved IMoveInputCollector is not of type PlayerMovement.");
        }

        [Test]
        public void MovementInputHandlerResolvedAsITickable()
        {
            ITickable tickable = _container.Resolve<ITickable>();
    
            Assert.IsNotNull(tickable, "ITickable was not resolved.");
            Assert.IsInstanceOf<MovementInputHandler>(tickable, "Resolved ITickable is not of type MovementInputHandler.");
        }

    }
}