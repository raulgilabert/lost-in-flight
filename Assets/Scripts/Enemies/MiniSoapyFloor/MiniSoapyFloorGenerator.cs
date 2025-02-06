using UnityEngine;

namespace Enemies.MiniSoapyFloor
{
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

        public void Generate(Vector3 position)
        {
            GameObject s = Instantiate(soapy_go, transform.position, Quaternion.identity);

            s.transform.position = position;
        }
    }
}
