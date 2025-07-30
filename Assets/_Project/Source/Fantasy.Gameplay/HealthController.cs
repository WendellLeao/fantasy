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
        private float _damagePerSecondCountdown;
        private float _damagePerSecond;

        public Transform HealthBarParent => healthBarParent;
        public float HealthRatio => _healthModel.HealthRatio;

        public void ApplyDamage(DamageData damageData)
        {
            DecrementHealth(damageData.Amount);

            if (damageData.HasDamagePerSecond)
            {
                _damagePerSecondCountdown += damageData.DamagePerSecondDuration;
                _damagePerSecond += damageData.AmountPerSecond;
            }
            
            OnDamageTaken?.Invoke(damageData);

            DispatchHealthChangedEvent();
        }
        
        public void IncrementHealth(float amount)
        {
            _healthModel.IncrementHealth(amount);

            DispatchHealthChangedEvent();
        }
        
        public void DecrementHealth(float amount)
        {
            _healthModel.DecrementHealth(amount);

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
            
            if (_damagePerSecondCountdown > 0f)
            {
                ApplyDamagePerSecond(deltaTime);

                _damagePerSecondCountdown -= deltaTime;
            }
        }

        private void ApplyDamagePerSecond(float deltaTime)
        {
            float amount = _damagePerSecond * deltaTime;

            DecrementHealth(amount);
        }

        private void DispatchHealthChangedEvent()
        {
            OnHealthChanged?.Invoke(HealthRatio);
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
