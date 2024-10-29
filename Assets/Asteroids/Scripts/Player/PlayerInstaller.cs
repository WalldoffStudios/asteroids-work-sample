using Asteroids.Bullets;
using Asteroids.Managers;
using Asteroids.Movement;
using Asteroids.Player.InputHandling;
using Asteroids.Weapons;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    public class PlayerInstaller : LifetimeScope
    {
        [SerializeField] private LazerWeapon weapon = null;
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
            
            builder.Register<ContinuousMover>(Lifetime.Singleton)
                .WithParameter(playerInstance.RigidBody)
                .As<ISetMovementDirection>()
                .As<IFixedTickable>();
            
            builder.Register<ObjectRotator>(Lifetime.Singleton)
                .WithParameter(playerInstance.RigidBody)
                .As<ITickable>();
            
            builder.Register<MovementCalculator>(Lifetime.Singleton)
                .As<IAddMovementCalculation>()
                .As<ITickable>();
            
            builder.Register<MovementInputHandler>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<IAddMovementCalculation>())
                .WithParameter(movementSpeed)
                .AsImplementedInterfaces();

            LazerWeapon lazerWeapon = Instantiate(weapon, playerInstance.transform);
            builder.RegisterComponent(lazerWeapon).As<IWeapon>();

            builder.Register<PlayerShooting>(Lifetime.Singleton)
                .WithParameter(playerInstance.transform)
                .WithParameter(resolver => resolver.Resolve<IWeapon>())
                .WithParameter(resolver => resolver.Resolve<IBulletFactory>())
                .WithParameter(resolver => resolver.Resolve<ILevelStateSubscription>())
                .As<IUpdateWeapon>()
                .As<ITickable>();
        }
    }   
}
