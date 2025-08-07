using UnityEngine;
using System;
using Fantasy.Domain.Health;
using Fantasy.Domain.Weapons;
using Leaosoft;
using Random = UnityEngine.Random;

namespace Fantasy.Gameplay
{
    internal sealed class HumanoidAnimatorController : EntityComponent
    {
        private static readonly int Velocity = Animator.StringToHash("Velocity");
        private static readonly int MovesetType = Animator.StringToHash("MovesetType");
        private static readonly int ExecuteWeapon = Animator.StringToHash("ExecuteWeapon");
        private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");
        private static readonly int DeathType = Animator.StringToHash("DeathType");
        private static readonly int Die = Animator.StringToHash("Die");

        [SerializeField]
        private Animator animator;
        [SerializeField]
        private float velocityDampTime = 0.08f;

        private IMoveableAgent _moveableAgent;
        private IDamageable _damageable;
        private IWeaponHolder _weaponHolder;
        private float _smoothedSpeed;

        public void Initialize(IMoveableAgent moveableAgent, IDamageable damageable, IWeaponHolder weaponHolder)
        {
            _moveableAgent = moveableAgent;
            _damageable = damageable;
            _weaponHolder = weaponHolder;

            base.Initialize();
        }

        protected override void OnBegin()
        {
            base.OnBegin();
            
            HandleWeaponMovesetType(_weaponHolder.Weapon);
            
            SubscribeEvents();
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            UnsubscribeEvents();
        }

        protected override void OnTick(float deltaTime)
        {
            base.OnTick(deltaTime);
            
            SetVelocity(_moveableAgent.Velocity.magnitude, deltaTime);
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

        private void SetVelocity(float velocityMagnitude, float deltaTime)
        {
            animator.SetFloat(id: Velocity, velocityMagnitude, velocityDampTime, deltaTime);
        }
    }
}
