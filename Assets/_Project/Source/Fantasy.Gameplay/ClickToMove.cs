using UnityEngine;
using UnityEngine.AI;

namespace Fantasy.Gameplay
{
    internal sealed class ClickToMove : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent navMeshAgent;
        [SerializeField]
        private HumanoidAnimatorController humanoidAnimatorController;
        [SerializeField]
        private float lookRotationSpeed = 3f;
        
        private RaycastHit _cachedHitInfo;

        private void Start()
        {
            // navMeshAgent.updateRotation = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray.origin, ray.direction, out _cachedHitInfo))
                {
                    navMeshAgent.destination = _cachedHitInfo.point;
                }
            }
            
            humanoidAnimatorController.SetVelocity(navMeshAgent.velocity.magnitude);

            // FaceDestination();
        }

        private void FaceDestination()
        {
            Vector3 direction = navMeshAgent.velocity;

            if (direction.sqrMagnitude < 0.01f)
                return;

            direction.y = 0f;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
        }


    }
}
