using System;
using GameDataStructures.Positioning;
using UnityEngine;
using CodeMonkey.Utils;

namespace Tilemaps
{
    public class HexGrid<TTile>
    {
        private readonly int xSize;
        private readonly int ySize;
        private readonly float cellSize;
        private readonly Vector3 offset;

        private readonly TTile[,] tiles;
        private readonly TextMesh[,] texts;
        
        private readonly Color color = Color.white;
        private readonly Vector3 left;
        private readonly Vector3 upLeft;

        public HexGrid(int xSize, int ySize, float cellSize, Vector3 offset)
        {
            this.xSize = xSize;
            this.ySize = ySize;
            this.cellSize = cellSize;
            this.offset = offset;
            
            left = Vector3.left * cellSize;
            upLeft = new Vector3(-.5f, (float)Math.Sqrt((double)3f/4)) * cellSize;
            
            tiles = new TTile[xSize, ySize];
            texts = new TextMesh[xSize, ySize];
            DrawTiles();
        }

        public HexGrid(int xSize, int ySize, float cellSize) : this(xSize, ySize, cellSize, Vector3.zero) { }

        private void DrawTiles()
        {
            for (int x = 0; x < xSize; x++)
            for (int y = 0; y < ySize; y++)
            {
                Vector3 center = offset + Cube.FromOffset(x, y).ToWorld(cellSize);
                CreateTile(center, x, y);
                DrawLines(center);
            }
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

        private void Draw(Vector3 offset, Vector3 end1, Vector3 end2)
        {
            Debug.DrawLine(offset + end1, offset + end2, color, 1000f);
        }

        private void CreateTile(Vector3 position, int x, int y)
        {
            TTile tile = default;
            TextMesh text = UtilsClass.CreateWorldText(tile?.ToString(), null, position);
            tiles[x, y] = tile;
            texts[x, y] = text;
        }

        public void SetTile(Vector3 wp, TTile tile)
        {
            Cube cube = Cube.ToCube(wp, cellSize);
            VectorTwo coords = cube.ToOffset();
            SetTile(coords, tile);
        }

        public void SetTile(VectorTwo coords, TTile tile)
        {
            Debug.Log(coords);
            int x = coords.X, y = coords.Y;
            if (x < 0 || x >= xSize || y < 0 || y >= ySize) return;
            tiles[x, y] = tile;
            texts[x, y].text = tile.ToString();
        }
    }
}