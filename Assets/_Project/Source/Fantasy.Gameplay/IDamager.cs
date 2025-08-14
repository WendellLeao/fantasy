using UnityEngine;

namespace Fantasy.Gameplay
{
    internal interface IDamager
    {
        public void SetUp();
        public bool TryApplyDamage(Collider other);
    }
}
