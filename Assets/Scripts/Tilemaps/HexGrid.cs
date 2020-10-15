using GameDataStructures.Positioning;
using UnityEngine;

namespace Tilemaps
{
    public class HexGrid<T>
    {
        protected readonly GridBase gridBase;
        protected readonly T[,] tiles;

        public HexGrid(GridBase gridBase)
        {
            this.gridBase = gridBase;
            tiles = new T[gridBase.xSize, gridBase.ySize];
        }

        public void SetTile(Vector3 wp, T tile)
        {
            VectorTwo coords = gridBase.ToOffset(wp);
            int x = coords.X, y = coords.Y;
            if (x < 0 || x >= gridBase.xSize || y < 0 || y >= gridBase.ySize) return;
            SetTile(x, y, tile);
        }

        public virtual void SetTile(int x, int y, T tile)
        {
            tiles[x, y] = tile;
        }

        public T GetTile(Vector3 wp)
        {
            VectorTwo coords = gridBase.ToOffset(wp);
            int x = coords.X, y = coords.Y;
            if (x < 0 || x >= gridBase.xSize || y < 0 || y >= gridBase.ySize) return default;
            return GetTile(x, y);
        }
        
        public virtual T GetTile(int x, int y)
        {
            return tiles[x, y];
        }
    }
}