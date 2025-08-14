using Leaosoft;
using UnityEngine;

namespace Fantasy.Gameplay.Spells
{
    internal sealed class ApplyForwardForce : EntityComponent, IEntityComponent
    {
        [SerializeField]
        private Rigidbody rigidBody;
        [SerializeField]
        private float force = 10f;

        protected override void OnSetUp()
        {
            base.OnSetUp();
            
            rigidBody.AddRelativeForce(transform.forward * force, ForceMode.Impulse);
        }
    }
}
