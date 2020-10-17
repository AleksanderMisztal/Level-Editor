using System;
using System.Collections.Generic;
using GameDataStructures.Positioning;
using UnityEngine;

namespace Tilemaps
{
    public class GridBase
    {
        public int XSize { get; private set; }
        public int YSize { get; private set; }
        public int maxXSize;
        public int maxYSize;
        private readonly float cellSize;
        
        private static Color color = Color.white;
        private static readonly Vector3 left = Vector3.left;
        private static readonly Vector3 upLeft = new Vector3(-.5f, (float)Math.Sqrt((double)3f/4));
        public List<VectorTwo> newReachable;
        public List<VectorTwo> newUnreachable;

        public GridBase(int xSize, int ySize, float cellSize)
        {
            XSize = xSize;
            YSize = ySize;
            maxXSize = xSize;
            maxYSize = ySize;
            this.cellSize = cellSize;

            DrawMargin(5);
            DrawTiles();
        }

        public void Redraw(int newX, int newY)
        {
            newReachable = new List<VectorTwo>();
            newUnreachable = new List<VectorTwo>();
            for (int x = 0; x < newX; x++)
            for (int y = 0; y < newY; y++)
            {
                if (IsInside(x, y))
                {
                    y = YSize - 1;
                    continue;
                }
                newReachable.Add(new VectorTwo(x, y));
                Vector3 center = Cube.FromOffset(x, y).ToWorld(cellSize);
                DrawLines(center);
            }

            int prevX = XSize;
            int prevY = YSize;
            XSize = newX;
            YSize = newY;
            color = Color.gray;
            for (int x = 0; x < prevX; x++)
            for (int y = 0; y < prevY; y++)
            {
                if (IsInside(x, y))
                {
                    y = YSize - 1;
                    continue;
                }
                newUnreachable.Add(new VectorTwo(x, y));
                Vector3 center = Cube.FromOffset(x, y).ToWorld(cellSize);
                DrawLines(center);
            }
            color = Color.white;
            for (int x = 0; x < XSize; x++)
            {
                Vector3 center = Cube.FromOffset(x, YSize-1).ToWorld(cellSize);
                DrawLines(center);
            }
            for (int y = 0; y < YSize; y++)
            {
                Vector3 center = Cube.FromOffset(XSize-1, y).ToWorld(cellSize);
                DrawLines(center);
            }
        }
        
        private void DrawTiles()
        {
            for (int x = 0; x < XSize; x++)
            for (int y = 0; y < YSize; y++)
            {
                Vector3 center = Cube.FromOffset(x, y).ToWorld(cellSize);
                DrawLines(center);
            }
        }

        private void DrawMargin(int dw)
        {
            color = Color.gray;
            for (int x = -dw; x < XSize + dw; x++)
            for (int y = -dw; y < YSize + dw; y++)
            {
                if (IsInside(x, y)) y = YSize;
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
            return !(x < 0 || x >= XSize || y < 0 || y >= YSize);
        }

        public Vector3 ToWorld(int x, int y)
        {
            return Cube.FromOffset(x, y).ToWorld(cellSize);
        }

        public VectorTwo ToOffset(Vector3 wp) => Cube.ToCube(wp, cellSize).ToOffset();
    }
}