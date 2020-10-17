using System.Collections.Generic;
using Saving;
using Tilemaps;
using UnityEngine;
using Terrain = Tilemaps.Terrain;

namespace Tools
{
    public class TerrainTool : DesignTool
    {
        [SerializeField] private Terrain[] terrains;
        private int currentTerrainId;
        private static readonly Dictionary<string, Terrain> terrainByName = new Dictionary<string, Terrain>();
        
        private TerrainGrid terrainGrid;

        public static Terrain GetTerrain(string name)
        {
            try
            {
                return terrainByName[name];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }

        public override void Initialize()
        {
            terrainGrid = new TerrainGrid(Initializer.grid);
            foreach (Terrain terrain in terrains) 
                terrainByName.Add(terrain.name, terrain);
        }

        public override void Resize()
        {
            
        }

        protected void Update()
        {
            if (!Enabled) return;
            
            if (Input.GetKeyDown("n"))
            {
                currentTerrainId++;
                currentTerrainId %= terrains.Length;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Initializer.GetMouseWorldPosition();
                Terrain terrain = terrains[currentTerrainId];
                
                terrainGrid.SetTile(mousePosition, terrain);
            }
        }

        public override void Load()
        {
            GridDto dto = Saver.Read<GridDto>(LevelConfiguration.name + "/terrains");
            terrainGrid.Load(dto);
        }

        public override void Save()
        {
            Saver.Save(LevelConfiguration.name + "/terrains", terrainGrid.Dto());
        }
    }
}