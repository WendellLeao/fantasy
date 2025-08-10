using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay.Particles
{
    internal sealed class SimpleParticle : Entity, IParticle
    {
        public event Action<IParticle> OnCompleted;
        
        [SerializeField]
        private ParticleSystem particle;

        private CancellationTokenSource _waitForParticleCts;
        private bool _isPlaying;
        
        public string PoolId { get; set; }

        protected override void InitializeComponents()
        { }

        protected override void OnBegin()
        {
            base.OnBegin();
            
            if (_isPlaying)
            {
                return;
            }

            _isPlaying = true;
            
            particle.Play();

            _waitForParticleCts?.Cancel();
            _waitForParticleCts = new CancellationTokenSource();
            
            WaitForParticleToCompleteAsync(_waitForParticleCts.Token).Forget();
        }

        protected override void OnStop()
        {
            base.OnStop();
            
            if (!_isPlaying)
            {
                return;
            }
            
            _isPlaying = false;
            
            particle.Stop(withChildren: true, ParticleSystemStopBehavior.StopEmittingAndClear);
            
            DisposeCancellationTokenSource();
        }

        private async UniTask WaitForParticleToCompleteAsync(CancellationToken token)
        {
            while (IsParticleAlive())
            {
                await UniTask.Yield(token);
            }
            
            if (token.IsCancellationRequested)
            {
                return;
            }

            OnCompleted?.Invoke(this);
        }
        
        private void DisposeCancellationTokenSource()
        {
            _waitForParticleCts?.Cancel();
            _waitForParticleCts?.Dispose();
            _waitForParticleCts = null;
        }
        
        private bool IsParticleAlive()
        {
            return particle && particle.gameObject && particle.IsAlive();
        }
    }
}
