#if UNITY_EDITOR || DEBUG
using Fantasy.Gameplay;
using Leaosoft.Utilities.Extensions;
using UnityEngine;

namespace Fantasy.Debugging.Weapons
{
    internal sealed class WeaponDebugger : MonoBehaviour
    {
        [SerializeField]
        private WeaponData[] weaponData;
        [SerializeField]
        private WeaponDebugButton debugButtonPrefab;
        [SerializeField]
        private RectTransform layoutGroupTransform;

        private void Start()
        {
            if (!GameObjectExtensions.TryFindObjectOfInterface(out ICharacter character))
            {
                return;
            }
            
            IWeaponHolder weaponHolder = character.gameObject.GetComponent<IWeaponHolder>();
            
            foreach (WeaponData data in weaponData)
            {
                WeaponDebugButton newDebugButton = Instantiate(debugButtonPrefab, layoutGroupTransform);
                
                newDebugButton.Setup(weaponHolder, data);
            }
        }
    }
}
#endif
