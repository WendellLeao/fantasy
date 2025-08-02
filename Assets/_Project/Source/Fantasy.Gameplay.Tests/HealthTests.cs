using NUnit.Framework;
using UnityEngine;

namespace Fantasy.Gameplay.Tests
{
    internal sealed class HealthTests : MonoBehaviour
    {
        private HealthData _mockHealthData;
        private DamageData _mockDamageData;
        private HealthController _healthController;

        [SetUp]
        public void SetUp()
        {
            _mockHealthData = GetMockHealthData();
            _mockDamageData = GetMockDamageData();
            
            _healthController = GetHealthController();
            
            _healthController.SetHealthData(_mockHealthData);
            
            _healthController.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            DestroyImmediate(_mockHealthData);
            DestroyImmediate(_mockDamageData);
            DestroyImmediate(_healthController.gameObject);
        }

        [Test]
        public void TakeDamage_DecrementCurrentHealth()
        {
            // Takes 30 of damage
            _healthController.TakeDamage(_mockDamageData);
            
            Assert.AreEqual(0.7f, _healthController.HealthRatio);
        }
        
        private HealthData GetMockHealthData()
        {
            HealthData healthData = ScriptableObject.CreateInstance<HealthData>();
            
            healthData.SetMaxHealth(100);

            return healthData;
        }

        private HealthController GetHealthController()
        {
            GameObject newGameObject = new GameObject();

            newGameObject.AddComponent<HumbleEntity>();
            
            return newGameObject.AddComponent<HealthController>();
        }
        
        private DamageData GetMockDamageData()
        {
            DamageData damageData = ScriptableObject.CreateInstance<DamageData>();
            
            damageData.SetAmount(30);

            return damageData;
        }
    }
}
