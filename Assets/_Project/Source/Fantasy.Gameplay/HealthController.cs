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
            if (_isInvincible || HealthRatio <= 0f)
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

        public void SetIsInvincible(bool isInvincible)
        {
            _isInvincible = isInvincible;
        }

#if UNITY_EDITOR
        public void SetHealthDataForTests(HealthData healthData)
        {
            data = healthData;
        }
#endif

        #region Debug

        [Button]
        public void Increment50Health()
        {
            IncrementHealth(50);
        }
        
        [Button]
        public void Decrement50Health()
        {
            DecrementHealth(50);
        }

        #endregion
    }
}
