using UnityEngine;

namespace Fantasy.Gameplay
{
    internal sealed class HumanoidAnimatorController : MonoBehaviour
    {
        private static readonly int MovesetType = Animator.StringToHash("MovesetType");
        private static readonly int ExecuteWeapon = Animator.StringToHash("ExecuteWeapon");
        private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");

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
            
            _weaponHolder.OnWeaponChanged += HandleWeaponMovesetType;
            _weaponHolder.Weapon.OnExecuted += HandleWeaponExecuted;
        }

        private void UnsubscribeEvents()
        {
            _damageable.OnDamageTaken -= HandleDamageTaken;
            
            _weaponHolder.OnWeaponChanged -= HandleWeaponMovesetType;
            _weaponHolder.Weapon.OnExecuted -= HandleWeaponExecuted;
        }

        private void HandleDamageTaken(DamageData damageData)
        {
            animator.SetTrigger(id: TakeDamage);
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
