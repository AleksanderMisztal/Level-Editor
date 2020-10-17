using GameDataStructures.Positioning;
using UnityEngine;

namespace Tilemaps
{
    public class HexGrid<T>
    {
        protected readonly GridBase gridBase;
        protected readonly T[,] tiles;

        protected HexGrid(GridBase gridBase)
        {
            this.gridBase = gridBase;
            tiles = new T[gridBase.xSize, gridBase.ySize];
        }

        public void SetTile(Vector3 wp, T tile)
        {
            VectorTwo coords = gridBase.ToOffset(wp);
            SetTile(coords.X, coords.Y, tile);
        }

        protected virtual void SetTile(int x, int y, T tile)
        {
            if (x < 0 || x >= gridBase.xSize || y < 0 || y >= gridBase.ySize) return;
            tiles[x, y] = tile;
        }

        public T GetTile(Vector3 wp)
        {
            VectorTwo coords = gridBase.ToOffset(wp);
            return GetTile(coords.X, coords.Y);
        }

        protected virtual T GetTile(int x, int y)
        {
            if (x < 0 || x >= gridBase.xSize || y < 0 || y >= gridBase.ySize) return default;
            return tiles[x, y];
        }
    }
}