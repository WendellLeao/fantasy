using System;
using Fantasy.SharedKernel.Weapons;
using Leaosoft;
using NaughtyAttributes;
using UnityEngine;

namespace Fantasy.Gameplay
{
    internal sealed class WeaponHolder : EntityComponent, IWeaponHolder
    {
        public event Action OnWeaponExecuted;
        public event Action<IWeapon> OnWeaponChanged;

        [SerializeField]
        private WeaponData data;
        [SerializeField]
        private Transform parent;
        
        private IWeaponFactory _weaponFactory;
        private IWeapon _weapon;

        public IWeapon Weapon => _weapon;
        
        public void Initialize(IWeaponFactory weaponFactory)
        {
            _weaponFactory = weaponFactory;
            
            base.Initialize();
        }

        public void ChangeWeapon(WeaponData weaponData)
        {
            if (_weapon != null)
            {
                _weaponFactory.DisposeWeapon(_weapon);
            }
            
            _weapon = _weaponFactory.CreateWeapon(weaponData, parent);
            
            OnWeaponChanged?.Invoke(_weapon);
        }
        
        [Button]
        public void ExecuteWeapon()
        {
            _weapon.Execute();
            
            OnWeaponExecuted?.Invoke();
        }
        
        protected override void OnInitialize()
        {
            base.OnInitialize();

            ChangeWeapon(data);
        }
    }
}
