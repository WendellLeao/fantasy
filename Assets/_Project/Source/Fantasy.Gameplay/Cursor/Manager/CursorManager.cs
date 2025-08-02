using UnityEngine;

namespace Fantasy.Gameplay.Cursor.Manager
{
    internal sealed class CursorManager : Leaosoft.Manager
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            SetCursorLockState(CursorLockMode.Locked);
        }
        
        private void SetCursorLockState(CursorLockMode cursorLockMode)
        {
            UnityEngine.Cursor.lockState = cursorLockMode;
        }
    }
}
