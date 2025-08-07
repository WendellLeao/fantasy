using System;
using Fantasy.Domain.Health;
using NaughtyAttributes;
using UnityEngine;

namespace Fantasy.Gameplay.Animations.StateMachines
{
    internal sealed class TakeDamageStateMachine : StateMachineBehaviour
    {
        [InfoBox("Normalized time range (0 to 1) during which the damageable will be invincible.")]
        [MinMaxSlider(0f, 1f)]
        [SerializeField]
        private Vector2 invincibilityDuration = new(0f, 0.3f);
        
        private IDamageable _cachedDamageable;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            _cachedDamageable ??= GetEntityDamageable(animator);
            
            _cachedDamageable.SetIsInvincible(false);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);
            
            float normalizedTime = stateInfo.normalizedTime;
            
            bool isInvincible = normalizedTime >= invincibilityDuration.x && normalizedTime < invincibilityDuration.y;
            
            _cachedDamageable.SetIsInvincible(isInvincible);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            
            _cachedDamageable.SetIsInvincible(false);
        }

        private IDamageable GetEntityDamageable(Animator animator)
        {
            Transform parent = animator.transform.parent;
            
            if (parent.TryGetComponent(out IDamageable damageable))
            {
                return damageable;
            }

            throw new InvalidOperationException($"The parent '{parent.name}' doesn't implement the {nameof(IDamageable)} component!");
        }
    }
}
