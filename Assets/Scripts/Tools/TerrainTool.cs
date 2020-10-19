using System.Collections.Generic;
using GameDataStructures.Positioning;
using LevelEditor.CodeMonkey.Utils;
using LevelEditor.Saving;
using LevelEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Serialization;
using Terrain = LevelEditor.Terrains.Terrain;

namespace LevelEditor.Tools
{
    public class TerrainTool : HexTool
    {
        [FormerlySerializedAs("terrains")] [SerializeField] private Terrain[] templates;
        private int activeId;
        
        private static readonly Dictionary<string, Terrain> templateByName = new Dictionary<string, Terrain>();
        
        private HexGrid hexGrid;
        private GridBase gridBase;

        public static Terrain GetTerrain(string name)
        {
            try
            {
                return templateByName[name];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public override void Initialize(GridBase gridBase)
        {
            this.gridBase = gridBase;
            hexGrid = new HexGrid(gridBase);
            foreach (Terrain terrain in templates) 
                templateByName.Add(terrain.name, terrain);
        }

        public override void Resize()
        {
            foreach (VectorTwo v in gridBase.newReachable)
            {
                GameObject go = hexGrid.GetTile(v.X, v.Y);
                if (go is null) continue;
                go.SetActive(true);
            }
            foreach (VectorTwo v in gridBase.newUnreachable)
            {
                GameObject go = hexGrid.GetTile(v.X, v.Y);
                if (go is null) continue;
                go.SetActive(false);
            }
        }

        protected void Update()
        {
            if (!Enabled) return;
            
            if (Input.GetKeyDown("n"))
            {
                activeId++;
                activeId %= templates.Length;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 position = Initializer.GetHexCenterWp();
                VectorTwo v = gridBase.ToOffset(position);
                if (!gridBase.IsInside(v.X, v.Y)) return;
                hexGrid.SetTile(position, CreateObject(position, templates[activeId]));
            }
        }

        private GameObject CreateObject(int x, int y, Terrain template) 
            => CreateObject(gridBase.ToWorld(x, y), template);

        private GameObject CreateObject(Vector3 position, Terrain template) 
            => UtilsClass.CreateWorldText(template.name, null, position).gameObject;

        public override void Load()
        {
            GridDto dto = Saver.Read<GridDto>(LevelConfig.name + "/terrains");
            int id = 0;
            for (int y = 0; y < gridBase.YSize; y++)
            for (int x = 0; x < gridBase.XSize; x++)
            {
                Terrain template = GetTerrain(dto.objects[id++]);
                hexGrid.SetTile(x, y, template is null ? null : CreateObject(x, y, template));
            }
        }

        public override void Save()
        {
            Saver.Save(LevelConfig.name + "/terrains", hexGrid.Dto());
        }
    }
}