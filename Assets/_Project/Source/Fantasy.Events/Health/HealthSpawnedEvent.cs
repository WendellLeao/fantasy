using Fantasy.Domain.Health;
using Leaosoft.Events;

namespace Fantasy.Events.Health
{
    public sealed class HealthSpawnedEvent : GameEvent
    {
        public IHealth Health { get; private set; }

        public HealthSpawnedEvent(IHealth health)
        {
            Health = health;
        }
    }
}
