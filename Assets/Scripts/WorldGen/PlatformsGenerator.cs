using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Serialization;

namespace WorldGen
{
    public class PlatformsGenerator : MonoBehaviour
    {
        [SerializeField] private TileBase[] textures = new TileBase[3];

        private Tilemap _tilemap;
    
        // Start is called before the first frame update
        void Start()
        {
            _tilemap = GameObject.Find("Platforms").GetComponentInChildren<Tilemap>();
        }

        public void Generate(TileKind tileKind, int y, int limitTilesLeft, int limitTilesRight)
        {
            int numPlatforms = Random.Range(3, 6);
            int mapWidth = limitTilesRight - limitTilesLeft;

            limitTilesLeft += Random.Range(-3, 3);

            for (int i = 0; i < numPlatforms; i++)
            {
                int size = Random.Range(4, mapWidth / (numPlatforms - 1));

                // TODO: Add generation of left and right variable width platforms

                TileBase tile = tileKind switch
                {
                    TileKind.Sandstone => textures[0],
                    TileKind.Tile => textures[1],
                    TileKind.Stone => textures[2],
                    _ => textures[0]
                };

                Matrix4x4 matrix = Matrix4x4.TRS(new Vector3(Random.Range(-0.32f, 0.32f), Random.Range(-0.48f, 0.48f)),
                    Quaternion.Euler(0, 0, 0), Vector3.one);

                for (int j = limitTilesLeft; j < limitTilesLeft + size; j++)
                {
                    _tilemap.SetTile(new Vector3Int(j, y), tile);
                    _tilemap.SetTransformMatrix(new Vector3Int(j, y), matrix);
                }

                limitTilesLeft += size + Random.Range(2, 4);

            }
        }
    }
}