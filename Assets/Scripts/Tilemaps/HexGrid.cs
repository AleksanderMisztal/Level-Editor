using System.Collections.Generic;
using GameDataStructures.Positioning;
using LevelEditor.Saving;
using UnityEngine;

namespace LevelEditor.Tilemaps
{
    public class HexGrid
    {
        private readonly ResizableGridBase gridBase;
        private readonly GameObject[,] tiles;

        public HexGrid(ResizableGridBase gridBase)
        {
            this.gridBase = gridBase;
            tiles = new GameObject[ResizableGridBase.maxSize, ResizableGridBase.maxSize];
        }

        public void SetTile(Vector3 wp, GameObject tile)
        {
            VectorTwo coords = gridBase.ToOffset(wp);
            SetTile(coords.X, coords.Y, tile);
        }

        public void SetTile(int x, int y, GameObject tile)
        {
            if (x < 0 || x >= gridBase.XSize || y < 0 || y >= gridBase.YSize) return;
            GameObject go = tiles[x, y];
            if (!(go is null)) Object.Destroy(go); 
            tiles[x, y] = tile;
        }

        public GameObject GetTile(int x, int y)
        {
            if (x < 0 || x >= ResizableGridBase.maxSize || y < 0 || y >= ResizableGridBase.maxSize) return default;
            return tiles[x, y];
        }
        
        public GridDto Dto()
        {
            List<string> gridTiles = new List<string>(); 
            for (int y = 0; y < gridBase.YSize; y++)
            for (int x = 0; x < gridBase.XSize; x++)
            {
                gridTiles.Add(tiles[x, y]?.name);
            }

            return new GridDto {xSize = gridBase.XSize, ySize = gridBase.YSize, objects = gridTiles.ToArray()};
        }
    }
}