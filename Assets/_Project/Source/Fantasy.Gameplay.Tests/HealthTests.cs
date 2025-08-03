using NUnit.Framework;
using UnityEngine;

namespace Fantasy.Gameplay.Tests
{
    internal sealed class HealthTests : MonoBehaviour
    {
        private HealthData _mockHealthData;
        private DamageData _mockDamageData;
        private HealthController _sut;

        [SetUp]
        public void SetUp()
        {
            _mockHealthData = GetMockHealthData();
            _mockDamageData = GetMockDamageData();
            _sut = GetHealthController();
            
            _sut.SetHealthDataForTests(_mockHealthData);
        }

        [TearDown]
        public void TearDown()
        {
            DestroyImmediate(_mockHealthData);
            DestroyImmediate(_mockDamageData);
            DestroyImmediate(_sut.gameObject);
        }

        [Test]
        public void TakeDamage_ReducesCurrentHealthByThreePercent()
        {
            // Arrange
            _mockDamageData.SetAmountForTests(30);
            _mockHealthData.SetMaxHealthForTests(100);
            
            _sut.Initialize();
            
            // Act
            _sut.TakeDamage(_mockDamageData);
            
            // Assert
            Assert.That(_sut.HealthRatio, expression: Is.EqualTo(expected: 0.7f).Within(0.0001f));
        }
        
        private HealthData GetMockHealthData()
        {
            return ScriptableObject.CreateInstance<HealthData>();
        }

        private HealthController GetHealthController()
        {
            GameObject newGameObject = new GameObject(name: $"Humble {nameof(HumbleEntity)}", components: new[]
            {
                typeof(HumbleEntity),
                typeof(HealthController)
            });

            return newGameObject.GetComponent<HealthController>();
        }
        
        private DamageData GetMockDamageData()
        {
            return ScriptableObject.CreateInstance<DamageData>();
        }
    }
}
