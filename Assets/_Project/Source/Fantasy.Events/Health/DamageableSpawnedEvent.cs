using Leaosoft.Events;

namespace Fantasy.Events.Health
{
    public sealed class DamageableSpawnedEvent : GameEvent
    {
        public IDamageable Damageable { get; private set; }

        public DamageableSpawnedEvent(IDamageable damageable)
        {
            Damageable = damageable;
        }
    }
}
