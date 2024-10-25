using Asteroids.Borders;
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
                .As<ICameraBoundsProvider>()
                .As<IScreenToWorldPoint>();
            
            builder.Register<ScreenBoundsProvider>(Lifetime.Singleton)
                .As<IScreenBoundsProvider>();
            
            builder.Register<ScreenBoundsHandler>(Lifetime.Singleton)
                .WithParameter(resolver => resolver.Resolve<IScreenBoundsProvider>());
            
            builder.Register<TransformWrapHandler>(Lifetime.Singleton)
                .As<IRegisterWrappingTransform>()
                .As<IUnregisterWrappingTransform>()
                .As<IFixedTickable>();
        }
    }   
}
