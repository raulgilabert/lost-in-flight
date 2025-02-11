using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallGenerator : MonoBehaviour
{
    enum TileKind
    {
        Sandstone,
        Tile,
        Stone
    };

    // Start is called before the first frame update
    [SerializeField] private Tilemap tilemapWall;
    [SerializeField] private Grid grid;
    [SerializeField] private PlatformsGenerator platformGen;

    [SerializeField] private int heightInit;
    [SerializeField] private int limitTilesLeft;
    [SerializeField] private int limitTilesRight;
    [SerializeField] private int playerNextToGenMagic;

    private int _lastChange;
    private int _size;
    private TileKind _tileKind;
    private TileKind _lastTileKind;
    private int _timesTileRepeated;

    private int _nextToGenPlatform;
    private float _mapWidth;

    private int _playerNextToGen;

    [SerializeField] private MiniSoapyFloorGenerator soapyFloorGenerator;
    [SerializeField] private int genLimit;

    [SerializeField] private Tile[] wallSandstone = new Tile[4];
    [SerializeField] private Tile wallTile;
    [SerializeField] private Tile[] wallStone = new Tile[4];

    void Start()
    {
        _lastChange = 0;
        _size = 1;
        _tileKind = TileKind.Sandstone;
        _lastTileKind = TileKind.Sandstone;
        _timesTileRepeated = 0;
        _nextToGenPlatform = UnityEngine.Random.Range(2, 3);
        _playerNextToGen = heightInit;

        _mapWidth = grid.CellToWorld(new Vector3Int(limitTilesRight, 0, 0)).x * 2;

        Generate(0, heightInit);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.player.transform.position.y > _playerNextToGen - playerNextToGenMagic)
        {
            Generate(_playerNextToGen, _playerNextToGen + playerNextToGenMagic);
            
            _playerNextToGen += playerNextToGenMagic;
        }
    }
    
    private TileBase SetTile(TileKind tileKind, int x, int y)
    {
        return tileKind switch
        {
            TileKind.Sandstone => wallSandstone[3 - (Math.Abs(x % 2) + 2 * (Math.Abs(y) % 2))],
            TileKind.Tile => wallTile,
            TileKind.Stone => wallStone[3 - (Math.Abs(x % 2) + 2 * (Math.Abs(y) % 2))],
            _ => (TileBase)null
        };
    }

    private static TileKind GetRandomTileKind()
    {
        int randomNum = UnityEngine.Random.Range(0, 3);

        return randomNum switch
        {
            0 => TileKind.Sandstone,
            1 => TileKind.Tile,
            2 => TileKind.Stone,
            _ => TileKind.Sandstone
        };
    }

    private void GeneratePlatforms(TileKind tileKind, int y)
    {
         int qttyOfPlatforms = UnityEngine.Random.Range(2, 5);
         float leftPos = grid.CellToWorld(new Vector3Int(limitTilesLeft, 0, 0)).x;

         for (int platformNum = 0; platformNum < qttyOfPlatforms; ++platformNum)
         {
             float platformSize =
                 UnityEngine.Random.Range(_mapWidth / (qttyOfPlatforms * 2), _mapWidth / qttyOfPlatforms);
             float yPos = grid.CellToWorld(new Vector3Int(0, y, 0)).y + UnityEngine.Random.Range(-0.5f, 0.5f);
             
             int platformTexture = tileKind switch
             {
                 TileKind.Sandstone => 0,
                 TileKind.Tile => 1,
                 TileKind.Stone => 2,
                 _ => 0
             };
             
             platformGen.Generate(new Vector3(leftPos + platformSize / 2, yPos, 0), platformTexture, platformSize);
             
             int genSoapyFloorRandomNum = UnityEngine.Random.Range(0, genLimit);

             if (genSoapyFloorRandomNum == 0)
             {
                 soapyFloorGenerator.Generate(new Vector3(
                     UnityEngine.Random.Range((leftPos + 1f), (leftPos + platformSize - 1f)), yPos + 0.16f, 0));
             }
             
             leftPos += platformSize + UnityEngine.Random.Range(1f, 4f);
         }
         
         _nextToGenPlatform = y + UnityEngine.Random.Range(2, 4);
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
            if ((y - _lastChange) % _size == 0)
            {
                _size = UnityEngine.Random.Range(5, 8);
                _lastChange = y + initHeight;
                
                do
                {
                    _tileKind = GetRandomTileKind();
                } while (_tileKind == _lastTileKind && _timesTileRepeated == 2);
                
                _timesTileRepeated = (_tileKind == _lastTileKind) ? _timesTileRepeated + 1 : 0;
                
                if (y != 0)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        int randomNumTransition = UnityEngine.Random.Range(0, 2);

                        if (randomNumTransition == 0)
                        {
                            tiles[(y - 1) * width + x] = SetTile(_tileKind, x, y - 1);
                        }
                    }
                }
            }
            
            if (y + initHeight == _nextToGenPlatform)
            {
                GeneratePlatforms(_tileKind, y + initHeight);
            }
         
            for (int x = 0; x < width; ++x)
            {
                tiles[y * width + x] = SetTile(_tileKind, x, y);
            }
            _lastTileKind = _tileKind;
        }
        
        tilemapWall.SetTilesBlock(bounds, tiles);
    }
}
