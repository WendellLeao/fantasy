using UnityEngine;

namespace Fantasy.Gameplay.Health
{
    internal sealed class HealthModel
    {
        private readonly float _maxHealth;
        private float _currentHealth;

        public float HealthRatio => _currentHealth / _maxHealth;
        
        public HealthModel(HealthData healthData)
        {
            _maxHealth = healthData.MaxHealth;
            _currentHealth = _maxHealth;
        }
        
        public void IncrementHealth(float amount)
        {
            SetCurrentHealth(_currentHealth + amount);
        }

        public void DecrementHealth(float amount)
        {
            SetCurrentHealth(_currentHealth - amount);
        }

        private void SetCurrentHealth(float currentHealth)
        {
            _currentHealth = Mathf.Clamp(currentHealth, min: 0f, _maxHealth);
        }
    }
}
