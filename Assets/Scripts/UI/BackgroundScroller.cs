using UnityEngine;

namespace UI
{
    public class BackgroundScroller : MonoBehaviour
    {
        [SerializeField] private float scrollSpeed;

        // Update is called once per frame
        void Update()
        {
            transform.position += Vector3.up * (scrollSpeed * Time.deltaTime);

            if (transform.position.y > 1f)
            {
                transform.position += Vector3.down;
            }
        }
    }
}
