using Asteroids.Movement;
using Asteroids.Player.InputHandling;
using NUnit.Framework;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids.Tests.EditMode
{
    public class MovementInjectionTests
    {
        private IObjectResolver _container;
        private Rigidbody2D _mockRigidbody;

        [SetUp]
        public void Setup()
        {
            var builder = new ContainerBuilder();
            
            _mockRigidbody = new Rigidbody2D();
            builder.RegisterInstance(_mockRigidbody).As<Rigidbody2D>().AsSelf();
            
            builder.Register<ContinuousMover>(Lifetime.Singleton)
                .As<ISetMovement>()
                .As<IFixedTickable>();
            
            builder.Register<MovementInputHandler>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<ISetMovement>())
                .WithParameter(50.0f) // Movement speed
                .As<ITickable>();
            
            _container = builder.Build();
        }

        [Test]
        public void PlayerMovementResolved()
        {
            ISetMovement setMovement = _container.Resolve<ISetMovement>();
            
            Assert.IsNotNull(setMovement, "ISetMovementDirection was not resolved.");
            Assert.IsInstanceOf<ContinuousMover>(setMovement, "Resolved ISetMovementDirection is not of type PlayerMovement.");
        }

        [Test]
        public void MovementInputHandlerResolved()
        {
            ITickable tickable = _container.Resolve<ITickable>();
            
            Assert.IsNotNull(tickable, "ITickable was not resolved.");
            Assert.IsInstanceOf<MovementInputHandler>(tickable, "Resolved ITickable is not of type MovementInputHandler.");
        }
    }
}
