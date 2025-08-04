using UnityEngine;
using System;
using Fantasy.SharedKernel.Health;
using Fantasy.SharedKernel.Weapons;
using Random = UnityEngine.Random;

namespace Fantasy.Gameplay
{
    internal sealed class HumanoidAnimatorController : MonoBehaviour
    {
        private static readonly int MovesetType = Animator.StringToHash("MovesetType");
        private static readonly int ExecuteWeapon = Animator.StringToHash("ExecuteWeapon");
        private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");
        private static readonly int DeathType = Animator.StringToHash("DeathType");
        private static readonly int Die = Animator.StringToHash("Die");

        [SerializeField]
        private Animator animator;

        private IDamageable _damageable;
        private IWeaponHolder _weaponHolder;

        public void Initialize(IDamageable damageable, IWeaponHolder weaponHolder)
        {
            _damageable = damageable;
            _weaponHolder = weaponHolder;

            HandleWeaponMovesetType(_weaponHolder.Weapon);
            
            SubscribeEvents();
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _damageable.OnDamageTaken += HandleDamageTaken;
            _damageable.OnDied += HandleDamageableDied;
            
            _weaponHolder.OnWeaponChanged += HandleWeaponMovesetType;
            _weaponHolder.OnWeaponExecuted += HandleWeaponExecuted;
        }

        private void UnsubscribeEvents()
        {
            _damageable.OnDamageTaken -= HandleDamageTaken;
            _damageable.OnDied -= HandleDamageableDied;
            
            _weaponHolder.OnWeaponChanged -= HandleWeaponMovesetType;
            _weaponHolder.OnWeaponExecuted -= HandleWeaponExecuted;
        }

        private void HandleDamageTaken(DamageData damageData)
        {
            animator.SetTrigger(id: TakeDamage);
        }
        
        private void HandleDamageableDied()
        {
            int randomDeathType = Random.Range(0, Enum.GetValues(typeof(DeathType)).Length);
            
            animator.SetInteger(id: DeathType, randomDeathType);
            animator.SetTrigger(id: Die);
        }
        
        private void HandleWeaponMovesetType(IWeapon weapon)
        {
            WeaponData weaponData = weapon.Data;
            
            animator.SetInteger(id: MovesetType, (int)weaponData.MovesetType);
        }
        
        private void HandleWeaponExecuted()
        {
            animator.SetTrigger(id: ExecuteWeapon);
        }
    }
}
