using System;
using Leaosoft;
using NaughtyAttributes;
using UnityEngine;

namespace Fantasy.Gameplay.Health
{
    public sealed class HealthController : EntityComponent, IHealth
    {
        public event Action<float> OnHealthChanged;
        public event Action OnDepleted;
        
        [SerializeField]
        private HealthData data;
        [SerializeField]
        private Transform healthBarParent;

        private HealthModel _healthModel;

        public Transform HealthBarParent => healthBarParent;
        public float HealthRatio => _healthModel.HealthRatio;

        public void IncrementHealth(float amount)
        {
            if (HealthRatio <= 0f)
            {
                return;
            }
            
            _healthModel.IncrementHealth(amount);

            DispatchHealthChangedEvent();
        }
        
        public void DecrementHealth(float amount)
        {
            _healthModel.DecrementHealth(amount);

            DispatchHealthChangedEvent();

            if (HealthRatio <= 0f)
            {
                OnDepleted?.Invoke();
            }
        }
        
        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            _healthModel = new HealthModel(data);
        }

        private void DispatchHealthChangedEvent()
        {
            OnHealthChanged?.Invoke(HealthRatio);
        }
        
#if UNITY_EDITOR
        [Button("IncrementHealth_IncrementBy50")]
        public void IncrementHealth_IncrementBy50()
        {
            IncrementHealth(amount: 50);
        }
        
        public void SetHealthDataForTests(HealthData healthData)
        {
            data = healthData;
        }
#endif
    }
}
