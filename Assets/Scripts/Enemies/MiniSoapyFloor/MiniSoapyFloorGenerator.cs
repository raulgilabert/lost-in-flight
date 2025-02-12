using UnityEngine;
using UnityEngine.Tilemaps;

namespace Enemies.MiniSoapyFloor
{
    public class MiniSoapyFloorGenerator : MonoBehaviour
    {
        [SerializeField] private AnimatedTile soapyFloorTile;
        [SerializeField] private Tilemap tilemap;

        public void Generate(Vector3Int position, Matrix4x4 transformMatrix)
        {
            tilemap.SetTile(position, soapyFloorTile);
            tilemap.SetTransformMatrix(position, transformMatrix);
        }
    }
}
