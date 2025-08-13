using System;
using NaughtyAttributes;
using UnityEngine;

namespace Fantasy.Gameplay.Animations.StateMachines
{
    internal sealed class MeleeWeaponStateMachine : StateMachineBehaviour
    {
        [InfoBox("Normalized time range (0 to 1) during which the collider will be enabled.")]
        [MinMaxSlider(0f, 1f)]
        [SerializeField]
        private Vector2 colliderEnableRange = new(0.15f, 0.3f);
        
        private IWeaponHolder _cachedWeaponHolder;
        private IMeleeWeapon _cachedMeleeWeapon;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            
            _cachedMeleeWeapon ??= GetEntityMeleeWeapon(animator);
            
            _cachedMeleeWeapon.SetColliderEnabled(false);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);

            float normalizedTime = stateInfo.normalizedTime;
            
            bool mustEnableCollider = normalizedTime >= colliderEnableRange.x && normalizedTime < colliderEnableRange.y;
            
            _cachedMeleeWeapon.SetColliderEnabled(mustEnableCollider);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            
            _cachedWeaponHolder.FinishWeaponExecution();
        }
        
        private IMeleeWeapon GetEntityMeleeWeapon(Animator animator)
        {
            Transform parent = animator.transform.parent;
            
            if (parent.TryGetComponent(out _cachedWeaponHolder))
            {
                if (_cachedWeaponHolder.Weapon is not IMeleeWeapon meleeWeapon)
                {
                    throw new InvalidOperationException($"The weapon of type '{_cachedWeaponHolder.Weapon.GetType().Name}' doesn't implement '{nameof(IMeleeWeapon)}'");
                }
                
                return meleeWeapon;
            }

            throw new InvalidOperationException($"The parent '{parent.name}' doesn't implement the {nameof(IWeaponHolder)} component!");
        }
    }
}
