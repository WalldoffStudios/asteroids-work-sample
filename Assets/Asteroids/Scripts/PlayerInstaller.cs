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

            // Instantiate the PlayerFacade prefab in the scene
            PlayerFacade playerInstance = Instantiate(playerPrefab);
            
            // Step 2: Retrieve the Rigidbody2D component from the instantiated PlayerFacade
            Rigidbody2D rb = playerInstance.RigidBody;

            if (rb == null)
            {
                Debug.LogError("Rigidbody2D component is missing in the PlayerFacade prefab.");
                return;
            }
            
            //Todo: Instead of RigidBody2D I should make an interface on the RigidBody2D 
            builder.RegisterInstance(rb).As<Rigidbody2D>().AsSelf();
            
            builder.Register<PlayerMovement>(Lifetime.Singleton).As<IApplyMovement>().As<IFixedTickable>();
            builder.Register<MovementCalculator>(Lifetime.Singleton).As<IAddMovementCalculation>().As<ITickable>();
            builder.Register<MovementInputHandler>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<IAddMovementCalculation>())
                .WithParameter(movementSpeed)
                .As<ITickable>();
        }
    }   
}
