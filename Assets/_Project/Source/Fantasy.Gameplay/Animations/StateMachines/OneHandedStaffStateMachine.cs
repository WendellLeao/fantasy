using System;
using Fantasy.SharedKernel.Weapons;
using NaughtyAttributes;
using UnityEngine;

namespace Fantasy.Gameplay.Animations.StateMachines
{
    internal sealed class OneHandedStaffStateMachine : StateMachineBehaviour
    {
        [InfoBox("Normalized time range (0 to 1) during which the spell will be casted.")]
        [Range(0f, 1f)]
        [SerializeField]
        private float castSpellTime;

        private ISpellCaster _cachedSpellCaster;
        private bool _hasCastedSpell;
        
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);

            _cachedSpellCaster ??= GetEntitySpellCaster(animator);

            _hasCastedSpell = false;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateUpdate(animator, stateInfo, layerIndex);

            if (_hasCastedSpell)
            {
                return;
            }

            if (stateInfo.normalizedTime >= castSpellTime)
            {
                _cachedSpellCaster.CastSpell();

                _hasCastedSpell = true;
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);

            _hasCastedSpell = false;
        }

        private ISpellCaster GetEntitySpellCaster(Animator animator)
        {
            Transform parent = animator.transform.parent;
            
            if (parent.TryGetComponent(out IWeaponHolder weaponHolder))
            {
                if (weaponHolder.Weapon is not ISpellCaster spellCaster)
                {
                    throw new InvalidOperationException($"The weapon '{weaponHolder.Weapon.Data.ViewName}' doesn't implement the {nameof(ISpellCaster)}!");
                }
                
                return spellCaster;
            }

            throw new InvalidOperationException($"The parent '{parent.name}' doesn't implement the {nameof(IWeaponHolder)} component!");
        }
    }
}
