using System.Collections.Generic;
using LevelEditor.Saving;
using LevelEditor.Tools;
using UnityEngine;
using UnityEngine.Assertions;

namespace LevelEditor.Tilemaps
{
    public class TerrainGrid : HexGrid<Terrain>
    {
        private readonly TextGrid textGrid;
        public TerrainGrid(GridBase gridBase) : base(gridBase)
        {
            textGrid = new TextGrid(gridBase);
        }

        public GridDto Dto()
        {
            List<string> gridTiles = new List<string>(); 
            for (int y = 0; y < gridBase.YSize; y++)
            for (int x = 0; x < gridBase.XSize; x++)
            {
                gridTiles.Add(tiles[x, y]?.name);
            }

            return new GridDto() {xSize = gridBase.XSize, ySize = gridBase.YSize, data = gridTiles.ToArray()};
        }

        public void Load(GridDto dto)
        {
            Assert.AreEqual(gridBase.XSize, dto.xSize);
            Assert.AreEqual(gridBase.YSize, dto.ySize);
            
            int id = 0;
            for (int y = 0; y < gridBase.YSize; y++)
            for (int x = 0; x < gridBase.XSize; x++)
            {
                string name = dto.data[id++];
                Terrain t = null;
                if (!string.IsNullOrEmpty(name)) t = TerrainTool.GetTerrain(name);
                SetTile(x, y, t);
            }
        }

        public override void SetTile(int x, int y, Terrain tile)
        {
            base.SetTile(x, y, tile);
            textGrid.SetTile(x, y, tile?.name);
        }

        public TextMesh GetTextTile(int x, int y)
        {
            if (x < 0 || x >= gridBase.maxXSize || y < 0 || y >= gridBase.maxYSize) return default;
            return textGrid.GetTile(x, y);
        }
    }
}