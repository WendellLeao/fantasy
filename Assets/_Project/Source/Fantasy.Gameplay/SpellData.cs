using Fantasy.Utilities;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.Gameplay
{
    [CreateAssetMenu(menuName = PathUtility.SpellsMenuPath + "/SpellData", fileName = "NewSpellData")]
    public sealed class SpellData : ScriptableObject
    {
        [SerializeField]
        private string id;
        [SerializeField]
        private PoolData poolData;

        public PoolData PoolData => poolData;
    }
}
