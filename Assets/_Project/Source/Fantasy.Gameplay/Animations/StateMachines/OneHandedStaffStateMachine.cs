using System;
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
        private IWeaponHolder _cachedWeaponHolder;
        private IWeapon _cachedWeapon;
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

            _cachedWeaponHolder.FinishWeaponExecution();
            
            _hasCastedSpell = false;
        }

        private ISpellCaster GetEntitySpellCaster(Animator animator)
        {
            Transform parent = animator.transform.parent;
            
            if (parent.TryGetComponent(out _cachedWeaponHolder))
            {
                _cachedWeapon = _cachedWeaponHolder.Weapon;
                
                if (_cachedWeapon is not ISpellCaster spellCaster)
                {
                    throw new InvalidOperationException($"The weapon '{_cachedWeapon.Data.ViewName}' doesn't implement the {nameof(ISpellCaster)}!");
                }
                
                return spellCaster;
            }

            throw new InvalidOperationException($"The parent '{parent.name}' doesn't implement the {nameof(IWeaponHolder)} component!");
        }
    }
}
