using Fantasy.Utilities;
using NaughtyAttributes;
using UnityEngine;

namespace Fantasy
{
    [CreateAssetMenu(menuName = PathUtility.DamageMenuPath + "/DamageData", fileName = "NewDamageData")]
    public sealed class DamageData : ScriptableObject
    {
        [SerializeField]
        private string id;
        [SerializeField]
        private float amount;
        [SerializeField]
        private bool hasDamagePerSecond;
        [ShowIf(condition: "hasDamagePerSecond")]
        [SerializeField]
        private float damagePerSecondDuration;
        [SerializeField]
        [ShowIf(condition: "hasDamagePerSecond")]
        private float amountPerSecond;
        [ShowIf(condition: "hasDamagePerSecond")]
        [SerializeField]
        private GameObject dpsParticlePrefab;
        
        public float Amount => amount;
        public bool HasDamagePerSecond => hasDamagePerSecond;
        public float AmountPerSecond => hasDamagePerSecond ? amountPerSecond : 0f;
        public float DamagePerSecondDuration => hasDamagePerSecond ? damagePerSecondDuration : 0f;
        public GameObject DpsParticlePrefab => hasDamagePerSecond ? dpsParticlePrefab : null;
    }
}
