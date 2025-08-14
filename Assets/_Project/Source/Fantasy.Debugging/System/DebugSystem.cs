#if UNITY_EDITOR || DEBUG
using UnityEngine;

namespace Fantasy.Debugging.System
{
    internal sealed class DebugSystem : Leaosoft.System
    {
        [SerializeField]
        private Canvas canvas;
        
        protected override void OnTick(float deltaTime)
        {
            base.OnTick(deltaTime);

            if (Input.GetKeyDown(KeyCode.F1))
            {
                ToggleCanvasActive();
            }
        }

        private void ToggleCanvasActive()
        {
            canvas.gameObject.SetActive(!canvas.gameObject.activeSelf);
        }
    }
}
#endif
