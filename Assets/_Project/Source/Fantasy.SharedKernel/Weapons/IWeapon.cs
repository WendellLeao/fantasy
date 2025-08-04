using System;

namespace Fantasy.SharedKernel.Weapons
{
    public interface IWeapon
    {
        public event Action OnExecuted;
        
        public WeaponData Data { get; }
        
        public void Initialize(WeaponData data);
        public void Begin();
        public void Execute();
        public void SetColliderEnabled(bool isEnabled);
    }
}
