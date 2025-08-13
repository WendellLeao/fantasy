using System;
using Leaosoft;
using NaughtyAttributes;
using UnityEngine;

namespace Fantasy.Gameplay.Damage
{
    public sealed class DamageController : EntityComponent, IDamageable
    {
        public event Action<DamageData> OnDamageTaken;
        
        private IHealth _health;
        private float _damagePerSecondDuration;
        private float _amountDamagePerSecond;
        private bool _isInvincible;

        public void Initialize(IHealth health)
        {
            _health = health;
            
            base.Initialize();
        }
        
        public void TakeDamage(DamageData damageData)
        {
            if (!CanTakeDamage())
            {
                return;
            }

            if (damageData.HasDamagePerSecond)
            {
                _damagePerSecondDuration += damageData.DamagePerSecondDuration;
                _amountDamagePerSecond += damageData.AmountPerSecond;
            }
            
            _health.DecrementHealth(damageData.Amount);
            
            OnDamageTaken?.Invoke(damageData);
        }
        
        protected override void OnTick(float deltaTime)
        {
            base.OnTick(deltaTime);
            
            if (_damagePerSecondDuration > 0f)
            {
                ApplyDamagePerSecond(deltaTime);

                _damagePerSecondDuration -= deltaTime;
            }
        }
        
        private void ApplyDamagePerSecond(float deltaTime)
        {
            float amount = _amountDamagePerSecond * deltaTime;

            _health.DecrementHealth(amount);
        }
        
        private bool CanTakeDamage()
        {
            return IsEnabled && !_isInvincible && _health.HealthRatio > 0f;
        }
        
        public void SetIsInvincible(bool isInvincible)
        {
            _isInvincible = isInvincible;
        }

#if UNITY_EDITOR
        [Button("TakeDamage_DecreaseCurrentHealthBy50")]
        public void TakeDamage_DecreaseCurrentHealthBy50()
        {
            DamageData mockDamageData = ScriptableObject.CreateInstance<DamageData>(); 
            
            mockDamageData.SetAmountForTests(50);
            
            TakeDamage(mockDamageData);
        }
        
        [Button("TakeDamage_Die")]
        public void TakeDamage_Die()
        {
            DamageData mockDamageData = ScriptableObject.CreateInstance<DamageData>(); 
            
            mockDamageData.SetAmountForTests(99999999);
            
            TakeDamage(mockDamageData);
        }
#endif
    }
}
