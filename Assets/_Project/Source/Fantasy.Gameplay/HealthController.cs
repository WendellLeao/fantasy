using System;
using Fantasy.Domain.Health;
using Leaosoft;
using NaughtyAttributes;
using UnityEngine;

namespace Fantasy.Gameplay
{
    public sealed class HealthController : EntityComponent, IDamageable
    {
        public event Action<float> OnHealthChanged;
        public event Action<DamageData> OnDamageTaken;
        public event Action OnDied;
        
        [SerializeField]
        private HealthData data;
        [SerializeField]
        private Transform healthBarParent;

        private HealthModel _healthModel;
        private float _damagePerSecondDuration;
        private float _amountDamagePerSecond;
        private bool _isInvincible;

        public Transform HealthBarParent => healthBarParent;
        public float HealthRatio => _healthModel.HealthRatio;
        public bool IsTakingDamage => _damagePerSecondDuration > 0f;
        
        public void TakeDamage(DamageData damageData)
        {
            if (!CanTakeDamage())
            {
                return;
            }
            
            DecrementHealth(damageData.Amount);

            if (damageData.HasDamagePerSecond)
            {
                _damagePerSecondDuration += damageData.DamagePerSecondDuration;
                _amountDamagePerSecond += damageData.AmountPerSecond;
            }
            
            OnDamageTaken?.Invoke(damageData);

            if (HealthRatio <= 0f)
            {
                OnDied?.Invoke();
            }
        }

        public void Heal(float amount)
        {
            if (HealthRatio <= 0f)
            {
                return;
            }
            
            IncrementHealth(amount);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            _healthModel = new HealthModel(data);
        }

        protected override void OnTick(float deltaTime)
        {
            base.OnTick(deltaTime);
            
            if (IsTakingDamage)
            {
                ApplyDamagePerSecond(deltaTime);

                _damagePerSecondDuration -= deltaTime;
            }
        }

        private void IncrementHealth(float amount)
        {
            _healthModel.IncrementHealth(amount);

            DispatchHealthChangedEvent();
        }
        
        private void DecrementHealth(float amount)
        {
            _healthModel.DecrementHealth(amount);

            DispatchHealthChangedEvent();
        }
        
        private void ApplyDamagePerSecond(float deltaTime)
        {
            float amount = _amountDamagePerSecond * deltaTime;

            DecrementHealth(amount);
        }

        private void DispatchHealthChangedEvent()
        {
            OnHealthChanged?.Invoke(HealthRatio);
        }

        private bool CanTakeDamage()
        {
            return IsEnabled && !_isInvincible && HealthRatio > 0f;
        }
        
        public void SetIsInvincible(bool isInvincible)
        {
            _isInvincible = isInvincible;
        }

#if UNITY_EDITOR
        [Button("Heal_IncrementCurrentHealthBy50")]
        public void Heal_IncrementCurrentHealthBy50()
        {
            Heal(amount: 50);
        }
        
        [Button("TakeDamage_DecreaseCurrentHealthBy50")]
        public void TakeDamage_DecreaseCurrentHealthBy50()
        {
            DamageData mockDamageData = ScriptableObject.CreateInstance<DamageData>(); 
            
            mockDamageData.SetAmountForTests(50);
            
            TakeDamage(mockDamageData);
        }
        
        public void SetHealthDataForTests(HealthData healthData)
        {
            data = healthData;
        }
#endif
    }
}
