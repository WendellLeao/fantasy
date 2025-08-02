using Fantasy.Utilities;
using UnityEngine;

namespace Fantasy
{
    [CreateAssetMenu(menuName = PathUtility.WeaponsMenuPath + "/WeaponData", fileName = "NewWeaponData")]
    public sealed class WeaponData : ScriptableObject
    {
        [SerializeField]
        private string id;
        [SerializeField]
        private float staminaCost;
        [SerializeField]
        private float range;
        [SerializeField]
        private GameObject prefab;
        [SerializeField]
        private MovesetType movesetType;
        
        public string Id => id;
        public GameObject Prefab => prefab;
        public MovesetType MovesetType => movesetType;
    }
}
