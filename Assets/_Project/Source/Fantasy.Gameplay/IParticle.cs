using System;
using UnityEngine;

namespace Fantasy.Gameplay
{
    public interface IParticle
    {
        public event Action<IParticle> OnCompleted;
        public Transform Transform { get; }
    }
}
