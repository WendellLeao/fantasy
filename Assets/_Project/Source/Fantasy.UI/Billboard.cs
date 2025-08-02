using UnityEngine;

namespace Fantasy.UI.Screens
{
    internal sealed class Billboard : MonoBehaviour
    {
        public void LookAt(Vector3 worldPosition)
        {
            transform.LookAt(worldPosition);
        }
    }
}
