using System;

namespace Fantasy
{
    public interface IWeaponHolder
    {
        public event Action<IWeapon> OnWeaponChanged;
        
        public IWeapon Weapon { get; }

        public void ChangeWeapon(WeaponData weaponData);
        public void ExecuteWeapon();
    }
}
