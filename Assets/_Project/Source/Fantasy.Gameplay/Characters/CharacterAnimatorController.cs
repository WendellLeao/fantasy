using UnityEngine;

namespace Fantasy.Gameplay.Characters
{
    internal sealed class CharacterAnimatorController : MonoBehaviour
    {
        private static readonly int ExecuteWeapon = Animator.StringToHash("ExecuteWeapon");

        [SerializeField]
        private Animator animator;

        private IWeaponHolder _weaponHolder;
        
        public void Initialize(IWeaponHolder weaponHolder)
        {
            _weaponHolder = weaponHolder;

            _weaponHolder.OnWeaponExecuted += HandleWeaponExecuted;
        }

        public void Dispose()
        {
            _weaponHolder.OnWeaponExecuted -= HandleWeaponExecuted;
        }

        private void HandleWeaponExecuted()
        {
            animator.SetTrigger(ExecuteWeapon);
        }
    }
}
