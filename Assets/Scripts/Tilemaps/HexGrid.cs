using GameDataStructures.Positioning;
using UnityEngine;

namespace LevelEditor.Tilemaps
{
    public class HexGrid<T>
    {
        protected readonly GridBase gridBase;
        protected readonly T[,] tiles;

        protected HexGrid(GridBase gridBase)
        {
            this.gridBase = gridBase;
            tiles = new T[gridBase.XSize, gridBase.YSize];
        }

        public void SetTile(Vector3 wp, T tile)
        {
            VectorTwo coords = gridBase.ToOffset(wp);
            SetTile(coords.X, coords.Y, tile);
        }

        protected virtual void SetTile(int x, int y, T tile)
        {
            if (x < 0 || x >= gridBase.XSize || y < 0 || y >= gridBase.YSize) return;
            tiles[x, y] = tile;
        }

        public T GetTile(Vector3 wp)
        {
            VectorTwo coords = gridBase.ToOffset(wp);
            return GetTile(coords.X, coords.Y);
        }

        public virtual T GetTile(int x, int y)
        {
            if (x < 0 || x >= gridBase.maxXSize || y < 0 || y >= gridBase.maxYSize) return default;
            return tiles[x, y];
        }
    }
}