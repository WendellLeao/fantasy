using Fantasy.Utilities;
using UnityEngine;

namespace Fantasy.Gameplay
{
    [CreateAssetMenu(menuName = PathUtility.HealthMenuPath + "/HealthData", fileName = "NewHealthData")]
    internal sealed class HealthData : ScriptableObject
    {
        [SerializeField]
        private string id;
        [SerializeField]
        private float maxHealth;

        public float MaxHealth => maxHealth;
    }
}
