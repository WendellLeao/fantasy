using System;

namespace Fantasy
{
    public interface IWeaponHolder
    {
        public event Action OnWeaponExecuted;
        
        public IWeapon Weapon { get; }
        
        public void ExecuteWeapon();
    }
}
