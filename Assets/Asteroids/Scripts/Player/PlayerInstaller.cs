using Asteroids.Player.InputHandling;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    public class PlayerInstaller : LifetimeScope
    {
        [SerializeField] private PlayerFacade playerPrefab = null;
        [SerializeField] private float movementSpeed = 10.0f;
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            
            if (playerPrefab == null)
            {
                Debug.LogError("PlayerFacade prefab is not assigned in the PlayerInstaller.");
                return;
            }
            
            PlayerFacade playerInstance = Instantiate(playerPrefab);
            builder.RegisterComponent(playerInstance);
            
            builder.Register<PlayerMovement>(Lifetime.Singleton)
                .WithParameter(playerInstance.RigidBody)
                .As<IApplyMovement>()
                .As<IFixedTickable>();
            
            builder.Register<MovementCalculator>(Lifetime.Singleton).As<IAddMovementCalculation>().As<ITickable>();
            
            builder.Register<MovementInputHandler>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<IAddMovementCalculation>())
                .WithParameter(movementSpeed)
                .As<ITickable>();
        }
    }   
}
