using System;
using Leaosoft;
using UnityEngine;

namespace Fantasy
{
    public interface IHealth : IEntityComponent
    {
        public event Action<float> OnHealthChanged;
        public event Action OnDepleted;
        
        public Transform HealthBarParent { get; }
        public float HealthRatio { get; }

        public void SetUp();
        public void IncrementHealth(float amount);
        public void DecrementHealth(float amount);
    }
}
