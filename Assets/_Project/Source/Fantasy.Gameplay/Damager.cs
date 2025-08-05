using Fantasy.SharedKernel.Health;
using UnityEngine;

namespace Fantasy.Gameplay
{
    internal sealed class Damager : MonoBehaviour
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
