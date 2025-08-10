#if UNITY_EDITOR || DEBUG
using Fantasy.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fantasy.Debugging.Weapons
{
    internal sealed class WeaponDebugButton : MonoBehaviour
    {
        [SerializeField]
        private Button button;
        [SerializeField]
        private TextMeshProUGUI weaponNameText;

        private IWeaponHolder _weaponHolder;
        private WeaponData _weaponData;

        public void Setup(IWeaponHolder weaponHolder, WeaponData weaponData)
        {
            _weaponHolder = weaponHolder;
            _weaponData = weaponData;
            
            weaponNameText.text = _weaponData.ViewName;
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
            _weaponHolder.ChangeWeapon(_weaponData);
        }
    }
}
#endif
