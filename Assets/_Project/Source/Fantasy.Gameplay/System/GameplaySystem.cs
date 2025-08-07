using Fantasy.Gameplay.Cameras.Manager;
using Fantasy.Gameplay.Cursor.Manager;
using Fantasy.Gameplay.Enemies.Manager;
using Fantasy.Gameplay.Particles.Manager;
using Fantasy.Gameplay.Spells.Manager;
using Fantasy.Gameplay.Weapons.Manager;
using Fantasy.Gameplay.Characters.Manager;
using Leaosoft.Events;
using Leaosoft.Services;

namespace Fantasy.Gameplay.System
{
    internal sealed class GameplaySystem : Leaosoft.System
    {
        protected override void InitializeManagers()
        {
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
                particleManager.Initialize();
            }

            if (TryGetManager(out SpellManager spellManager))
            {
                spellManager.Initialize(particleManager);
            }

            if (TryGetManager(out WeaponManager weaponManager))
            {
                weaponManager.Initialize(particleManager, spellManager);
            }

            if (TryGetManager(out CharacterManager characterManager))
            {
                characterManager.Initialize(particleManager, weaponManager, eventService, cameraManager);
            }

            if (TryGetManager(out EnemyManager enemyManager))
            {
                enemyManager.Initialize(particleManager, weaponManager, eventService);
            }
        }
    }
}
