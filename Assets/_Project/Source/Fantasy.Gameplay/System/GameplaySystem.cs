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
            
            TryGetManager(out CursorManager cursorManager);
            TryGetManager(out ParticleManager particleManager);
            TryGetManager(out SpellManager spellManager);
            TryGetManager(out WeaponManager weaponManager);
            TryGetManager(out CharacterManager characterManager);
            TryGetManager(out EnemyManager enemyManager);
                
            cursorManager.Initialize();
            particleManager.Initialize();
            spellManager.Initialize(particleManager);
            weaponManager.Initialize(particleManager, spellManager);
            characterManager.Initialize(particleManager, weaponManager, eventService);
            enemyManager.Initialize(particleManager, weaponManager, eventService);
        }
    }
}
