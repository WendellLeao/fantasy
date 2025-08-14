using Fantasy.UI.Health.Manager;
using Leaosoft.Events;
using Leaosoft.Pooling;
using Leaosoft.Services;
using UnityEngine;

namespace Fantasy.UI.System
{
    internal sealed class UISystem : Leaosoft.System
    {
        [SerializeField]
        private HealthViewManager healthViewsManager;
        
        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            IPoolingService poolingService = ServiceLocator.GetService<IPoolingService>();
            IEventService eventService = ServiceLocator.GetService<IEventService>();
                
            // TODO: camera service
            healthViewsManager.SetUp(Camera.main, poolingService, eventService);
            
            RegisterManagers(healthViewsManager);
        }
    }
}
