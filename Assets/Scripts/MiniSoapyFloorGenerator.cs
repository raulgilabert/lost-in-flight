using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MiniSoapyFloorGenerator : MonoBehaviour
{
    public GameObject soapy_go;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void generate(Vector3 position)
    {
        GameObject s = Instantiate(soapy_go, transform.position, Quaternion.identity);

        s.transform.position = position;
    }
}
