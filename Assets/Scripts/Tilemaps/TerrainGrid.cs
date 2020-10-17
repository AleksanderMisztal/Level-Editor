using System.Collections.Generic;
using Saving;
using Tools;
using UnityEngine.Assertions;

namespace Tilemaps
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
            for (int y = 0; y < gridBase.ySize; y++)
            for (int x = 0; x < gridBase.xSize; x++)
            {
                gridTiles.Add(tiles[x, y]?.name);
            }

            return new GridDto() {xSize = gridBase.xSize, ySize = gridBase.ySize, data = gridTiles.ToArray()};
        }

        public void Load(GridDto dto)
        {
            Assert.AreEqual(gridBase.xSize, dto.xSize);
            Assert.AreEqual(gridBase.ySize, dto.ySize);
            
            int id = 0;
            for (int y = 0; y < gridBase.ySize; y++)
            for (int x = 0; x < gridBase.xSize; x++)
            {
                string name = dto.data[id++];
                Terrain t = null;
                if (!string.IsNullOrEmpty(name)) t = TerrainTool.GetTerrain(name);
                SetTile(x, y, t);
            }
        }

        protected override void SetTile(int x, int y, Terrain tile)
        {
            base.SetTile(x, y, tile);
            textGrid.SetTile(x, y, tile?.name);
        }
    }
}