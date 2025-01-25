using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapyGenerator : MonoBehaviour
{
    public GameObject soapy_go;
    public float min_x, max_x;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generate()
    {
        GameObject s = Instantiate(soapy_go, new Vector3(Random.Range(min_x, max_x), 5, 0), Quaternion.identity);
    }
}
