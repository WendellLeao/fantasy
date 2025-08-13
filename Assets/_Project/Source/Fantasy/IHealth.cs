using System;
using UnityEngine;

namespace Fantasy
{
    public interface IHealth
    {
        public event Action<float> OnHealthChanged;
        public event Action OnDepleted;
        
        public Transform HealthBarParent { get; }
        public float HealthRatio { get; }

        public void Initialize();
        public void IncrementHealth(float amount);
        public void DecrementHealth(float amount);
    }
}
