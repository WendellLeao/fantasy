using Leaosoft.Services;
using Leaosoft.UI;
using Leaosoft.UI.Screens;
using UnityEngine;
using UnityEngine.UI;

namespace Fantasy.UI.Screens
{
    internal sealed class TitleScreen : UIScreen
    {
        [Header("Objects")]
        [SerializeField]
        private Button playButton;
        [SerializeField]
        private Button quitButton;

        [Header("Data")]
        [SerializeField]
        private UIScreenData playConfirmationScreenData;

        protected override void OnSubscribeEvents()
        {
            base.OnSubscribeEvents();
            
            playButton.onClick.AddListener(HandlePlayButtonClick);
        }

        protected override void OnUnsubscribeEvents()
        {
            base.OnUnsubscribeEvents();
            
            playButton.onClick.RemoveListener(HandlePlayButtonClick);
        }

        private void HandlePlayButtonClick()
        {
            IScreenService screenService = ServiceLocator.GetService<IScreenService>();
            
            screenService.OpenScreenAsync(playConfirmationScreenData);
        }
    }
}
