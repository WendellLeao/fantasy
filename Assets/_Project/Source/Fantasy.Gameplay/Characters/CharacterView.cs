using UnityEngine;

namespace Fantasy.Gameplay.Characters
{
    internal sealed class CharacterView : MonoBehaviour
    {
        [SerializeField]
        private CharacterAnimatorController characterAnimatorController;

        public void Initialize(IWeaponHolder weaponHolder)
        {
            characterAnimatorController.Initialize(weaponHolder);
        }
        
        public void Dispose()
        {
            characterAnimatorController.Dispose();
        }
    }
}
