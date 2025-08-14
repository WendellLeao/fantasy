using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay
{
    internal interface IDamager : IEntityComponent
    {
        public bool TryApplyDamage(Collider other);
    }
}
