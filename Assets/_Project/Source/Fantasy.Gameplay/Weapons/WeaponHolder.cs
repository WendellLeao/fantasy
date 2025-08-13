using System;
using Leaosoft;
using NaughtyAttributes;
using UnityEngine;

namespace Fantasy.Gameplay.Weapons
{
    internal sealed class WeaponHolder : EntityComponent, IWeaponHolder
    {
        public event Action<IWeapon> OnWeaponChanged;
        public event Action OnWeaponExecuted;
        public event Action OnWeaponExecutionFinished;

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
            if (!IsEnabled)
            {
                return;
            }
            
            DisposeWeapon();
            
            _weapon = _weaponFactory.CreateWeapon(weaponData, parent);
            
            OnWeaponChanged?.Invoke(_weapon);
        }

        [Button]
        public void ExecuteWeapon()
        {
            if (!IsEnabled)
            {
                return;
            }
                  
            _weapon.Execute();
            
            OnWeaponExecuted?.Invoke();
        }

        public void FinishWeaponExecution()
        {
            _weapon.FinishExecution();
            
            OnWeaponExecutionFinished?.Invoke();
        }
        
        protected override void OnBegin()
        {
            base.OnBegin();

            ChangeWeapon(data);
            
            _weapon?.Begin();
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            _weapon?.Stop();
        }
        
        private void DisposeWeapon()
        {
            if (_weapon == null)
            {
                return;
            }
            
            _weaponFactory.DisposeWeapon(_weapon);
        }
    }
}
