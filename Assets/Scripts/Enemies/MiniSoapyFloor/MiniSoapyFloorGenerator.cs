using UnityEngine;
using UnityEngine.Tilemaps;

namespace Enemies.MiniSoapyFloor
{
    public class MiniSoapyFloorGenerator : MonoBehaviour
    {
        [SerializeField] private AnimatedTile soapyFloorTile; 
        private Tilemap _tilemap;
        
        // Start is called before the first frame update
        void Start()
        {
            _tilemap = GameObject.Find("SoapyFloor").GetComponentInChildren<Tilemap>();
        }

        public void Generate(Vector3Int position, Matrix4x4 transformMatrix)
        {
            _tilemap.SetTile(position, soapyFloorTile);
            _tilemap.SetTransformMatrix(position, transformMatrix);
        }
    }
}
