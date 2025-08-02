using System;
using UnityEngine;

namespace Fantasy
{
    public interface IDamageable
    {
        public event Action<float> OnHealthChanged;
        public event Action<DamageData> OnDamageTaken;
        public event Action OnDied;
        
        public Transform HealthBarParent { get; }
        public float HealthRatio { get; }
        public bool IsTakingDamage { get; }
        
        public void TakeDamage(DamageData damageData);
        public void SetIsInvincible(bool isInvincible);
    }
}
