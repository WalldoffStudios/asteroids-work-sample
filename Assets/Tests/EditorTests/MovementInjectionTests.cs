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
            
            builder.Register<PlayerMovement>(Lifetime.Singleton)
                .As<IApplyMovement>()
                .As<IFixedTickable>();
            
            builder.Register<MovementCalculator>(Lifetime.Singleton)
                .As<IAddMovementCalculation>()
                .As<ITickable>();
            
            builder.Register<MovementInputHandler>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<IAddMovementCalculation>())
                .WithParameter(50.0f) // Movement speed
                .As<ITickable>();
            
            _container = builder.Build();
        }

        [Test]
        public void PlayerMovementResolved()
        {
            IApplyMovement applyMovement = _container.Resolve<IApplyMovement>();
            
            Assert.IsNotNull(applyMovement, "IApplyMovement was not resolved.");
            Assert.IsInstanceOf<PlayerMovement>(applyMovement, "Resolved IApplyMovement is not of type PlayerMovement.");
        }
        
        [Test]
        public void MovementCalculatorResolved()
        {
            IAddMovementCalculation movementCalculation = _container.Resolve<IAddMovementCalculation>();
    
            // Assert
            Assert.IsNotNull(movementCalculation, "IAddMovementCalculation was not resolved.");
            Assert.IsInstanceOf<MovementCalculator>(movementCalculation, "Resolved IAddMovementCalculation is not of type MovementCalculator.");
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
