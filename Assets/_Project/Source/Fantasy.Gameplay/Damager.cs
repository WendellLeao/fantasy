using Fantasy.Domain.Health;
using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay
{
    internal sealed class Damager : EntityComponent
    {
        [SerializeField]
        private DamageData data;

        public bool TryApplyDamage(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable damageable))
            {
                return false;
            }
            
            damageable.TakeDamage(data);
            
            return true;
        }
    }
}
