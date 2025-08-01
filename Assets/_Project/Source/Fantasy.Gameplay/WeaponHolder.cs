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
        private IWeapon _currentWeapon;

        public void Initialize(IWeaponFactory weaponFactory)
        {
            _weaponFactory = weaponFactory;
            
            base.Initialize();
        }

        [Button]
        public void ExecuteWeapon()
        {
            _currentWeapon.Execute();
            
            OnWeaponExecuted?.Invoke();
        }
        
        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            _currentWeapon = _weaponFactory.CreateWeapon(data, parent);
        }
    }
}
