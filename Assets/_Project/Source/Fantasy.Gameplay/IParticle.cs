using System;
using UnityEngine;

namespace Fantasy.Gameplay
{
    internal interface IParticle
    {
        public event Action<IParticle> OnCompleted;
        public Transform Transform { get; }
    }
}
