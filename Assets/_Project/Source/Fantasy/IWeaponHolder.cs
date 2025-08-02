using System;

namespace Fantasy
{
    public interface IWeaponHolder
    {
        public event Action OnWeaponExecuted;
        public event Action<IWeapon> OnWeaponChanged;
        
        public IWeapon Weapon { get; }
        
        public void ExecuteWeapon();
    }
}
