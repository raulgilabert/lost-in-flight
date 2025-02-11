using UnityEngine;
using Random = UnityEngine.Random;

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

        private float _spawnTimer;

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
            Gizmos.DrawLine(transform.position + Vector3.up * min_y, transform.position + Vector3.up * max_y);
        }

        private void Generate()
        {
            GameObject s = Instantiate(minisoapyfly_go, transform.position + new Vector3(0, Random.Range(min_y, max_y), 0), Quaternion.identity);
            MiniSoapyFly miniSoapyFly = s.GetComponent<MiniSoapyFly>();
            miniSoapyFly.speed = Random.Range(minimumMiniSoapySpeed, maximumMiniSoapySpeed);
            miniSoapyFly.flyRight = flyRight;
        }
    }
}
