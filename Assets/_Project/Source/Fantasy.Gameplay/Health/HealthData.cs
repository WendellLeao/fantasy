using Fantasy.Utilities;
using UnityEngine;

namespace Fantasy.Gameplay.Health
{
    [CreateAssetMenu(menuName = PathUtility.HealthMenuPath + "/HealthData", fileName = "NewHealthData")]
    public sealed class HealthData : ScriptableObject
    {
        [SerializeField]
        private string id;
        [SerializeField]
        private float maxHealth;

        public float MaxHealth => maxHealth;

#if UNITY_EDITOR
        public void SetMaxHealthForTests(float newMaxHealth)
        {
            maxHealth = newMaxHealth;
        }
#endif
    }
}
