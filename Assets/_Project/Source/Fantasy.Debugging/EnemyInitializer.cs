#if UNITY_EDITOR || DEBUG
using Fantasy.Gameplay.Enemies;
using Fantasy.Gameplay.Particles.Manager;
using Fantasy.Gameplay.Weapons.Manager;
using UnityEngine;

namespace Fantasy.Debugging
{
    internal sealed class EnemyInitializer : MonoBehaviour
    {
        [SerializeField]
        private BasicEnemy basicEnemy;
        
        private void Start()
        {
            ParticleManager particleManager = FindAnyObjectByType<ParticleManager>();
            WeaponManager weaponManager = FindAnyObjectByType<WeaponManager>();
            
            basicEnemy.Initialize(particleManager, weaponManager);
        }
    }
}
#endif
