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

            cursorManager.Initialize();
            cameraManager.Initialize();
            particleManager.Initialize(poolingService);
            spellManager.Initialize(poolingService, particleManager);
            weaponManager.Initialize(poolingService, particleManager, spellManager);
            characterManager.Initialize(poolingService, eventService, particleManager, weaponManager, cameraManager);
            enemyManager.Initialize(poolingService, eventService, particleManager, weaponManager);
            
            RegisterManagers(cursorManager, cameraManager, particleManager, spellManager, weaponManager, characterManager,enemyManager);
        }
    }
}
