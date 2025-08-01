using System;

namespace Fantasy
{
    public interface IWeaponHolder
    {
        public event Action OnWeaponExecuted;
        
        public void ExecuteWeapon();
    }
}
