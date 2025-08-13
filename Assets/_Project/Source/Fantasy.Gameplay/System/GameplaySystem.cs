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

namespace Fantasy.Gameplay.System
{
    internal sealed class GameplaySystem : Leaosoft.System
    {
        protected override void InitializeManagers()
        {
            IPoolingService poolingService = ServiceLocator.GetService<IPoolingService>();
            IEventService eventService = ServiceLocator.GetService<IEventService>();

            if (TryGetManager(out CursorManager cursorManager))
            {
                cursorManager.Initialize();
            }

            if (TryGetManager(out CameraManager cameraManager))
            {
                cameraManager.Initialize();
            }
            
            if (TryGetManager(out ParticleManager particleManager))
            {
                particleManager.Initialize(poolingService);
            }

            if (TryGetManager(out SpellManager spellManager))
            {
                spellManager.Initialize(poolingService, particleManager);
            }

            if (TryGetManager(out WeaponManager weaponManager))
            {
                weaponManager.Initialize(poolingService, particleManager, spellManager);
            }

            if (TryGetManager(out CharacterManager characterManager))
            {
                characterManager.Initialize(poolingService, eventService, particleManager, weaponManager, cameraManager);
            }

            if (TryGetManager(out EnemyManager enemyManager))
            {
                enemyManager.Initialize(poolingService, eventService, particleManager, weaponManager);
            }
        }
    }
}
