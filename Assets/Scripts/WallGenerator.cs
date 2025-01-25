using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallGenerator : MonoBehaviour
{
    enum TileKind
    {
        TERRACOTA,
        RACHOLAS,
        PIEDRA
    };

    // Start is called before the first frame update
    public Tilemap tilemap;
        
    private int max_height_generated;
    public int height_init;
    public int limit_tiles_left;
    public int limit_tiles_right;

    private int last_change;
    private int size;
    private TileKind tile_kind;
    private TileKind last_tile_kind;
    private int times_tile_repeated;

    public Tile[] wall_1 = new Tile[4]; // terracota
    public Tile wall_2; // racholas
    public Tile[] wall_3 = new Tile[4]; // piedra grande

    void Start()
    {
        last_change = 0;
        size = 1;
        tile_kind = TileKind.TERRACOTA;
        last_tile_kind = TileKind.TERRACOTA;
        times_tile_repeated = 0;

        generate(0, height_init);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generate(int init_height, int end_height)
    {
        // generar inicio del mapa
        for (int i = init_height; i < end_height; i++) {
            if ((i - last_change) % size == 0)
            {
                Debug.Log(times_tile_repeated);
                size = UnityEngine.Random.Range(5, 8);
                last_change = i;

                do
                {
                    int random_num = UnityEngine.Random.Range(0, 3);

                    if (random_num == 0)
                    {
                        tile_kind = TileKind.TERRACOTA;
                    }
                    else if (random_num == 1)
                    {
                        tile_kind = TileKind.RACHOLAS;
                    }
                    else
                    {
                        tile_kind = TileKind.PIEDRA;
                    }
                } while (tile_kind == last_tile_kind && times_tile_repeated == 2);

                if (tile_kind == last_tile_kind)
                {
                    ++times_tile_repeated;
                } else
                {
                    times_tile_repeated = 0;
                }
                // generación de cambio de textura

                if (i != 0)
                {
                    for (int j = limit_tiles_left; j < limit_tiles_right; j++)
                    {
                        int random_num_transition = UnityEngine.Random.Range(0, 2);

                        if (random_num_transition == 0)
                        {
                            if (tile_kind == TileKind.TERRACOTA)
                            {
                                tilemap.SetTile(new Vector3Int(j, i - 1, 0), wall_1[3 - (Math.Abs(j % 2) + 2 * (Math.Abs(i - 1) % 2))]);
                            }
                            else if (tile_kind == TileKind.RACHOLAS)
                            {
                                tilemap.SetTile(new Vector3Int(j, i - 1, 0), wall_2);
                            }
                            else
                            {
                                tilemap.SetTile(new Vector3Int(j, i - 1, 0), wall_3[3 - (Math.Abs(j % 2) + 2 * (Math.Abs(i - 1) % 2))]);
                            }
                        }
                    }
                }
            }

            for (int j = limit_tiles_left;  j < limit_tiles_right; j++)
            {
                if (tile_kind == TileKind.TERRACOTA)
                {
                    tilemap.SetTile(new Vector3Int(j, i, 0), wall_1[3 - (Math.Abs(j % 2) + 2 * (i % 2))]);
                }
                else if (tile_kind == TileKind.RACHOLAS)
                {
                    tilemap.SetTile(new Vector3Int(j, i, 0), wall_2);
                } else
                {
                    tilemap.SetTile(new Vector3Int(j, i, 0), wall_3[3 - (Math.Abs(j % 2) + 2 * (i % 2))]);
                }
            }

            last_tile_kind = tile_kind;
        }


        max_height_generated = end_height;
    }
}
