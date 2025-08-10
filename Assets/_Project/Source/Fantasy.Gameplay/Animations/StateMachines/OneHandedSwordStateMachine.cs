using System;
using NaughtyAttributes;
using UnityEngine;

namespace Fantasy.Gameplay.Animations.StateMachines
{
    internal sealed class OneHandedSwordStateMachine : StateMachineBehaviour
    {
        [InfoBox("Normalized time range (0 to 1) during which the collider will be enabled.")]
        [MinMaxSlider(0f, 1f)]
        [SerializeField]
        private Vector2 colliderEnableRange = new(0.15f, 0.3f);
        
        private IWeapon _cachedWeapon;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            
            _cachedWeapon ??= GetEntityWeapon(animator);
            
            _cachedWeapon.SetColliderEnabled(false);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);

            float normalizedTime = stateInfo.normalizedTime;
            
            bool mustEnableCollider = normalizedTime >= colliderEnableRange.x && normalizedTime < colliderEnableRange.y;
            
            _cachedWeapon.SetColliderEnabled(mustEnableCollider);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            
            _cachedWeapon.SetColliderEnabled(false);
        }
        
        private IWeapon GetEntityWeapon(Animator animator)
        {
            Transform parent = animator.transform.parent;
            
            if (parent.TryGetComponent(out IWeaponHolder weaponHolder))
            {
                return weaponHolder.Weapon;
            }

            throw new InvalidOperationException($"The parent '{parent.name}' doesn't implement the {nameof(IWeaponHolder)} component!");
        }
    }
}
