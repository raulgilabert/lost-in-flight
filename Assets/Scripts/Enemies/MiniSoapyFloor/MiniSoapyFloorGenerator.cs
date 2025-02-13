using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Enemies.MiniSoapyFloor
{
    public class MiniSoapyFloorGenerator : MonoBehaviour
    {
        [SerializeField] private AnimatedTile soapyFloorTile;
        [SerializeField] private Tilemap tilemap;

        private List<TileChangeData> _updateQueue;

        private void Start()
        {
            _updateQueue = new List<TileChangeData>();
        }

        public void QueueGenerate(Vector3Int position, Matrix4x4 transformMatrix)
        {
            _updateQueue.Add(new TileChangeData
            {
                color = Color.white,
                position = position,
                tile = soapyFloorTile,
                transform = transformMatrix,
            });
        }

        public void FlushQueues()
        {
            tilemap.SetTiles(_updateQueue.ToArray(), true);
            _updateQueue.Clear();
        }
    }
}
