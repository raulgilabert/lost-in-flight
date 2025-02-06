using UnityEngine;

namespace Performance
{
    public class KillZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<KillZoneTarget>() != null)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
