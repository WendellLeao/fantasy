using Fantasy.Domain.Health;
using Fantasy.Domain.Weapons;
using UnityEngine;

namespace Fantasy.Gameplay.Enemies
{
    internal sealed class BasicEnemyView : MonoBehaviour
    {
        [SerializeField]
        private HumanoidAnimatorController humanoidAnimatorController;

        public void Initialize(IMoveableAgent moveableAgent, IDamageable damageable, IWeaponHolder weaponHolder)
        {
            humanoidAnimatorController.Initialize(moveableAgent, damageable, weaponHolder);
        }
        
        public void Dispose()
        {
            humanoidAnimatorController.Dispose();
        }
    }
}
