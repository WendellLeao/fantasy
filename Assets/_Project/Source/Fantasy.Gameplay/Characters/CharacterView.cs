using Fantasy.Domain.Health;
using Fantasy.Domain.Weapons;
using UnityEngine;

namespace Fantasy.Gameplay.Characters
{
    internal sealed class CharacterView : MonoBehaviour
    {
        [SerializeField]
        private HumanoidAnimatorController humanoidAnimatorController;

        public void Initialize(IDamageable damageable, IWeaponHolder weaponHolder)
        {
            humanoidAnimatorController.Initialize(damageable, weaponHolder);
        }
        
        public void Dispose()
        {
            humanoidAnimatorController.Dispose();
        }
    }
}
