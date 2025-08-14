using Fantasy.Gameplay.Cameras.Manager;
using Fantasy.Gameplay.Cursor.Manager;
using Fantasy.Gameplay.Enemies.Manager;
using Fantasy.Gameplay.Particles.Manager;
using Fantasy.Gameplay.Spells.Manager;
using Fantasy.Gameplay.Weapons.Manager;
using Fantasy.Gameplay.Characters.Manager;
using Leaosoft.Events;
using Leaosoft.Pooling;
using Leaosoft.Services;
using UnityEngine;

namespace Fantasy.Gameplay.System
{
    internal sealed class GameplaySystem : Leaosoft.System
    {
        [SerializeField]
        private CursorManager cursorManager;
        [SerializeField]
        private CameraManager cameraManager;
        [SerializeField]
        private ParticleManager particleManager;
        [SerializeField]
        private SpellManager spellManager;
        [SerializeField]
        private WeaponManager weaponManager;
        [SerializeField]
        private CharacterManager characterManager;
        [SerializeField]
        private EnemyManager enemyManager;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            IPoolingService poolingService = ServiceLocator.GetService<IPoolingService>();
            IEventService eventService = ServiceLocator.GetService<IEventService>();

            cursorManager.SetUp();
            cameraManager.SetUp();
            particleManager.SetUp(poolingService);
            spellManager.SetUp(poolingService, particleManager);
            weaponManager.SetUp(poolingService, particleManager, spellManager);
            characterManager.SetUp(poolingService, eventService, particleManager, weaponManager, cameraManager);
            enemyManager.SetUp(poolingService, eventService, particleManager, weaponManager);
            
            RegisterManagers(cursorManager, cameraManager, particleManager, spellManager, weaponManager, characterManager,enemyManager);
        }
    }
}
