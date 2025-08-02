using UnityEngine;

namespace Fantasy.Gameplay.Debug.Weapons
{
    internal sealed class WeaponButtonManagerDebug : MonoBehaviour
    {
        [SerializeField]
        private WeaponData[] weapons;
        [SerializeField]
        private WeaponButtonDebug buttonPrefab;

        private void Start()
        {
            foreach (WeaponData weaponData in weapons)
            {
                WeaponButtonDebug newButtonDebug = Instantiate(buttonPrefab, transform);
                
                newButtonDebug.Setup(weaponData);
            }
        }
    }
}
