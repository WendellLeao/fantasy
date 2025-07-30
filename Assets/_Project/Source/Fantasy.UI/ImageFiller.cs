using DG.Tweening;
using Leaosoft;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Fantasy.UI.Screens
{
    internal sealed class ImageFiller : EntityComponent
    {
        [SerializeField]
        private Image firstLayerImage;
        [SerializeField]
        private float animationDuration;
        
        [SerializeField]
        private bool hasSecondLayerImage;
        [ShowIf(condition: "hasSecondLayerImage")]
        [SerializeField]
        private Image secondLayerImage;
        [ShowIf(condition: "hasSecondLayerImage")]
        [SerializeField]
        private float secondLayerDelay;

        private float _timer;
        private float _cachedAmount;
        private float _defaultAmount;
        private bool _mustEvaluatingTimer;

        public void Initialize(float defaultAmount)
        {
            _defaultAmount = defaultAmount;
            
            base.Initialize();
        }
        
        public void UpdateFillAmount(float amount)
        {
            _cachedAmount = amount;
            
            firstLayerImage.DOFillAmount(endValue: _cachedAmount, animationDuration);

            if (hasSecondLayerImage)
            {
                SetupSecondLayerImageTimer();
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            firstLayerImage.fillAmount = _defaultAmount;

            if (hasSecondLayerImage)
            {
                secondLayerImage.fillAmount = _defaultAmount;
            }
        }

        protected override void OnTick(float deltaTime)
        {
            base.OnTick(deltaTime);
            
            if (!_mustEvaluatingTimer)
            {
                return;
            }

            EvaluateSecondLayerImageTimer();
        }
        
        private void SetupSecondLayerImageTimer()
        {
            if (_cachedAmount < secondLayerImage.fillAmount)
            {
                _timer = secondLayerDelay;
                _mustEvaluatingTimer = true;    
                return;
            }
            
            UpdateSecondLayerImageFillAmount();
        }

        private void EvaluateSecondLayerImageTimer()
        {
            if (_timer > 0f)
            {
                _timer -= Time.deltaTime;
                return;
            }

            UpdateSecondLayerImageFillAmount();
        }

        private void UpdateSecondLayerImageFillAmount()
        {
            secondLayerImage.DOFillAmount(endValue: _cachedAmount, animationDuration);

            ResetSecondLayerImageTimer();
        }
        
        private void ResetSecondLayerImageTimer()
        {
            _timer = 0f;
            _mustEvaluatingTimer = false;
        }
    }
}
