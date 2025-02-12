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
    
        // Start is called before the first frame update
        void Start()
        {
            _tilemapPlatforms = GameObject.Find("Platforms").GetComponentInChildren<Tilemap>();
        }

        public void Generate(TileKind tileKind, int y, int limitTilesLeft, int limitTilesRight)
        {
            int numPlatforms = Random.Range(3, 6);
            int mapWidth = limitTilesRight - limitTilesLeft;

            limitTilesLeft += Random.Range(-3, 3);

            for (int i = 0; i < numPlatforms; i++)
            {
                int size = Random.Range(4, mapWidth / (numPlatforms - 1));

                TileBase tile = tileKind switch
                {
                    TileKind.Sandstone => textures[0],
                    TileKind.Tile => textures[1],
                    TileKind.Stone => textures[2],
                    _ => textures[0]
                };

                Vector3 matrixPosition = new Vector3(Random.Range(-0.32f, 0.32f), Random.Range(-0.48f, 0.48f));
                
                Matrix4x4 matrix = Matrix4x4.TRS(matrixPosition, Quaternion.Euler(0, 0, 0), Vector3.one);

                TileChangeData[] platformTiles = new TileChangeData[size];
                
                for (int j = 0; j < size; j++)
                {
                    platformTiles[j] = new TileChangeData
                    {
                        color = new Color(1f,1f, 1f),
                        position = new Vector3Int(j + limitTilesLeft, y, 0),
                        tile = tile,
                        transform = matrix
                    };
                }
                
                _tilemapPlatforms.SetTiles(platformTiles, true);
                
                if (Random.Range(0, 10) < 2)
                {
                    Matrix4x4 soapyMatrix = Matrix4x4.TRS(matrixPosition + new Vector3(0, 5f/32f, 0), 
                        Quaternion.Euler(0, 0, 0), Vector3.one);
                    miniSoapyFloorGenerator.Generate(new Vector3Int(limitTilesLeft + Random.Range(1, size-1), y, 0), 
                        soapyMatrix);
                }

                limitTilesLeft += size + Random.Range(2, 4);

            }
        }
    }
}