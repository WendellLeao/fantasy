using System;
using Leaosoft;
using NaughtyAttributes;
using UnityEngine;

namespace Fantasy.Gameplay
{
    internal sealed class HealthController : EntityComponent, IDamageable
    {
        public event Action<float> OnHealthChanged;
        public event Action<DamageData> OnDamageTaken;
        
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

        public void ApplyDamage(DamageData damageData)
        {
            if (_isInvincible)
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

            DispatchHealthChangedEvent();
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

        #region DEBUG
        
        [Button]
        private void DecrementHealth()
        {
            DecrementHealth(amount: 10);
        }
        
        [Button]
        private void IncrementHealth()
        {
            IncrementHealth(amount: 10);
        }

        #endregion
    }
}
