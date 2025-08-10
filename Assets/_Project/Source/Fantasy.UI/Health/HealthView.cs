using System;
using DG.Tweening;
using Fantasy.Domain.Health;
using Fantasy.UI.Screens;
using Leaosoft;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.UI.Health
{
    internal sealed class HealthView : Entity, IPooledObject
    {
        public event Action<HealthView> OnHealthDepleted;
        
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
        private IHealth _health;
        private ImageFiller _imageFiller;

        public string PoolId { get; set; }
        
        public void Initialize(Camera mainCamera, IHealth health)
        {
            _mainCamera = mainCamera;
            _health = health;

            base.Initialize();
        }

        protected override void InitializeComponents()
        {
            if (TryGetComponent(out _imageFiller))
            {
                _imageFiller.Initialize(_health.HealthRatio);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            
            _health.OnHealthChanged += HandleHealthChanged;
            _health.OnDepleted += HandleHealthDepleted;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            _health.OnHealthChanged -= HandleHealthChanged;
            _health.OnDepleted -= HandleHealthDepleted;
        }

        protected override void OnBegin()
        {
            base.OnBegin();

            canvasGroup.alpha = 1f;
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
        
        private void HandleHealthDepleted()
        {
            canvasGroup
                .DOFade(endValue: 0f, canvasGroupFadeDuration)
                .SetDelay(canvasGroupFadeDelay)
                .OnComplete(DispatchHealthDepletedEvent);
        }

        private void DispatchHealthDepletedEvent()
        {
            OnHealthDepleted?.Invoke(this);
        }
    }
}
