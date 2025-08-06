using UnityEngine;

namespace Fantasy.Gameplay
{
    public interface IMoveableAgent
    {
        public Vector3 Velocity { get; }
        
        public void SetDestination(Vector3 position);
    }
}
