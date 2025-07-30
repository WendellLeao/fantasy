using Fantasy.Utilities;
using UnityEngine;

namespace Fantasy.Gameplay
{
    [CreateAssetMenu(menuName = PathUtility.WeaponsMenuPath + "/WeaponData", fileName = "NewWeaponData")]
    internal sealed class WeaponData : ScriptableObject
    {
        [SerializeField]
        private string id;
        [SerializeField]
        private float staminaCost;
        [SerializeField]
        private float range;
        [SerializeField]
        private GameObject prefab;
        
        public GameObject Prefab => prefab;
    }
}
