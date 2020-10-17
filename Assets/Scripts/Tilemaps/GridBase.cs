using System.Collections.Generic;
using GameDataStructures.Positioning;
using UnityEngine;

namespace LevelEditor.Tilemaps
{
    public class GridBase
    {
        public int XSize { get; private set; }
        public int YSize { get; private set; }
        public readonly int maxXSize;
        public readonly int maxYSize;
        private readonly float cellSize;
        
        public List<VectorTwo> newReachable;
        public List<VectorTwo> newUnreachable;
        private GameObject[,] tiles; 

        public GridBase(int xSize, int ySize, float cellSize)
        {
            XSize = xSize;
            YSize = ySize;
            maxXSize = xSize;
            maxYSize = ySize;
            this.cellSize = cellSize;
            tiles = new GameObject[xSize, ySize];

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
                tiles[x, y].SetActive(true);
            }

            int prevX = XSize;
            int prevY = YSize;
            XSize = newX;
            YSize = newY;
            for (int x = 0; x < prevX; x++)
            for (int y = 0; y < prevY; y++)
            {
                if (IsInside(x, y))
                {
                    y = YSize - 1;
                    continue;
                }
                newUnreachable.Add(new VectorTwo(x, y));
                tiles[x, y].SetActive(false);
            }
        }
        
        private void DrawTiles()
        {
            for (int x = 0; x < XSize; x++)
            for (int y = 0; y < YSize; y++)
            {
                Vector3 center = Cube.FromOffset(x, y).ToWorld(cellSize);
                tiles[x, y] = LineDrawer.DrawHex(center, cellSize);
            }
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