using GlobalState;
using Physics;
using UnityEngine;

namespace WorldGen
{
    public class PlatformsMaterial : MonoBehaviour
    {
        private Rigidbody2D _rb;
    
        [SerializeField] private PhysicsMaterial2D materialFloor;
        [SerializeField] private PhysicsMaterial2D materialWall;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            GameManager.Instance.player.GetComponent<GroundDetector>().onGroundedStateChange.AddListener(OnFloor);
        }

        private void OnFloor(bool isFloor)
        {
            _rb.sharedMaterial = isFloor ? materialFloor : materialWall;
        }
    }
}
