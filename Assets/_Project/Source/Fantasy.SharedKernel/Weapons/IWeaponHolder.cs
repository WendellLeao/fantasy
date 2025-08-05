using System;

namespace Fantasy.SharedKernel.Weapons
{
    public interface IWeaponHolder
    {
        public event Action<IWeapon> OnWeaponChanged;
        public event Action OnWeaponExecuted;
        
        public IWeapon Weapon { get; }

        public void ChangeWeapon(WeaponData weaponData);
        public void ExecuteWeapon();
    }
}
