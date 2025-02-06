using System.Collections;
using UnityEngine;

namespace Enemies.Soapy
{
    public class SoapyGenerator : MonoBehaviour
    {
        public GameObject soapy_go;
        public float min_x, max_x;

        public float cadence;
        public float maxCadenceOffset;

        public float minimumSoapySpeed;
        public float maximumSoapySpeed;

        private void OnEnable()
        {
            StartCoroutine(GenerateLoop());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        private IEnumerator GenerateLoop()
        {
            while (true)
            {
                Generate();
                yield return new WaitForSeconds(Random.Range(cadence - maxCadenceOffset, cadence + maxCadenceOffset));
            }
        }
        
        private void Generate()
        {
            GameObject s = Instantiate(soapy_go, transform.position + new Vector3(Random.Range(min_x, max_x), 0, 0), Quaternion.identity);
            s.GetComponent<Soapy>().velocity = Random.Range(minimumSoapySpeed, maximumSoapySpeed);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position + Vector3.right * min_x, transform.position + Vector3.right * max_x);
        }

    }
}
