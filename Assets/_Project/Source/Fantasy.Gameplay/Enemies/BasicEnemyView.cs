using Fantasy.Domain.Health;
using Fantasy.Domain.Weapons;
using UnityEngine;

namespace Fantasy.Gameplay.Enemies
{
    internal sealed class BasicEnemyView : MonoBehaviour
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
