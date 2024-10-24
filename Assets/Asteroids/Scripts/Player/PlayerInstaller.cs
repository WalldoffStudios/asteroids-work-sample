using Asteroids.Bullets;
using Asteroids.Player.InputHandling;
using Asteroids.Scripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    public class PlayerInstaller : LifetimeScope
    {
        [SerializeField] private ObjectFacade objectPrefab = null;
        [SerializeField] private float movementSpeed = 10.0f;
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            if (objectPrefab == null)
            {
                Debug.LogError("PlayerFacade prefab is not assigned in the PlayerInstaller.");
                return;
            }
            
            ObjectFacade objectInstance = Instantiate(objectPrefab);
            builder.RegisterComponent(objectInstance);
            
            builder.Register<ObjectMover>(Lifetime.Singleton)
                .WithParameter(objectInstance.RigidBody)
                .As<ISetMovementDirection>()
                .As<IFixedTickable>();
            
            builder.Register<ObjectRotator>(Lifetime.Singleton)
                .WithParameter(objectInstance.RigidBody)
                .As<ITickable>();
            
            builder.Register<MovementCalculator>(Lifetime.Singleton)
                .As<IAddMovementCalculation>()
                .As<ITickable>();
            
            builder.Register<MovementInputHandler>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<IAddMovementCalculation>())
                .WithParameter(movementSpeed)
                .As<ITickable>();

            builder.Register<PlayerShooting>(Lifetime.Singleton)
                .WithParameter(objectInstance.transform)
                .WithParameter(resolver => resolver.Resolve<IBulletFactory>())
                .As<ITickable>();

            //Todo: This part should maybe be in its own installer
            //TODO:-------------------------------------------------------------------------------------------
            // builder.Register<AimInputHandler>(Lifetime.Transient)
            //     .WithParameter(resolver => resolver.Resolve<IScreenToWorldPoint>())
            //     .As<ITickable>();
        }
    }   
}
