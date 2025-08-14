using System;
using DG.Tweening;
using Leaosoft;
using Leaosoft.Pooling;
using UnityEngine;

namespace Fantasy.UI.Health
{
    internal sealed class HealthView : Entity, IEntity, IPooledObject
    {
        public event Action<HealthView> OnHealthDepleted;
        
        [Header("Components")]
        [SerializeField]
        private ImageFiller imageFiller;
        [SerializeField]
        private Billboard billboard;
        
        [Header("Objects")]
        [SerializeField]
        private CanvasGroup canvasGroup;

        [Header("Canvas Group Fade Settings")]
        [SerializeField]
        private float canvasGroupFadeDuration = 0.5f;
        [SerializeField]
        private float canvasGroupFadeDelay = 2f;
        
        private Camera _mainCamera;
        private IHealth _health;

        public string PoolId { get; set; }
        
        public void SetUp(Camera mainCamera, IHealth health)
        {
            _mainCamera = mainCamera;
            _health = health;

            base.SetUp();
        }
        
        protected override void OnSetUp()
        {
            base.OnSetUp();
            
            imageFiller.SetUp(_health.HealthRatio);
            
            RegisterComponents(imageFiller);

            _health.OnHealthChanged += HandleHealthChanged;
            _health.OnDepleted += HandleHealthDepleted;
            
            canvasGroup.alpha = 1f;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            _health.OnHealthChanged -= HandleHealthChanged;
            _health.OnDepleted -= HandleHealthDepleted;
        }

        protected override void OnLateTick(float deltaTime)
        {
            base.OnLateTick(deltaTime);

            Vector3 worldPosition = transform.position + _mainCamera.transform.forward;
            
            billboard.LookAt(worldPosition);
        }
        
        private void HandleHealthChanged(float healthRatio)
        {
            imageFiller.UpdateFillAmount(healthRatio);
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
