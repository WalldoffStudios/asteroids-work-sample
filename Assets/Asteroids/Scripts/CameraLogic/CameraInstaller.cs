using System;
using Asteroids.Borders;
using Asteroids.Managers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Asteroids.CameraLogic
{
    public class CameraInstaller : LifetimeScope
    {
        [SerializeField] private Camera mainCam = null;
        
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterInstance(mainCam);

            builder.Register<CameraFacade>(Lifetime.Singleton)
                .As<ICameraBoundsProvider>();
            
            builder.Register<ScreenBoundsProvider>(Lifetime.Singleton)
                .As<IScreenBoundsProvider>();
            
            builder.Register<ScreenBoundsHandler>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<IScreenBoundsProvider>());
            
            builder.Register<ScreenBoundsTransporter>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<ScreenBoundsHandler>())
                .WithParameter(resolver => resolver.Resolve<ILevelStateSubscription>())
                .As<IScreenBoundsTransporter>()
                .As<ITickable>()
                .As<ILevelStateListener>()
                .As<IDisposable>();
            
            builder.Register<ScreenBoundsRecycler>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<ScreenBoundsHandler>())
                .WithParameter(resolver => resolver.Resolve<ILevelStateSubscription>())
                .As<IScreenBoundsRecycler>()
                .As<ILevelStateListener>()
                .As<ITickable>()
                .As<IDisposable>();
            
            builder.Register<ScreenBorderPositionProvider>(Lifetime.Singleton)
                .As<IGetScreenBorderPosition>();

            builder.Register<ScreenMoveDirectionProvider>(Lifetime.Singleton)
                .As<IGetScreenMoveDirection>();
        }
    }   
}
