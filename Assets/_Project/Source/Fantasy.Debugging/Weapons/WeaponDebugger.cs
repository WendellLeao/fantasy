using UnityEngine;

namespace Fantasy.Debugging.Weapons
{
    internal sealed class WeaponDebugger : MonoBehaviour
    {
        [SerializeField]
        private WeaponData[] weapons;
        [SerializeField]
        private WeaponDebugButton debugButtonPrefab;

        private void Start()
        {
            foreach (WeaponData weaponData in weapons)
            {
                WeaponDebugButton newDebugButton = Instantiate(debugButtonPrefab, transform);
                
                newDebugButton.Setup(weaponData);
            }
        }
    }
}
