using System.Collections.Generic;
using GameDataStructures.Positioning;
using LevelEditor.Saving;
using LevelEditor.Tools;
using LevelEditor.Troops;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

namespace LevelEditor.Tilemaps
{
    public class TroopGrid : HexGrid<GameObject>
    {
        public TroopGrid(GridBase gridBase) : base(gridBase) { }

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
                TroopTemplate t = null;
                if (!string.IsNullOrEmpty(name)) t = TroopTool.GetTroop(name);
                SetTile(x, y, t);
            }
        }

        public void SetTile(int x, int y, TroopTemplate tile)
        {
            if (!gridBase.IsInside(x, y)) return;
            GameObject troop = GetTile(x, y);
            if (!(troop is null)) Object.Destroy(troop);
            base.SetTile(x, y, tile is null ? null : CreateTroop(gridBase.ToWorld(x, y), tile));
        }

        public void SetTile(Vector3 wp, TroopTemplate tile)
        {
            VectorTwo pos = gridBase.ToOffset(wp);
            SetTile(pos.X, pos.Y, tile);
        }

        private static GameObject CreateTroop(Vector3 position, TroopTemplate template)
        {
            GameObject go = new GameObject(template.name);
            go.transform.position = position;
            SpriteRenderer spriteRenderer = go.gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = template.Sprite;
            go.transform.localScale *= 7;
            return go;
        }
    }
}