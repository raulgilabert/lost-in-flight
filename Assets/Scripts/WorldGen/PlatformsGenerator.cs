using System.Collections.Generic;
using Enemies.MiniSoapyFloor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WorldGen
{
    public class PlatformsGenerator : MonoBehaviour
    {
        [SerializeField] private TileBase[] textures = new TileBase[3];
        [SerializeField] private MiniSoapyFloorGenerator miniSoapyFloorGenerator;

        private Tilemap _tilemapPlatforms;

        private List<TileChangeData> _updateQueue;
    
        // Start is called before the first frame update
        void Start()
        {
            _tilemapPlatforms = GameObject.Find("Platforms").GetComponentInChildren<Tilemap>();
            _updateQueue = new List<TileChangeData>();
        }

        public void QueueGenerate(TileKind tileKind, int y, int limitTilesLeft, int limitTilesRight)
        {
            int numPlatforms = Random.Range(3, 6);
            int mapWidth = limitTilesRight - limitTilesLeft;
            int maxSize = mapWidth / (numPlatforms - 1);

            limitTilesLeft += Random.Range(-3, 3);

            // Ensure enough capacity while keeping amortized constant time
            _updateQueue.Capacity = RoundUpToPowerOfTwo(_updateQueue.Count + numPlatforms * maxSize);

            for (int i = 0; i < numPlatforms; i++)
            {
                int size = Random.Range(4, maxSize);

                TileBase tile = tileKind switch
                {
                    TileKind.Sandstone => textures[0],
                    TileKind.Tile => textures[1],
                    TileKind.Stone => textures[2],
                    _ => textures[0]
                };

                Vector3 matrixPosition = new Vector3(Random.Range(-0.32f, 0.32f), Random.Range(-0.48f, 0.48f));
                
                Matrix4x4 matrix = Matrix4x4.TRS(matrixPosition, Quaternion.Euler(0, 0, 0), Vector3.one);
                
                for (int j = 0; j < size; j++)
                {
                    _updateQueue.Add(new TileChangeData
                    {
                        color = new Color(1f,1f, 1f),
                        position = new Vector3Int(j + limitTilesLeft, y, 0),
                        tile = tile,
                        transform = matrix
                    });
                }
                
                if (Random.Range(0, 10) < 2)
                {
                    Matrix4x4 soapyMatrix = Matrix4x4.TRS(matrixPosition + new Vector3(0, 5f/32f, 0), 
                        Quaternion.Euler(0, 0, 0), Vector3.one);
                    miniSoapyFloorGenerator.QueueGenerate(new Vector3Int(limitTilesLeft + Random.Range(1, size-1), y, 0), 
                        soapyMatrix);
                }

                limitTilesLeft += size + Random.Range(2, 4);

            }
        }
        
        public void FlushQueues()
        {
            _tilemapPlatforms.SetTiles(_updateQueue.ToArray(), true);
            _updateQueue.Clear();
            
            miniSoapyFloorGenerator.FlushQueues();
        }
        

        private static int RoundUpToPowerOfTwo(int value)
        {
            // Source: https://graphics.stanford.edu/%7Eseander/bithacks.html#RoundUpPowerOf2
            
            value--;
            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            value |= value >> 16;
            value++;

            return value;
        }
    }
}