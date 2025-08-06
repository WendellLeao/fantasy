using UnityEngine;

namespace Fantasy.Gameplay
{
    internal interface IMoveableAgent
    {
        public Vector3 Velocity { get; }
        
        public void SetDestination(Vector3 position);
    }
}
