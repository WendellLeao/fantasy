using UnityEngine;

namespace Fantasy.Gameplay.Enemies
{
    internal sealed class BasicEnemyView : MonoBehaviour
    {
        [SerializeField]
        private HumanoidAnimatorController humanoidAnimatorController;

        public void Initialize(IWeaponHolder weaponHolder)
        {
            humanoidAnimatorController.Initialize(weaponHolder);
        }
        
        public void Dispose()
        {
            humanoidAnimatorController.Dispose();
        }
    }
}
