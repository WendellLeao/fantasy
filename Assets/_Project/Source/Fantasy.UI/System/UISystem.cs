using Fantasy.UI.Health.Manager;
using Leaosoft.Events;
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
                IEventService eventService = ServiceLocator.GetService<IEventService>();
                
                healthViewsManager.Initialize(Camera.main, eventService);
            }
        }
    }
}
