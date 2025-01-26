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
    public Tilemap tilemap_wall;
    public Grid grid;
    public PlatformsGenerator platform_gen;

    private int max_height_generated;
    public int height_init;
    public int limit_tiles_left;
    public int limit_tiles_right;

    private int last_change;
    private int size;
    private TileKind tile_kind;
    private TileKind last_tile_kind;
    private int times_tile_repeated;

    private int next_to_gen_platform;
    private float map_width;

    private float player_next_to_gen;

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
        next_to_gen_platform = UnityEngine.Random.Range(2, 3);
        Debug.Log(next_to_gen_platform);

        map_width = grid.CellToWorld(new Vector3Int(limit_tiles_right, 0, 0)).x * 2;

        generate(0, height_init);

        player_next_to_gen = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.player.transform.position.y > player_next_to_gen)
        {
            generate(last_change, last_change + 50);

            player_next_to_gen += 50;
        }
    }

    private void generate(int init_height, int end_height)
    {
        for (int i = init_height; i < end_height; i++) {
            if ((i - last_change) % size == 0)
            {
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
                                tilemap_wall.SetTile(new Vector3Int(j, i - 1, 0), wall_1[3 - (Math.Abs(j % 2) + 2 * (Math.Abs(i - 1) % 2))]);
                            }
                            else if (tile_kind == TileKind.RACHOLAS)
                            {
                                tilemap_wall.SetTile(new Vector3Int(j, i - 1, 0), wall_2);
                            }
                            else
                            {
                                tilemap_wall.SetTile(new Vector3Int(j, i - 1, 0), wall_3[3 - (Math.Abs(j % 2) + 2 * (Math.Abs(i - 1) % 2))]);
                            }
                        }
                    }
                }
            }
            // generación de plataformas (aprox cada 2-3 iteraciones)
            if (i == next_to_gen_platform)
            {
                int texture;
                switch (tile_kind)
                {
                    case TileKind.TERRACOTA:
                        {
                            //Platformer.generate();
                            texture = 0;

                            break;
                        }
                    case TileKind.RACHOLAS:
                        {
                            texture = 1;
                            break;
                        }
                    case TileKind.PIEDRA:
                        {
                            texture = 2;
                            break;
                        }
                    default:
                        {
                            texture = 0;
                            break;
                        }
                }

                int qtty_of_platforms = UnityEngine.Random.Range(2, 5);

                float left_pos = grid.CellToWorld(new Vector3Int(limit_tiles_left, 0, 0)).x*4;

                for (int j = 0; j < qtty_of_platforms; j++)
                {
                    float size = UnityEngine.Random.Range(map_width / (qtty_of_platforms*2), (map_width) / qtty_of_platforms);

                    platform_gen.generate(new Vector3(left_pos + size / 2, grid.CellToWorld(new Vector3Int(0, i, 0)).y + UnityEngine.Random.Range(-0.5f, 0.5f), 0), texture, size);
                    left_pos += size + 40;
                }

                //platform_gen.generate(grid.CellToWorld(new Vector3Int(-3, i, 0)), texture, 20);
                next_to_gen_platform = i + UnityEngine.Random.Range(3, 5);

                Debug.Log(next_to_gen_platform);
            }

            for (int j = limit_tiles_left;  j < limit_tiles_right; j++)
            {
                if (tile_kind == TileKind.TERRACOTA)
                {
                    tilemap_wall.SetTile(new Vector3Int(j, i, 0), wall_1[3 - (Math.Abs(j % 2) + 2 * (i % 2))]);
                }
                else if (tile_kind == TileKind.RACHOLAS)
                {
                    tilemap_wall.SetTile(new Vector3Int(j, i, 0), wall_2);
                } else
                {
                    tilemap_wall.SetTile(new Vector3Int(j, i, 0), wall_3[3 - (Math.Abs(j % 2) + 2 * (i % 2))]);
                }
            }

            last_tile_kind = tile_kind;
        }


        max_height_generated = end_height;
    }
}
