using Fantasy.Utilities;
using UnityEngine;

namespace Fantasy.Gameplay
{
    [CreateAssetMenu(menuName = PathUtility.SpellsMenuPath + "/SpellData", fileName = "NewSpellData")]
    public sealed class SpellData : ScriptableObject
    {
        [SerializeField]
        private string id;
        [SerializeField]
        private GameObject prefab;

        public GameObject Prefab => prefab;
    }
}
