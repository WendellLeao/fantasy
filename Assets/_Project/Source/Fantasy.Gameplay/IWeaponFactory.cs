using UnityEngine;

namespace Fantasy.Gameplay
{
    internal interface IWeaponFactory
    {
        public IWeapon CreateWeapon(WeaponData data, Transform parent);
    }
}
