using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
