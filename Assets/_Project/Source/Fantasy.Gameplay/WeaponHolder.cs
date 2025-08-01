using System;
using Leaosoft;
using NaughtyAttributes;
using UnityEngine;

namespace Fantasy.Gameplay
{
    internal sealed class WeaponHolder : EntityComponent, IWeaponHolder
    {
        public event Action OnWeaponExecuted;
        
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

        [Button]
        public void ExecuteWeapon()
        {
            _weapon.Execute();
            
            OnWeaponExecuted?.Invoke();
        }
        
        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            _weapon = _weaponFactory.CreateWeapon(data, parent);
        }
    }
}
