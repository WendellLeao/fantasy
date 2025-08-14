using Fantasy.Gameplay.Damage;
using Fantasy.Gameplay.Health;
using NUnit.Framework;
using UnityEngine;

namespace Fantasy.Gameplay.Tests
{
    internal sealed class HealthTests : MonoBehaviour
    {
        private HealthData _mockHealthData;
        private DamageData _mockDamageData;
        private HumbleEntity _humbleEntity;
        private DamageController _damageController;
        private HealthController _sut;

        [SetUp]
        public void SetUp()
        {
            _mockHealthData = GetMockHealthData();
            _mockDamageData = GetMockDamageData();

            _humbleEntity = CreateHumbleEntity();

            _sut = _humbleEntity.GetComponent<HealthController>();
            _damageController = _humbleEntity.GetComponent<DamageController>();
        }

        [TearDown]
        public void TearDown()
        {
            _humbleEntity.Dispose();
            
            DestroyImmediate(_mockHealthData);
            DestroyImmediate(_mockDamageData);
            DestroyImmediate(_humbleEntity.gameObject);
        }

        [Test]
        public void TakeDamage_DecreaseCurrentHealthByThreePercent()
        {
            // Arrange
            _mockDamageData.SetAmountForTests(30);
            _mockHealthData.SetMaxHealthForTests(100);
            
            _sut.SetHealthDataForTests(_mockHealthData);
            
            _humbleEntity.AddComponentsForTests(_sut, _damageController);
            
            _humbleEntity.SetUp();
            
            // Act
            _damageController.TakeDamage(_mockDamageData);
            
            // Assert
            Assert.That(_sut.HealthRatio, expression: Is.EqualTo(expected: 0.7f).Within(0.0001f));
        }
        
        private HealthData GetMockHealthData()
        {
            return ScriptableObject.CreateInstance<HealthData>();
        }

        private HumbleEntity CreateHumbleEntity()
        {
            GameObject humbleEntityObject = new GameObject(name: $"Humble {nameof(HumbleEntity)}", components: new[]
            {
                typeof(HumbleEntity),
                typeof(HealthController),
                typeof(DamageController)
            });

            return humbleEntityObject.GetComponent<HumbleEntity>();
        }
        
        private DamageData GetMockDamageData()
        {
            return ScriptableObject.CreateInstance<DamageData>();
        }
    }
}
