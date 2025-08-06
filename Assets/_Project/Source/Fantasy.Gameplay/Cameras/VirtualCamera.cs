using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Fantasy.Gameplay.Cameras
{
    internal sealed class VirtualCamera : MonoBehaviour, IVirtualCamera
    {
        [SerializeField]
        private CinemachineCamera cinemachineCamera;
        
        public void SetTarget(Transform targetTransform)
        {
            if (!targetTransform)
            {
                throw new ArgumentNullException(nameof(targetTransform),
                    "Target transform cannot be null. Make sure to pass a valid Transform reference!");
            }
            
            cinemachineCamera.Follow = targetTransform;
        }
    }
}
