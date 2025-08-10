using Fantasy.UI.Health.Manager;
using Leaosoft.Events;
using Leaosoft.Pooling;
using Leaosoft.Services;
using UnityEngine;

namespace Fantasy.UI.System
{
    internal sealed class UISystem : Leaosoft.System
    {
        protected override void InitializeManagers()
        {
            if (TryGetManager(out HealthViewManager healthViewsManager))
            {
                IPoolingService poolingService = ServiceLocator.GetService<IPoolingService>();
                IEventService eventService = ServiceLocator.GetService<IEventService>();
                
                // TODO: camera service
                healthViewsManager.Initialize(Camera.main, poolingService, eventService);
            }
        }
    }
}
