﻿using System.Collections.Generic;
using GameDataStructures.Positioning;
using Saving;
using Tools;
using Troops;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

namespace Tilemaps
{
    public class TroopGrid : HexGrid<GameObject>
    {
        public TroopGrid(GridBase gridBase) : base(gridBase) { }

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