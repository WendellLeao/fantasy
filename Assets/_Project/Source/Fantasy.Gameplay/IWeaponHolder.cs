using System;
using Leaosoft;

namespace Fantasy.Gameplay
{
    public interface IWeaponHolder : IEntityComponent
    {
        public event Action<IWeapon> OnWeaponChanged;
        public event Action OnWeaponExecuted;
        
        public IWeapon Weapon { get; }

        public void SetUp(IWeaponFactory weaponFactory);
        public void ChangeWeapon(WeaponData weaponData);
        public void ExecuteWeapon();
        public void FinishWeaponExecution();
    }
}
