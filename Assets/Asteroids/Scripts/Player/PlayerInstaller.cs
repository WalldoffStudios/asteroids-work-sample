using Asteroids.Bullets;
using Asteroids.Player.InputHandling;
using Asteroids.Weapons;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
    public class PlayerInstaller : LifetimeScope
    {
        [SerializeField] private LazerWeapon weapon = null;
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

            LazerWeapon lazerWeapon = Instantiate(weapon, objectInstance.transform);
            builder.RegisterComponent(lazerWeapon).As<IWeapon>();

            builder.Register<PlayerShooting>(Lifetime.Singleton)
                .WithParameter(objectInstance.transform)
                .WithParameter(resolver => resolver.Resolve<IWeapon>())
                .WithParameter(resolver => resolver.Resolve<IBulletFactory>())
                .As<IUpdateWeapon>()
                .As<ITickable>();
        }
    }   
}
