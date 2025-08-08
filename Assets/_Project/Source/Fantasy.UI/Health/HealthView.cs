using System;
using DG.Tweening;
using Fantasy.Domain.Health;
using Fantasy.UI.Screens;
using Leaosoft;
using UnityEngine;

namespace Fantasy.UI.Health
{
    internal sealed class HealthView : Entity
    {
        public event Action<HealthView> OnDamageableDied;
        
        [SerializeField]
        private CanvasGroup canvasGroup;
        [SerializeField]
        private Billboard billboard;

        [Header("Canvas Group Fade Settings")]
        [SerializeField]
        private float canvasGroupFadeDuration = 0.5f;
        [SerializeField]
        private float canvasGroupFadeDelay = 2f;
        
        private Camera _mainCamera;
        private IDamageable _damageable;
        private ImageFiller _imageFiller;

        public void Initialize(Camera mainCamera, IDamageable damageable)
        {
            _mainCamera = mainCamera;
            _damageable = damageable;

            base.Initialize();
        }

        protected override void InitializeComponents()
        {
            if (TryGetComponent(out _imageFiller))
            {
                _imageFiller.Initialize(_damageable.HealthRatio);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            _damageable.OnHealthChanged += HandleHealthChanged;
            _damageable.OnDied += HandleDamageableDied;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            _damageable.OnHealthChanged -= HandleHealthChanged;
            _damageable.OnDied -= HandleDamageableDied;
        }

        protected override void OnLateTick(float deltaTime)
        {
            base.OnLateTick(deltaTime);

            Vector3 worldPosition = transform.position + _mainCamera.transform.forward;
            
            billboard.LookAt(worldPosition);
        }
        
        private void HandleHealthChanged(float healthRatio)
        {
            _imageFiller.UpdateFillAmount(healthRatio);
        }
        
        private void HandleDamageableDied()
        {
            canvasGroup
                .DOFade(endValue: 0f, canvasGroupFadeDuration)
                .SetDelay(canvasGroupFadeDelay)
                .OnComplete(DispatchDamageableDiedEvent);
        }

        private void DispatchDamageableDiedEvent()
        {
            OnDamageableDied?.Invoke(this);
        }
    }
}
