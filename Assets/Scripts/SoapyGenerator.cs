using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoapyGenerator : MonoBehaviour
{
    public GameObject soapy_go;
    public float min_x, max_x;

    public float cadence;
    public float maxCadenceOffset;

    public float minimumSoapySpeed;
    public float maximumSoapySpeed;

    private float _spawnTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer <= 0)
        {
            _spawnTimer = Random.Range(cadence - maxCadenceOffset, cadence + maxCadenceOffset);
            Generate();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + Vector3.right * min_x, transform.position + Vector3.right * max_x);
    }

    private void Generate()
    {
        GameObject s = Instantiate(soapy_go, transform.position + new Vector3(Random.Range(min_x, max_x), 0, 0), Quaternion.identity);
        s.GetComponent<Soapy>().velocity = Random.Range(minimumSoapySpeed, maximumSoapySpeed);
    }
}
