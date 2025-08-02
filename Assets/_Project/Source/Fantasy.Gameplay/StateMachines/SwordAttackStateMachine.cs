using System;
using Fantasy.Gameplay.Weapons;
using UnityEngine;

namespace Fantasy.Gameplay.StateMachines
{
    public sealed class SwordAttackStateMachine : StateMachineBehaviour
    {
        private IWeapon _cachedWeapon;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _cachedWeapon ??= GetEntityWeapon(animator);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);

            bool mustEnableCollider = stateInfo.normalizedTime is >= 0.15f and < 0.3f;
            
            SetWeaponColliderEnabled(mustEnableCollider);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            
            SetWeaponColliderEnabled(false);
        }
        
        private void SetWeaponColliderEnabled(bool isEnabled)
        {
            if (_cachedWeapon is Sword sword)
            {
                sword.SetColliderEnabled(isEnabled);
            }
        }

        private IWeapon GetEntityWeapon(Animator animator)
        {
            Transform parent = animator.transform.parent;
            
            if (parent.TryGetComponent(out IWeaponHolder weaponHolder))
            {
                return weaponHolder.Weapon;
            }

            throw new InvalidOperationException($"The parent '{parent.name}' does not implement IWeaponHolder!");
        }
    }
}
