using System.Collections;
using UnityEngine;

namespace Enemies.MiniSoapyFly
{
    public class MiniSoapyFlyGenerator : MonoBehaviour
    {
        public GameObject minisoapyfly_go;
        public float min_y, max_y;

        public float cadence;
        public float maxCadenceOffset;

        public float minimumMiniSoapySpeed;
        public float maximumMiniSoapySpeed;
        public bool flyRight;

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
            GameObject s = Instantiate(minisoapyfly_go, transform.position + new Vector3(0, Random.Range(min_y, max_y), 0), Quaternion.identity);
            MiniSoapyFly miniSoapyFly = s.GetComponent<MiniSoapyFly>();
            miniSoapyFly.speed = Random.Range(minimumMiniSoapySpeed, maximumMiniSoapySpeed);
            miniSoapyFly.flyRight = flyRight;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position + Vector3.up * min_y, transform.position + Vector3.up * max_y);
        }
    }
}
