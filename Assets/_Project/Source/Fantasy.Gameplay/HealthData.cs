using Fantasy.Utilities;
using UnityEngine;

namespace Fantasy.Gameplay
{
    [CreateAssetMenu(menuName = PathUtility.HealthMenuPath + "/HealthData", fileName = "NewHealthData")]
    public sealed class HealthData : ScriptableObject
    {
        [SerializeField]
        private string id;
        [SerializeField]
        private float maxHealth;

        public float MaxHealth => maxHealth;

        public void SetMaxHealth(float newMaxHealth)
        {
            maxHealth = newMaxHealth;
        }
    }
}
