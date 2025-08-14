#if UNITY_EDITOR || DEBUG
using Fantasy.Gameplay;
using Fantasy.Gameplay.Cameras.Manager;
using Fantasy.Gameplay.Particles.Manager;
using Fantasy.Gameplay.Weapons.Manager;
using UnityEngine;

namespace Fantasy.Debugging
{
    internal sealed class CharacterInitializer : MonoBehaviour
    {
        private void Start()
        {
            ParticleManager particleManager = FindAnyObjectByType<ParticleManager>();
            WeaponManager weaponManager = FindAnyObjectByType<WeaponManager>();
            CameraManager cameraManager = FindAnyObjectByType<CameraManager>();

            if (TryGetComponent(out ICharacter character))
            {
                character.SetUp(particleManager, weaponManager, cameraManager);
            }
        }
    }
}
#endif
