using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay
{
    public interface IVirtualCamera : IEntity
    {
        public void SetTarget(Transform targetTransform);
    }
}
