using System.Collections.Generic;
using GameDataStructures.Positioning;
using LevelEditor.Saving;
using LevelEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Serialization;
using Terrain = LevelEditor.Tilemaps.Terrain;

namespace LevelEditor.Tools
{
    public class TerrainTool : HexTool
    {
        [FormerlySerializedAs("terrains")] [SerializeField] private Terrain[] templates;
        private int activeId;
        
        private static readonly Dictionary<string, Terrain> templateByName = new Dictionary<string, Terrain>();
        
        private TerrainGrid terrainGrid;
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
            terrainGrid = new TerrainGrid(gridBase);
            foreach (Terrain terrain in templates) 
                templateByName.Add(terrain.name, terrain);
        }

        public override void Resize()
        {
            foreach (VectorTwo v in gridBase.newReachable)
            {
                TextMesh text = terrainGrid.GetTextTile(v.X, v.Y);
                if (text is null) continue;
                text.gameObject.SetActive(true);
            }
            foreach (VectorTwo v in gridBase.newUnreachable)
            {
                TextMesh text = terrainGrid.GetTextTile(v.X, v.Y);
                if (text is null) continue;
                text.gameObject.SetActive(false);
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
                Vector3 mousePosition = Initializer.GetHexCenterWp();
                terrainGrid.SetTile(mousePosition, templates[activeId]);
            }
        }

        public override void Load()
        {
            GridDto dto = Saver.Read<GridDto>(LevelConfig.name + "/terrains");
            terrainGrid.Load(dto);
        }

        public override void Save()
        {
            Saver.Save(LevelConfig.name + "/terrains", terrainGrid.Dto());
        }
    }
}