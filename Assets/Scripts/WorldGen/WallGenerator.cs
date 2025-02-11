using System;
using Enemies.MiniSoapyFloor;
using GlobalState;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace WorldGen
{    
    public enum TileKind
    {
        Sandstone,
        Tile,
        Stone
    }
    public class WallGenerator : MonoBehaviour
    {
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
            _playerNextToGen = (int)(GameManager.Instance.player.transform.position.y - 3);
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
                    platformGen.Generate(_tileKind, y + initHeight, limitTilesLeft, limitTilesRight);
                    
                    _nextToGenPlatform += UnityEngine.Random.Range(2, 4);
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
}