using Fantasy.SharedKernel.Health;
using Fantasy.UI.Screens;
using Leaosoft;
using UnityEngine;

namespace Fantasy.UI.Health
{
    internal sealed class HealthView : Entity
    {
        [SerializeField]
        private Billboard billboard;

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
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            
            _damageable.OnHealthChanged -= HandleHealthChanged;
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
    }
}
