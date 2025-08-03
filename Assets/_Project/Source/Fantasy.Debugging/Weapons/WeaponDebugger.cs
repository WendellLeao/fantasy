using Fantasy.Gameplay.Characters;
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
            Character character = FindFirstObjectByType<Character>();

            IWeaponHolder weaponHolder = character.GetComponent<IWeaponHolder>();
            
            foreach (WeaponData data in weaponData)
            {
                WeaponDebugButton newDebugButton = Instantiate(debugButtonPrefab, layoutGroupTransform);
                
                newDebugButton.Setup(weaponHolder, data);
            }
        }
    }
}
