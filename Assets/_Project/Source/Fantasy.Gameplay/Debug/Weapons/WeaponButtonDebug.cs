using Fantasy.Gameplay.Characters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantasy.Gameplay.Debug.Weapons
{
    internal sealed class WeaponButtonDebug : MonoBehaviour
    {
        [SerializeField]
        private Button button;
        [SerializeField]
        private TextMeshProUGUI idText;

        private WeaponData _weaponData;

        public void Setup(WeaponData weaponData)
        {
            _weaponData = weaponData;
            
            idText.text = _weaponData.Id;
        }
        
        private void OnEnable()
        {
            button.onClick.AddListener(HandleButtonClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(HandleButtonClick);
        }

        private void HandleButtonClick()
        {
            Character character = FindFirstObjectByType<Character>();
            
            character.GetComponent<IWeaponHolder>().ChangeWeapon(_weaponData);
        }
    }
}
