using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soapy : MonoBehaviour
{
    public Rigidbody2D rb;
    public float velocity;

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(0, -velocity, 0);
    }

    public void OnDealtDamage(float damage)
    {
        Destroy(gameObject);
    }
}
