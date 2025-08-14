using UnityEngine;

namespace Fantasy.Gameplay
{
    internal interface IDamager
    {
        public void Initialize();
        public bool TryApplyDamage(Collider other);
    }
}
