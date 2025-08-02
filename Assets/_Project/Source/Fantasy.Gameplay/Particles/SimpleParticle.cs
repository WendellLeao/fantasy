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

        private CancellationTokenSource _cancellationTokenSource;
        private bool _isPlaying;
        
        public Transform Transform => transform;

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

            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            
            WaitForParticleToCompleteAsync(_cancellationTokenSource.Token).Forget();
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
            
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
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
        
        private bool IsParticleAlive()
        {
            return particle && particle.gameObject && particle.IsAlive();
        }
    }
}
