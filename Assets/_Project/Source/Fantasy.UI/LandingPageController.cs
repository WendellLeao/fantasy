using Leaosoft.Services;
using Leaosoft.UI;
using Leaosoft.UI.Screens;
using UnityEngine;

namespace Fantasy.UI
{
    // TODO: this is temporary. The title screen must be loaded after the startup scene finishes its processes.
    internal sealed class LandingPageController : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField]
        private UIScreenData titleScreenData;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.None;
            
            IScreenService screenService = ServiceLocator.GetService<IScreenService>();

            screenService.OpenScreenAsync(titleScreenData);
        }
    }
}
