using System;

namespace Fantasy.Gameplay
{
    public interface IWeaponHolder
    {
        public event Action<IWeapon> OnWeaponChanged;
        public event Action OnWeaponExecuted;
        public event Action OnWeaponExecutionFinished;
        
        public IWeapon Weapon { get; }

        public void ChangeWeapon(WeaponData weaponData);
        public void ExecuteWeapon();
        public void FinishWeaponExecution();
    }
}
