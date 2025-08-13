using UnityEngine;

namespace Fantasy.Gameplay
{
    public interface IDamager
    {
        public void Initialize();
        public bool TryApplyDamage(Collider other);
    }
}
