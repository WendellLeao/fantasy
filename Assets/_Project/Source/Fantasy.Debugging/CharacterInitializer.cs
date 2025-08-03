using Fantasy.Gameplay.Characters;
using Fantasy.Gameplay.Particles.Manager;
using Fantasy.Gameplay.Weapons.Manager;
using UnityEngine;

namespace Fantasy.Debugging
{
    internal sealed class CharacterInitializer : MonoBehaviour
    {
        [SerializeField]
        private Character character;
        
        private void Start()
        {
            ParticleManager particleManager = FindAnyObjectByType<ParticleManager>();
            WeaponManager weaponManager = FindAnyObjectByType<WeaponManager>();
            
            character.Initialize(particleManager, weaponManager);
        }
    }
}
