using System;
using GameDataStructures.Positioning;
using UnityEngine;

namespace Tilemaps
{
    public class GridBase
    {
        public readonly int xSize;
        public readonly int ySize;
        private readonly float cellSize;
        
        private static Color color = Color.white;
        private static readonly Vector3 left = Vector3.left;
        private static readonly Vector3 upLeft = new Vector3(-.5f, (float)Math.Sqrt((double)3f/4));

        public GridBase(int xSize, int ySize, float cellSize)
        {
            this.xSize = xSize;
            this.ySize = ySize;
            this.cellSize = cellSize;

            DrawMargin(5);
            DrawTiles();
        }

        private void DrawTiles()
        {
            for (int x = 0; x < xSize; x++)
            for (int y = 0; y < ySize; y++)
            {
                Vector3 center = Cube.FromOffset(x, y).ToWorld(cellSize);
                DrawLines(center);
            }
        }

        private void DrawMargin(int dw)
        {
            color = Color.gray;
            for (int x = -dw; x < xSize + dw; x++)
            for (int y = -dw; y < ySize + dw; y++)
            {
                if (IsInside(x, y)) y = ySize;
                Vector3 center = Cube.FromOffset(x, y).ToWorld(cellSize);
                DrawLines(center);
            }
            color = Color.white;
        }

        private void DrawLines(Vector3 center)
        {
            Draw(center, left, upLeft);
            Draw(center, upLeft, upLeft-left);
            Draw(center, upLeft-left, -left);
            Draw(center, -left, -upLeft);
            Draw(center, -upLeft, -upLeft+left);
            Draw(center, -upLeft+left, left);
        }

        private void Draw(Vector3 center, Vector3 end1, Vector3 end2)
        {
            Debug.DrawLine(center + end1 * cellSize, center + end2 * cellSize, color, 1000f);
        }

        public bool IsInside(int x, int y)
        {
            return !(x < 0 || x >= xSize || y < 0 || y >= ySize);
        }

        public Vector3 ToWorld(int x, int y)
        {
            return Cube.FromOffset(x, y).ToWorld(cellSize);
        }

        public VectorTwo ToOffset(Vector3 wp) => Cube.ToCube(wp, cellSize).ToOffset();
    }
}