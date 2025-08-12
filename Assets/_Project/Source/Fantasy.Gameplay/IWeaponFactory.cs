using UnityEngine;

namespace Fantasy.Gameplay
{
    public interface IWeaponFactory
    {
        public IWeapon CreateWeapon(WeaponData data, Transform parent);
        public virtual void DisposeEntity(IWeapon weapon) { }
    }
}
