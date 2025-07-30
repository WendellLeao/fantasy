using System;
using Fantasy.Gameplay;
using UnityEngine;

namespace Fantasy
{
    public interface IDamageable
    {
        public event Action<float> OnHealthChanged;
        public event Action<DamageData> OnDamageTaken;
        
        public Transform HealthBarParent { get; }
        public float HealthRatio { get; }
        
        public void ApplyDamage(DamageData damageData);
    }
}
