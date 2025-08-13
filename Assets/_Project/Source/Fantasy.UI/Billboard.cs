using UnityEngine;

namespace Fantasy.UI
{
    internal sealed class Billboard : MonoBehaviour
    {
        public void LookAt(Vector3 worldPosition)
        {
            transform.LookAt(worldPosition);
        }
    }
}
