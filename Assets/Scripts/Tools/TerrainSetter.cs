using System.Collections.Generic;
using Saving;
using Tilemaps;
using UnityEngine;
using Terrain = Tilemaps.Terrain;

namespace Tools
{
    public class TerrainSetter : DesignTool
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

        private void Start()
        {
            foreach (Terrain terrain in terrains) 
                terrainByName.Add(terrain.name, terrain);
            terrainGrid = new TerrainGrid(Initializer.grid);
        }

        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Saver.Save(LevelConfiguration.name +  "_terrains", terrainGrid.Dto());
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                TerrainGridDto dto = Saver.Read<TerrainGridDto>(LevelConfiguration.name +  "_terrains");
                terrainGrid.Load(dto);
            }
            
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
    }
}