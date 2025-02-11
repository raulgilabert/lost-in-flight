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
    public Tilemap tilemapWall;
    public Grid grid;
    public PlatformsGenerator platform_gen;

    private int maxHeightGenerated;
    public int height_init;
    public int limitTilesLeft;
    public int limitTilesRight;
    public int player_next_to_gen_magic;

    private int last_change;
    private int size;
    private TileKind tile_kind;
    private TileKind last_tile_kind;
    private int times_tile_repeated;

    private int next_to_gen_platform;
    private float map_width;

    private float player_next_to_gen;

    public MiniSoapyFloorGenerator soapy_floor_generator;
    public int gen_limit;

    [SerializeField] private Tile[] wallSandstone = new Tile[4];
    [SerializeField] private Tile wallTile;
    [SerializeField] private Tile[] wallStone = new Tile[4];

    void Start()
    {
        last_change = 0;
        size = 1;
        tile_kind = TileKind.TERRACOTA;
        last_tile_kind = TileKind.TERRACOTA;
        times_tile_repeated = 0;
        next_to_gen_platform = UnityEngine.Random.Range(2, 4);
        //Debug.Log(next_to_gen_platform);

        map_width = grid.CellToWorld(new Vector3Int(limitTilesRight, 0, 0)).x * 2;

        Generate(0, height_init);

        player_next_to_gen = player_next_to_gen_magic;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.player.transform.position.y > player_next_to_gen)
        {
            Generate(last_change, last_change + player_next_to_gen_magic);

            player_next_to_gen += player_next_to_gen_magic;
        }
    }

    private void Generate(int initHeight, int endHeight)
    {
        int width = limitTilesRight - limitTilesLeft;
        int height = endHeight - initHeight;
        BoundsInt bounds =
            new BoundsInt(new Vector3Int(limitTilesLeft, initHeight, 0), new Vector3Int(width, height, 1));
        TileBase[] tiles = new TileBase[width * height];

        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                tiles[y*width + x] = wallSandstone[0];
            }
        }
        
        tilemapWall.SetTilesBlock(bounds, tiles);
        
        maxHeightGenerated = endHeight;
    }
    
    /*
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
                // generaci�n de cambio de textura

                if (i != 0)
                {
                    for (int j = limitTilesLeft; j < limitTilesRight; j++)
                    {
                        int random_num_transition = UnityEngine.Random.Range(0, 2);

                        if (random_num_transition == 0)
                        {
                            if (tile_kind == TileKind.TERRACOTA)
                            {
                                tilemapWall.SetTile(new Vector3Int(j, i - 1, 0), wallSandstone[3 - (Math.Abs(j % 2) + 2 * (Math.Abs(i - 1) % 2))]);
                            }
                            else if (tile_kind == TileKind.RACHOLAS)
                            {
                                tilemapWall.SetTile(new Vector3Int(j, i - 1, 0), wallTile);
                            }
                            else
                            {
                                tilemapWall.SetTile(new Vector3Int(j, i - 1, 0), wallStone[3 - (Math.Abs(j % 2) + 2 * (Math.Abs(i - 1) % 2))]);
                            }
                        }
                    }
                }
            }
            // generaci�n de plataformas (aprox cada 2-3 iteraciones)
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

                float left_pos = grid.CellToWorld(new Vector3Int(limitTilesLeft, 0, 0)).x;

                for (int j = 0; j < qtty_of_platforms; j++)
                {
                    float size = UnityEngine.Random.Range(map_width / (qtty_of_platforms*2), (map_width) / qtty_of_platforms);
                    float y_pos = grid.CellToWorld(new Vector3Int(0, i, 0)).y + UnityEngine.Random.Range(-0.5f, 0.5f);

                    platform_gen.generate(new Vector3(left_pos + size / 2, y_pos, 0), texture, size);

                    int gen_soapy_floor_random_num = UnityEngine.Random.Range(0, gen_limit);

                    if (gen_soapy_floor_random_num == 0)
                    {
                        soapy_floor_generator.generate(new Vector3(UnityEngine.Random.Range((left_pos + 1f), (left_pos + size - 1f)), y_pos + 0.16f, 0));
                    }

                    left_pos += size + UnityEngine.Random.Range(0.5f, 4f);
                }

                //platform_gen.generate(grid.CellToWorld(new Vector3Int(-3, i, 0)), texture, 20);
                next_to_gen_platform = i + UnityEngine.Random.Range(2, 4);

                //Debug.Log(next_to_gen_platform);
            }

            for (int j = limitTilesLeft;  j < limitTilesRight; j++)
            {
                if (tile_kind == TileKind.TERRACOTA)
                {
                    tilemapWall.SetTile(new Vector3Int(j, i, 0), wallSandstone[3 - (Math.Abs(j % 2) + 2 * (i % 2))]);
                }
                else if (tile_kind == TileKind.RACHOLAS)
                {
                    tilemapWall.SetTile(new Vector3Int(j, i, 0), wallTile);
                } else
                {
                    tilemapWall.SetTile(new Vector3Int(j, i, 0), wallStone[3 - (Math.Abs(j % 2) + 2 * (i % 2))]);
                }
            }

            last_tile_kind = tile_kind;
        }


        maxHeightGenerated = end_height;
    }
    */
}
