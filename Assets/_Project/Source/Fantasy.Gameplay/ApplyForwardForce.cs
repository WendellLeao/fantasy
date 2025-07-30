using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay
{
    internal sealed class ApplyForwardForce : EntityComponent
    {
        [SerializeField]
        private Rigidbody rigidBody;
        [SerializeField]
        private float force = 10f;

        protected override void OnBegin()
        {
            base.OnBegin();
            
            rigidBody.AddRelativeForce(transform.forward * force, ForceMode.Impulse);
        }
    }
}
