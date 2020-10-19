using GameDataStructures.Positioning;
using UnityEngine;

namespace LevelEditor.Tilemaps
{
    public class GridBase
    {
        private readonly int xSize;
        private readonly int ySize;
        private readonly float cellSize;
        private readonly GameObject[,] tiles;

        public GridBase(int xSize, int ySize, float cellSize)
        {
            this.xSize = xSize;
            this.ySize = ySize;
            this.cellSize = cellSize;
            tiles = new GameObject[xSize, ySize];

            DrawTiles();
        }
        
        private void DrawTiles()
        {
            for (int x = 0; x < xSize; x++)
            for (int y = 0; y < ySize; y++)
            {
                Vector3 center = Cube.FromOffset(x, y).ToWorld(cellSize);
                GameObject tile = LineDrawer.DrawHex(center, cellSize);
                tiles[x, y] = tile;
            }
        }

        public bool IsInside(int x, int y) => !(x < 0 || x >= xSize || y < 0 || y >= ySize);
        public Vector3 GetHexCenter(Vector3 wp) => Cube.ToCube(wp, cellSize).ToWorld(cellSize);
        public Vector3 ToWorld(int x, int y) => Cube.FromOffset(x, y).ToWorld(cellSize);
        public VectorTwo ToOffset(Vector3 wp) => Cube.ToCube(wp, cellSize).ToOffset();
    }
}